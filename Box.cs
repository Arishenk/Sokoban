using Sokoban;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sokoban
{
    public class Box : ICreature
    {
        public string GetImageFileName
        {
            get { return "Box.png"; }
        }

        public int GetDrawingPriority
        {
            get { return 1; }
        }

        static bool PointInField(int x, int y)
        {
            return y >= 0 && y < Game.fieldHeight && x < Game.fieldWidth && x >= 0;
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
            var keyPressed = Game.KeyPressed;
            var command = new CreatureCommand();
            switch (keyPressed)
            {
                case Keys.Up:
                    if (PointInField(x, y - 1))
                    {
                        if (PointInField(x, y + 1))
                        {
                            if (Game.field[x, y + 1] is Player && Game.field[x, y - 1] == null)
                            {
                                command.DeltaY = -1;
                            }
                            if (Game.field[x, y + 1] is Player && Game.field[x, y - 1] is RedBox)
                            {
                                command.DeltaY = -1;
                                command.TransformTo = new GreenBox();
                            }
                        }
                    }
                    break;
                case Keys.Down:
                    if (PointInField(x, y + 1))
                    {
                        if (PointInField(x, y - 1))
                        {
                            if (Game.field[x, y - 1] is Player && Game.field[x, y + 1] == null)
                            {
                                command.DeltaY = 1;
                            }
                            if (Game.field[x, y - 1] is Player && Game.field[x, y + 1] is RedBox)
                            {
                                command.DeltaY = 1;
                                command.TransformTo = new GreenBox();
                            }
                        }
                    }
                    break;
                case Keys.Right:
                    if (PointInField(x + 1, y))
                    {
                        if (PointInField(x - 1, y))
                        {
                            if (Game.field[x - 1, y] is Player && Game.field[x + 1, y] == null)
                            {
                                command.DeltaX = 1;
                            }
                            if (Game.field[x - 1, y] is Player && Game.field[x + 1, y] is RedBox)
                            {
                                command.DeltaX = 1;
                                command.TransformTo = new GreenBox();
                            }
                        }
                    }
                    break;
                case Keys.Left:
                    if (PointInField(x - 1, y))
                    {
                        if (PointInField(x + 1, y))
                        {
                            if (Game.field[x + 1, y] is Player && Game.field[x - 1, y] == null)
                            {
                                command.DeltaX = -1;
                            }
                            if (Game.field[x + 1, y] is Player && Game.field[x - 1, y] is RedBox)
                            {
                                command.DeltaX = -1;
                                command.TransformTo = new GreenBox();
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
            return command;
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
