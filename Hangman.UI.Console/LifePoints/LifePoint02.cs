using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman.UI.Console.LifePoints
{
    // Pole
    public class LifePoint02 : LifePointBase
    {
        public override int LifeNumber { get { return 2; } }

        protected override char? GetCharacterInternal(int x, int y)
        {
            if (x == 2 && y != 0)
            {
                return '|';
            }

            return null;
        }
    }
}
