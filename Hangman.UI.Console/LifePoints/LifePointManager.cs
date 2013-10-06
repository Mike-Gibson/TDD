using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman.UI.Console.LifePoints
{
    public class LifePointManager
    {
        private List<LifePointBase> LifePoints { get; set; }

        public LifePointManager()
        {
            this.LifePoints = new List<LifePointBase>()
            {
                new LifePoint11(),
                new LifePoint10(),
                new LifePoint09(),
                new LifePoint08(),
                new LifePoint07(),
                new LifePoint06(),
                new LifePoint05(),
                new LifePoint04(),
                new LifePoint03(),
                new LifePoint02(),
                new LifePoint01(),                
            };
        }

        public char GetCharacter(int livesRemaining, int x, int y)
        {
            char? c = null;

            foreach (LifePointBase lpb in this.LifePoints)
            {
                c = lpb.GetCharacter(livesRemaining, x, y);

                if (c.HasValue)
                {
                    return c.Value;
                }
            }

            return ' ';
        }
    }
}
