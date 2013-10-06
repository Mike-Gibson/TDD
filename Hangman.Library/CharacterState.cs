
namespace Hangman.Library
{
    public struct CharacterState
    {
        public char Character { get; private set; }
        public bool Guessed { get; private set; }

        public CharacterState(char character, bool guessed)
            : this()
        {
            this.Character = character;
            this.Guessed = guessed;
        }
    }
}