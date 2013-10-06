using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman.UI.Console.LifePoints
{
    // Body
    public class LifePoint07 : LifePointBase
    {
        public override int LifeNumber { get { return 7; } }

        protected override char? GetCharacterInternal(int x, int y)
        {
            if ((y == 3 || y == 4) && (x == 12 || x == 13))
            {
                return '|';
            }

            return null;
        }
    }
}
