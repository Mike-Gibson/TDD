using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman.Library.Test
{
    public abstract class GameSpecBase : SpecBase
    {
        protected Game Game { get; set; }

        protected void StartNewGame(string word)
        {
            this.Game = new Game(word, new CharacterValidator());
        }
    }
}
