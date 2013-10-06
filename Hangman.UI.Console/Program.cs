using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hangman.Library;
using Hangman.UI.Console.LifePoints;

namespace Hangman.UI.Console
{
    class Program
    {
        public static void Main(string[] args)
        {
            // Create the game using input from player 1
            Game game = CreateGame();
            
            // Clear screen and start guesses
            System.Console.Clear();
            System.Console.WriteLine("The word has been set. The game is afoot..");
            System.Console.WriteLine();
            System.Console.WriteLine("Press any key to start guessing.");
            System.Console.ReadKey();

            PlayGame(game);

            System.Console.WriteLine();
            System.Console.WriteLine("Thanks for playing.");
            System.Console.WriteLine("Press any key to exit.");
            System.Console.ReadKey();
        }

        public static Game CreateGame()
        {
            Game game = null;
            bool validWord = false;

            System.Console.Clear();            

            while (!validWord)
            {
                System.Console.WriteLine();
                System.Console.WriteLine("Player 1, enter a word for Player 2 to guess");
                System.Console.WriteLine();
                string word = System.Console.ReadLine();

                try
                {
                    game = new Game(word, new CharacterValidator());
                    validWord = true;
                }
                catch (ArgumentNullException ane)
                {
                    System.Console.WriteLine("Invalid word: " + ane.Message);
                }
                catch (ArgumentException ae)
                {
                    System.Console.WriteLine("Invalid word: " + ae.Message);
                }
            }

            return game;
        }

        public static void PlayGame(Game game)
        {
            string lastMessage = null;

            while (true)
            {
                System.Console.Clear();

                // Ouput last guess
                System.Console.WriteLine(lastMessage);
                System.Console.WriteLine();

                // Output word progress
                OutputWordProgress(game);
                
                // Output guessed words
                OutputGuessedCharacters(game);

                // Output lives
                OutputLives(game);
                                
                // Check game result
                if (game.Status == GameStatus.PlayerWon)
                {
                    using (new ConsoleColouring(ConsoleColor.Green))
                    {
                        System.Console.WriteLine();
                        System.Console.WriteLine("Congratulations! You won!");
                    }
                    break;
                }

                if (game.Status == GameStatus.PlayerLost)
                {
                    var o = System.Console.ForegroundColor;
                    System.Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine();
                    System.Console.WriteLine("You lost :(");
                    System.Console.ForegroundColor = o;
                    break;
                }

                // Get next guess
                System.Console.WriteLine();
                System.Console.WriteLine("Enter your guess:");
                char guess = System.Console.ReadKey().KeyChar;

                GuessResult guessResult = game.GuessCharacter(guess);

                switch (guessResult)
                {
                    case GuessResult.Correct:
                        lastMessage = string.Format("Well done, you guessed the character '{0}' correctly.", guess);
                        break;

                    case GuessResult.Incorrect:
                        lastMessage = string.Format("Unlucky, the guess '{0}' is incorrect", guess);
                        break;

                    case GuessResult.Invalid:
                        lastMessage = string.Format(
                            "The guess '{0}' is not valid. You may have already guessed that character, or it is not a valid character", 
                            guess);
                        break;

                    default:
                        throw new NotSupportedException();
                }
            }
        }

        private static void OutputLives(Game game)
        {
            ConsoleColor colour = System.Console.ForegroundColor;

            if (game.Lives < 7)
            {
                colour = ConsoleColor.DarkYellow;
            }

            if (game.Lives < 4)
            {
                colour = ConsoleColor.Red;
            }
            
            // Man
            
            //  __________    
            //  | /       |   
            //  |/        |   
            //  |         ()  
            //  |        /||\ 
            //  |       / || \
            //  |         /\  
            //  |        /  \ 
            //  |                         
            //__|_____________

            LifePointManager mgr = new LifePointManager();

            for (int y = 0; y < 10; y++)
            {
                System.Console.WriteLine();
                System.Console.Write(" ");

                for (int x = 0; x < 16; x++)
                {
                    using (new ConsoleColouring(colour))
                    {
                        System.Console.Write(mgr.GetCharacter(game.Lives, x, y));
                    }
                }
            }
                        
            // Numeric
            System.Console.WriteLine();
            System.Console.WriteLine();
            System.Console.Write("Lives remaining: ");
            
            using (new ConsoleColouring(colour))
            {
                System.Console.Write(game.Lives);
            }

            System.Console.WriteLine();
        }

        public static void OutputWordProgress(Game game)
        {
            IEnumerable<CharacterState> progress = game.GetCurrentState();

            System.Console.WriteLine();

            foreach (var c in progress)
            {
                System.Console.Write(" ");
                if (c.Guessed)
                {
                    System.Console.Write(c.Character);
                }
                else
                {
                    System.Console.Write("_");
                }
            }

            System.Console.WriteLine();
        }

        public static void OutputGuessedCharacters(Game game)
        {
            IEnumerable<char> guesses = game.GuessedCharacters;

            System.Console.WriteLine();

            if (guesses.Count() > 0)
            {
                System.Console.Write("You have guessed: ");
            }

            foreach (var c in guesses)
            {
                System.Console.Write(" ");
                System.Console.Write(c);
            }

            System.Console.WriteLine();
        }
    }

    public class ConsoleColouring : IDisposable
    {
        private ConsoleColor _original;

        public ConsoleColouring(System.ConsoleColor colour)
        {
            _original = System.Console.ForegroundColor;

            System.Console.ForegroundColor = colour;
        }
        
        public void Dispose()
        {
            System.Console.ForegroundColor = _original;

        }
    }
}
