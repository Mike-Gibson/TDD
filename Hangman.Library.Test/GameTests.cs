using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Hangman.Library.Test
{
    public class GameTests
    {
        #region Starting New Game

        public class StartNewGameWithValidWord : GameSpecBase
        {
            public override void Given()
            { }

            public override void When()
            {
                StartNewGame("hangman");                
            }

            [Then]
            public void GameShouldBeCreatedSuccessfully()
            {
                // There should be no exception thrown
                Assert.IsNull(base.Exception);
                Assert.IsNotNull(base.Game);

                // Status should be InProgress
                Assert.AreEqual(base.Game.Status, GameStatus.InProgress);
            }
        }

        public class StartNewGameWithEmptyWord : GameSpecBase
        {
            public override void Given()
            { }

            public override void When()
            {
                StartNewGame("");
            }

            [Then]
            public void GameShouldNotBeCreatedSuccessfully()
            {
                // There should be an exception thrown
                Assert.IsNotNull(base.Exception);
                Assert.IsInstanceOf<ArgumentNullException>(base.Exception);
            }
        }
        
        public class StartNewGameWithNullWord : GameSpecBase
        {
            public override void Given()
            { }

            public override void When()
            {
                StartNewGame(null);
            }

            [Then]
            public void GameShouldNotBeCreatedSuccessfully()
            {
                // There should be an exception thrown
                Assert.IsNotNull(base.Exception);
                Assert.IsInstanceOf<ArgumentNullException>(base.Exception);
            }
        }

        public class StartNewGameWithWhiteSpaceWord : GameSpecBase
        {
            public override void Given()
            { }

            public override void When()
            {
                StartNewGame("    ");
            }

            [Then]
            public void GameShouldNotBeCreatedSuccessfully()
            {
                // There should be an exception thrown
                Assert.IsNotNull(base.Exception);
                Assert.IsInstanceOf<ArgumentNullException>(base.Exception);
            }
        }

        public class StartNewGameWithInvalidWord : GameSpecBase
        {
            public override void Given()
            { }

            public override void When()
            {
                StartNewGame("ab_c");
            }

            [Then]
            public void GameShouldNotBeCreatedSuccessfully()
            {
                // There should be an exception thrown
                Assert.IsNotNull(base.Exception);
                Assert.IsInstanceOf<ArgumentException>(base.Exception);
            }
        }

        #endregion

        #region Making a Guess

        public class MakeValidGuessButDoesNotCompleteWord : GameSpecBase
        {
            GuessResult guessResult;
            char guessedChar;

            public override void Given()
            {
                StartNewGame("hangman");

                guessedChar = 'a';
            }

            public override void When()
            {                
                guessResult = base.Game.GuessCharacter(guessedChar);
            }

            [Then]
            public void GuessIsCorrect()
            {
                Assert.AreEqual(GuessResult.Correct, guessResult);
            }

            [Then]
            public void CurrentStatusIsCorrect()
            {
                List<CharacterState> currentState = new List<CharacterState>(Game.GetCurrentState());

                // h
                Assert.IsTrue(currentState[0].Character == 'h');
                Assert.IsFalse(currentState[0].Guessed);

                // a
                Assert.IsTrue(currentState[1].Character == 'a');
                Assert.IsTrue(currentState[1].Guessed);

                // n
                Assert.IsTrue(currentState[2].Character == 'n');
                Assert.IsFalse(currentState[2].Guessed);

                // g
                Assert.IsTrue(currentState[3].Character == 'g');
                Assert.IsFalse(currentState[3].Guessed);

                // m
                Assert.IsTrue(currentState[4].Character == 'm');
                Assert.IsFalse(currentState[4].Guessed);

                // a
                Assert.IsTrue(currentState[5].Character == 'a');
                Assert.IsTrue(currentState[5].Guessed);

                // n
                Assert.IsTrue(currentState[6].Character == 'n');
                Assert.IsFalse(currentState[6].Guessed);

                Assert.IsTrue(currentState.Count == 7);
            }
            
            [Then]
            public void GuessedLetterAddedToList()
            {
                IEnumerable<char> guessedCharacters = base.Game.GuessedCharacters;
                
                Assert.IsTrue(guessedCharacters.Count() == 1);
                Assert.IsTrue(base.Game.GuessedCharacters.Contains(guessedChar));
            }
        }

        public class MakeValidGuessAndCompletesWord : GameSpecBase
        {
            GuessResult guessResult;

            public override void Given()
            {
                StartNewGame("hangman");
            }

            public override void When()
            {
                base.Game.GuessCharacter('h');
                base.Game.GuessCharacter('a');
                base.Game.GuessCharacter('n');
                base.Game.GuessCharacter('g');
                
                guessResult = base.Game.GuessCharacter('m');
            }

            [Then]
            public void GuessIsCorrect()
            {
                Assert.AreEqual(GuessResult.Correct, guessResult);
            }

            [Then]
            public void CurrentStatusIsCorrect()
            {
                List<CharacterState> currentState = new List<CharacterState>(Game.GetCurrentState());

                // h
                Assert.IsTrue(currentState[0].Character == 'h');
                Assert.IsTrue(currentState[0].Guessed);

                // a
                Assert.IsTrue(currentState[1].Character == 'a');
                Assert.IsTrue(currentState[1].Guessed);

                // n
                Assert.IsTrue(currentState[2].Character == 'n');
                Assert.IsTrue(currentState[2].Guessed);

                // g
                Assert.IsTrue(currentState[3].Character == 'g');
                Assert.IsTrue(currentState[3].Guessed);

                // m
                Assert.IsTrue(currentState[4].Character == 'm');
                Assert.IsTrue(currentState[4].Guessed);

                // a
                Assert.IsTrue(currentState[5].Character == 'a');
                Assert.IsTrue(currentState[5].Guessed);

                // n
                Assert.IsTrue(currentState[6].Character == 'n');
                Assert.IsTrue(currentState[6].Guessed);

                Assert.IsTrue(currentState.Count == 7);
            }

            [Then]
            public void GuessedLetterAddedToList()
            {
                IEnumerable<char> guessedCharacters = base.Game.GuessedCharacters;

                Assert.IsTrue(guessedCharacters.Count() == 5);
                Assert.IsTrue(base.Game.GuessedCharacters.Contains('m'));
            }

            [Then]
            public void GameEndsSuccessfully()
            {
                Assert.IsTrue(base.Game.Status == GameStatus.PlayerWon);
            }
        }

        public class MakeInvalidGuessButDoesNotEndGame : GameSpecBase
        {
            GuessResult guessResult;

            public override void Given()
            {
                StartNewGame("hangman");

                base.Game.GuessCharacter('z');
            }

            public override void When()
            {
                guessResult = base.Game.GuessCharacter('y');
            }

            [Then]
            public void GuessIsIncorrect()
            {
                Assert.AreEqual(GuessResult.Incorrect, guessResult);
            }

            [Then]
            public void GuessedLetterAddedToList()
            {
                IEnumerable<char> guessedCharacters = base.Game.GuessedCharacters;

                Assert.IsTrue(guessedCharacters.Count() == 2);
                Assert.IsTrue(base.Game.GuessedCharacters.Contains('y'));
            }
            
            [Then]
            public void LivesAreReduced()
            {
                Assert.IsTrue(base.Game.Lives == (11 - 2));
            }
        }

        public class MakeInvalidGuessAndEndsGame : GameSpecBase
        {
            GuessResult guessResult;

            public override void Given()
            {
                StartNewGame("hangman");

                base.Game.GuessCharacter('b');
                base.Game.GuessCharacter('c');
                base.Game.GuessCharacter('d');
                base.Game.GuessCharacter('e');
                base.Game.GuessCharacter('f');
                base.Game.GuessCharacter('i');
                base.Game.GuessCharacter('j');
                base.Game.GuessCharacter('k');
                base.Game.GuessCharacter('l');
                base.Game.GuessCharacter('o');
            }

            public override void When()
            {
                guessResult = base.Game.GuessCharacter('p');
            }

            [Then]
            public void GuessIsIncorrect()
            {
                Assert.AreEqual(GuessResult.Incorrect, guessResult);
            }

            [Then]
            public void GuessedLetterAddedToList()
            {
                IEnumerable<char> guessedCharacters = base.Game.GuessedCharacters;

                Assert.IsTrue(guessedCharacters.Count() == 11);
                Assert.IsTrue(base.Game.GuessedCharacters.Contains('p'));
            }

            [Then]
            public void LivesEqualToZero()
            {
                Assert.IsTrue(base.Game.Lives == 0);
            }

            [Then]
            public void GameEndsWithLoss()
            {
                Assert.AreEqual(base.Game.Status, GameStatus.PlayerLost);
            }
        }

        public class MakeGuessThatIsAnInvalidCharacter : GameSpecBase
        {
            GuessResult guessResult;

            public override void Given()
            {
                StartNewGame("hangman");
            }

            public override void When()
            {
                guessResult = base.Game.GuessCharacter('+');
            }

            [Then]
            public void GuessIsInvalid()
            {
                Assert.AreEqual(GuessResult.Invalid, guessResult);
            }

            [Then]
            public void GuessedCharacterNotAddedToList()
            {
                IEnumerable<char> guessedCharacters = base.Game.GuessedCharacters;

                Assert.IsTrue(guessedCharacters.Count() == 0);
                Assert.IsFalse(base.Game.GuessedCharacters.Contains('+'));
            }

            [Then]
            public void DidNotLoseLife()
            {
                Assert.IsTrue(base.Game.Lives == 11);
            }
        }

        public class MakeGuessThatIsInWordButNotSameCase : GameSpecBase
        {
            GuessResult guessResult;

            public override void Given()
            {
                StartNewGame("hangman");
            }

            public override void When()
            {
                guessResult = base.Game.GuessCharacter('H');
            }

            [Then]
            public void GuessIsCorrect()
            {
                Assert.AreEqual(GuessResult.Correct, guessResult);
            }

            [Then]
            public void GuessedCharacterAddedToList()
            {
                IEnumerable<char> guessedCharacters = base.Game.GuessedCharacters;

                Assert.IsTrue(guessedCharacters.Count() == 1);
                Assert.IsTrue(base.Game.GuessedCharacters.Contains('h'));
            }

            [Then]
            public void ListOfGuessedCharactersDoesNotIncludeDuplicate()
            {
                Assert.IsTrue(base.Game.GuessedCharacters.Count() == 1);
            }
        }

        public class MakeDuplicateGuessThatIsInWord : GameSpecBase
        {
            GuessResult guessResult;

            public override void Given()
            {
                StartNewGame("hangman");

                base.Game.GuessCharacter('h');
            }

            public override void When()
            {
                guessResult = base.Game.GuessCharacter('h');
            }

            [Then]
            public void GuessIsInvalid()
            {
                Assert.AreEqual(GuessResult.Invalid, guessResult);
            }

            [Then]
            public void DidNotLoseLife()
            {
                Assert.IsTrue(base.Game.Lives == 11);
            }

            [Then]
            public void ListOfGuessedCharactersDoesNotIncludeDuplicate()
            {
                Assert.IsTrue(base.Game.GuessedCharacters.Count() == 1);
            }
        }

        public class MakeDuplicateGuessThatIsNotInWord : GameSpecBase
        {
            GuessResult guessResult;

            public override void Given()
            {
                StartNewGame("hangman");

                base.Game.GuessCharacter('z');
            }

            public override void When()
            {
                guessResult = base.Game.GuessCharacter('z');
            }

            [Then]
            public void GuessIsInvalid()
            {
                Assert.AreEqual(GuessResult.Invalid, guessResult);
            }

            [Then]
            public void DidNotLoseLife()
            {
                Assert.IsTrue(base.Game.Lives == (11 - 1));
            }

            [Then]
            public void ListOfGuessedCharactersDoesNotIncludeDuplicate()
            {
                Assert.IsTrue(base.Game.GuessedCharacters.Count() == 1);
            }
        }
        
        public class MakeDuplicateGuessThatIsInDifferentCaseAndNotInWord : GameSpecBase
        {
            GuessResult guessResult;

            public override void Given()
            {
                StartNewGame("hangman");

                base.Game.GuessCharacter('z');
            }

            public override void When()
            {
                guessResult = base.Game.GuessCharacter('Z');
            }

            [Then]
            public void GuessIsInvalid()
            {
                Assert.AreEqual(GuessResult.Invalid, guessResult);
            }

            [Then]
            public void DidNotLoseLife()
            {
                Assert.IsTrue(base.Game.Lives == (11 - 1));
            }

            [Then]
            public void ListOfGuessedCharactersDoesNotIncludeDuplicate()
            {
                Assert.IsTrue(base.Game.GuessedCharacters.Count() == 1);
            }
        }

        public class MakeGuessAfterAlreadyWinning : GameSpecBase
        {
            public override void Given()
            {
                StartNewGame("hangman");

                base.Game.GuessCharacter('h');
                base.Game.GuessCharacter('a');
                base.Game.GuessCharacter('n');
                base.Game.GuessCharacter('g');
                base.Game.GuessCharacter('m');
            }

            public override void When()
            {
                base.Game.GuessCharacter('z');
            }

            [Then]
            public void ExceptionIsThrown()
            {
                Assert.IsNotNull(base.Exception);
                Assert.IsInstanceOf<InvalidOperationException>(base.Exception);
            }

            [Then]
            public void DidNotLoseLife()
            {
                Assert.IsTrue(base.Game.Lives == 11);
            }

            [Then]
            public void GameStatusIsCorrect()
            {
                Assert.AreEqual(base.Game.Status, GameStatus.PlayerWon);
            }

            [Then]
            public void ListOfGuessedCharactersDoesNotIncludeGuess()
            {
                Assert.IsFalse(base.Game.GuessedCharacters.Contains('z'));
            }
        }

        public class MakeGuessAfterAlreadyLosing : GameSpecBase
        {
            public override void Given()
            {
                StartNewGame("hangman");

                base.Game.GuessCharacter('b');
                base.Game.GuessCharacter('c');
                base.Game.GuessCharacter('d');
                base.Game.GuessCharacter('e');
                base.Game.GuessCharacter('f');
                base.Game.GuessCharacter('i');
                base.Game.GuessCharacter('j');
                base.Game.GuessCharacter('k');
                base.Game.GuessCharacter('l');
                base.Game.GuessCharacter('o');
                base.Game.GuessCharacter('p');
            }

            public override void When()
            {
                base.Game.GuessCharacter('z');
            }

            [Then]
            public void ExceptionIsThrown()
            {
                Assert.IsNotNull(base.Exception);
                Assert.IsInstanceOf<InvalidOperationException>(base.Exception);
            }

            [Then]
            public void DidNotLoseLife()
            {
                Assert.IsTrue(base.Game.Lives == 0);
            }

            [Then]
            public void GameStatusIsCorrect()
            {
                Assert.AreEqual(base.Game.Status, GameStatus.PlayerLost);
            }

            [Then]
            public void ListOfGuessedCharactersDoesNotIncludeGuess()
            {
                Assert.IsFalse(base.Game.GuessedCharacters.Contains('z'));
            }
        }

        #endregion        
    }
}
