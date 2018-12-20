using Sokoban;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sokoban
{
    public class Player : ICreature
    {
        public string GetImageFileName
        {
            get { return "Sokoban.png"; }
        }

        public int GetDrawingPriority
        {
            get { return 2; }
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

        static bool CellIsEmpty(int x, int y)
        {
            return Game.field[x, y] == null;
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
                        if (Game.field[x, y - 1] is Box)
                        {
                            if (PointInField(x, y - 2) && (Game.field[x, y - 2] == null || Game.field[x, y - 2] is RedBox))
                            {
                                command.DeltaY = -1;
                                Game.field[x, y - 1].Act(x, y - 1);
                            }
                        }
                        else if (CellIsEmpty(x, y - 1))
                        {
                            command.DeltaY = -1;
                        }
                    }
                    break;
                case Keys.Down:
                    if (PointInField(x, y + 1))
                    {
                        if (Game.field[x, y + 1] is Box)
                        {
                            if (PointInField(x, y + 2) && (Game.field[x, y + 2] == null || Game.field[x, y + 2] is RedBox))
                            {
                                command.DeltaY = 1;
                                Game.field[x, y + 1].Act(x, y + 1);
                            }
                        }
                        else if (CellIsEmpty(x, y + 1))
                        {
                            command.DeltaY = 1;
                        }

                    }
                    break;
                case Keys.Right:
                    if (PointInField(x + 1, y))
                    {
                        if (Game.field[x + 1, y] is Box)
                        {
                            if (PointInField(x + 2, y) && (Game.field[x + 2, y] == null || Game.field[x + 2, y] is RedBox))
                            {
                                command.DeltaX = 1;
                                Game.field[x + 1, y].Act(x + 1, y);
                            }
                        }
                        else if (CellIsEmpty(x + 1, y))
                        {
                            command.DeltaX = 1;
                        }

                    }
                    break;
                case Keys.Left:
                    if (PointInField(x - 1, y))
                    {
                        if (Game.field[x - 1, y] is Box)
                        {
                            if (PointInField(x - 2, y) && (Game.field[x - 2, y] == null || Game.field[x - 2, y] is RedBox))
                            {
                                command.DeltaX = -1;
                                Game.field[x - 1, y].Act(x - 1, y);
                            }
                        }
                        else if (CellIsEmpty(x - 1, y))
                        {
                            command.DeltaX = -1;
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
