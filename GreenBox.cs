
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sokoban
{
    public class GreenBox : ICreature
    {
        public string GetImageFileName
        {
            get { return "GreenBox.png"; }
        }

        public int GetDrawingPriority
        {
            get { return -1; }
        }

        static void CheckArguments(int x, int y)
        {
            if (x < 0 || x > Game.fieldWidth)
            {
                throw new ArgumentException("x");
            }

            if (y < 0 || y > Game.fieldHeight)
            {
                throw new ArgumentException("y");
            }
        }

        public CreatureCommand Act(int x, int y)
        {
            CheckArguments(x, y);
            return new CreatureCommand();
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject == null)
            {
                throw new ArgumentNullException("conflictedObject");
            }
            return false;
        }
    }
}