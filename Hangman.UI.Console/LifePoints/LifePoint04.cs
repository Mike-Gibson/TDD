using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman.UI.Console.LifePoints
{
    // Top beam
    public class LifePoint04 : LifePointBase
    {
        public override int LifeNumber { get { return 4; } }

        protected override char? GetCharacterInternal(int x, int y)
        {
            if (y == 0 && x > 1 && x < 12)
            {
                return '_';
            }

            return null;
        }
    }
}
