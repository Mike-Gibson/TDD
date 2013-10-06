using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman.UI.Console.LifePoints
{
    // Right arm
    public class LifePoint11 : LifePointBase
    {
        public override int LifeNumber { get { return 11; } }

        protected override char? GetCharacterInternal(int x, int y)
        {
            if ((y == 3 && x == 14) || (y == 4) && (x == 15))
            {
                return '\\';
            }

            return null;
        }
    }
}
