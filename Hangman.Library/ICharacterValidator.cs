
namespace Hangman.Library
{
    public interface ICharacterValidator
    {
        bool CharacterValid(char c);

        bool WordValid(string word);
    }
}