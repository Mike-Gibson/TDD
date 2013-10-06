using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman.UI.Console.LifePoints
{
    // Left arm
    public class LifePoint10 : LifePointBase
    {
        public override int LifeNumber { get { return 10; } }

        protected override char? GetCharacterInternal(int x, int y)
        {
            if ((y == 3 && x == 11) || (y == 4) && (x == 10))
            {
                return '/';
            }

            return null;
        }
    }
}
