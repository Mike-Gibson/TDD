using System;

namespace Hangman.Library
{
    public class CharacterValidator : ICharacterValidator
    {
        public bool CharacterValid(char c)
        {
            return char.IsLetter(c);
        }

        public bool WordValid(string word)
        {
            if (string.IsNullOrEmpty(word))
            {
                throw new ArgumentNullException("word");
            }

            foreach (char c in word)
            {
                if (!CharacterValid(c))
                {
                    return false;
                }
            }

            return true;
        }
    }
}