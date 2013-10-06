using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman.UI.Console.LifePoints
{
    // Head
    public class LifePoint06 : LifePointBase
    {
        public override int LifeNumber { get { return 6; } }

        protected override char? GetCharacterInternal(int x, int y)
        {
            if (y == 2)
            {
                if (x == 12)
                {
                    return '(';
                }

                if (x == 13)
                {
                    return ')';
                }
            }

            return null;
        }
    }
}
