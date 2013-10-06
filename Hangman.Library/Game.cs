using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman.Library
{
    public class Game
    {
        #region Member Variables

        private string _word;
        private ICharacterValidator _characterValidator;
        private List<char> _guessedCharacters;

        #endregion

        #region Properties

        /// <summary>
        /// Returns a sorted list of characters which have been guessed.
        /// </summary>
        public IEnumerable<char> GuessedCharacters
        {
            get { return _guessedCharacters.AsReadOnly(); }
        }

        /// <summary>
        /// Returns the current status of the game.
        /// </summary>
        public GameStatus Status { get; private set; }

        /// <summary>
        /// Number of lives that th eplayer has left.
        /// </summary>
        public int Lives { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new game with the provided word, using the provided validator.
        /// </summary>
        /// <param name="word">The word to use.</param>
        /// <param name="characterValidator">The class which validates the word and 
        /// guessed letters.</param>
        public Game(string word, ICharacterValidator characterValidator)
        {
            if (string.IsNullOrWhiteSpace(word))
            {
                throw new ArgumentNullException("word", "word cannot be empty");
            }

            if (characterValidator == null)
            {
                throw new ArgumentNullException("characterValidator");
            }

            if (!characterValidator.WordValid(word))
            {
                throw new ArgumentException("Word is not valid", "word");
            }

            _word = word;
            _characterValidator = characterValidator;
            _guessedCharacters = new List<char>();

            this.Status = GameStatus.InProgress;
            this.Lives = 11;
        }

        #endregion

        #region Instance Methods

        /// <summary>
        /// Guesses a character that appears in the word.
        /// </summary>
        /// <param name="c">Character to guess</param>
        /// <returns>Enum of the result of the guess.</returns>
        public GuessResult GuessCharacter(char c)
        {
            GuessResult result;

            if (this.Status != GameStatus.InProgress)
            {
                // Cannot guess when game is not in progress
                throw new InvalidOperationException(
                    string.Format(
                        "Cannot make a guess if the status is not {0}.",
                        GameStatus.InProgress));                        
            }

            if (!_characterValidator.CharacterValid(c))
            {
                // Not a valid charater, so deal with it
                result = GuessResult.Invalid;
            }
            else if (_guessedCharacters.Contains(c, StringComparison.InvariantCultureIgnoreCase))
            {
                // Already guessed this character
                result = GuessResult.Invalid;
            }
            else if (_word.Contains(c.ToString(), StringComparison.InvariantCultureIgnoreCase))
            {
                result = GuessResult.Correct;
            }
            else
            {
                result = GuessResult.Incorrect;

                // Result is incorrect so reduce number of lives
                this.Lives--;
            }

            // Add guess to the list, if not invalid
            if (result != GuessResult.Invalid)
            {
                _guessedCharacters.Add(c.ToString().ToLowerInvariant()[0]);
            }

            // See if player has won
            if (GetCurrentState().All(cs => cs.Guessed))
            {
                // Player won
                this.Status = GameStatus.PlayerWon;
            }

            // See if player has lost
            if (this.Lives == 0)
            {
                // Player lost
                this.Status = GameStatus.PlayerLost;
            }

            return result;
        }

        /// <summary>
        /// Returns the letters that are contained in the word and whether the player
        /// has guessed that letter.
        /// </summary>
        public IEnumerable<CharacterState> GetCurrentState()
        {
            List<CharacterState> result = new List<CharacterState>();

            foreach (char c in _word)
            {
                result.Add(new CharacterState(c, _guessedCharacters.Contains(c)));
            }

            return result;
        }

        #endregion
    }
}