using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman.UI.Console.LifePoints
{
    // Left leg
    public class LifePoint08 : LifePointBase
    {
        public override int LifeNumber { get { return 8; } }

        protected override char? GetCharacterInternal(int x, int y)
        {
            if ((y == 5 && x == 12) || (y == 6 && x == 11))
            {
                return '/';
            }

            return null;
        }
    }
}
