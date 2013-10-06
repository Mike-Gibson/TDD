using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman.UI.Console.LifePoints
{
    // Support
    public class LifePoint03 : LifePointBase
    {
        public override int LifeNumber { get { return 3; } }

        protected override char? GetCharacterInternal(int x, int y)
        {
            if ((x == 3 && y == 2) || (x == 4 && y == 1))
            {
                return '/';
            }

            return null;
        }
    }
}
