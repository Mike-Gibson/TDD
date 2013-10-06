using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman.UI.Console.LifePoints
{
    // Right leg
    public class LifePoint09 : LifePointBase
    {
        public override int LifeNumber { get { return 9; } }

        protected override char? GetCharacterInternal(int x, int y)
        {
            if ((y == 5 && x == 13) || (y == 6 && x == 14))
            {
                return '\\';
            }

            return null;
        }
    }
}
