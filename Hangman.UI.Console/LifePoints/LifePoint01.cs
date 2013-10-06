using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman.UI.Console.LifePoints
{
    // Base
    public class LifePoint01 : LifePointBase
    {
        public override int LifeNumber { get { return 1; } }

        protected override char? GetCharacterInternal(int x, int y)
        {
            if (y == 9)
            {
                return '_';
            }

            return null;
        }
    }
}
