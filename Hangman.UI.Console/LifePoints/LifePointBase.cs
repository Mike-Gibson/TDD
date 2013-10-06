using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman.UI.Console
{
    public abstract class LifePointBase
    {
        public abstract int LifeNumber { get; }

        public char? GetCharacter(int livesRemaining, int x, int y)
        {
            if (11 - livesRemaining >= this.LifeNumber)
            {
                return GetCharacterInternal(x, y);
            }
            else
            {
                return null;
            }
        }

        protected abstract char? GetCharacterInternal(int x, int y);
    }
}
