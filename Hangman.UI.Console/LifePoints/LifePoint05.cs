using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman.UI.Console.LifePoints
{
    // Noose
    public class LifePoint05 : LifePointBase
    {
        public override int LifeNumber { get { return 5; } }

        protected override char? GetCharacterInternal(int x, int y)
        {
            if (y == 1 && x == 12)
            {
                return '|';
            }

            return null;
        }
    }
}
