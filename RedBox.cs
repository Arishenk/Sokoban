using Sokoban;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    public class RedBox : ICreature
    {
        public string GetImageFileName
        {
            get { return "RedBox.png"; }
        }

        public int GetDrawingPriority
        {
            get { return 4; }
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
            var command = new CreatureCommand();
            return command;
        }

        private void CountScores(ICreature conflictedObject)
        {
            if (conflictedObject is GreenBox)
            {
                Game.Scores += 1;
                if (Game.Scores == Game.CountScores)
                {
                    Game.IsOver = true;
                }
            }
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject == null)
            {
                throw new ArgumentNullException("conflictedObject");
            }
            CountScores(conflictedObject);
            return conflictedObject is GreenBox;
        }
    }
}
