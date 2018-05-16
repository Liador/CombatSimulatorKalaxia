using System;
using System.Collections.Generic;
using System.Drawing;


namespace Simulator
{
    class BattleField
    {
        private List<Ship>[,] grid;
        private Army attackers;
        private Army defenders;
        private readonly int size;

        public List<Ship>[,] Grid { get => grid; set => grid = value; }
        public Army Attackers { get => attackers; set => attackers = value; }
        public Army Defenders { get => defenders; set => defenders = value; }
        public int Size { get => size; }

        public BattleField(int size,Army attackers, Army defenders)
        {
            Grid = new List<Ship>[size,size];
            Attackers = attackers;
            Defenders = defenders;
            this.size = size;
        }

        public BattleField(int size)
        {
            this.size = size;
            Grid = new List<Ship>[size, size];
        }

        public BattleField(BattleField battlefield)
        {
            int i, j, k;
            Ship shipTemp;
            size = battlefield.Size;
            Grid = new List<Ship>[size, size];
            Attackers = new Army(battlefield.Attackers);
            Defenders = new Army(battlefield.Defenders);
            Attackers.Ships = new List<Ship>();
            Defenders.Ships = new List<Ship>();
            for (i=0;i<size;i++)
            {
                for(j=0;j<size; j++)
                {
                    Grid[i, j] = new List<Ship>();
                    for(k=0;k<battlefield.Grid[i,j].Count;k++)
                    {
                        shipTemp = new Ship(battlefield.Grid[i, j][k]);
                        if(battlefield.Grid[i, j][k].ArmyName==Attackers.Name)
                        {
                            Attackers.Add(shipTemp);
                        }
                        else
                        {
                            Defenders.Add(shipTemp);
                        }
                        Grid[i,j].Add(shipTemp);
                    }
                }
            }
        }

        public void Init(Army attackers, Army defenders)
        {

            InitGrid();
            Attackers = attackers;
            Defenders = defenders;
            //Grid = new List<Ship>[size, size];
            PlaceAttackers();
            PlaceDefenders();

            //for (i = 0; i < Attackers.Ships.Count; i++)
            //{
            //    Grid[0, size / 2].Add(Attackers.Ships[i]);
            //}
            //Grid[size - 1, size / 2] = new List<Ship>();
            //for (i = 0; i < Defenders.Ships.Count; i++)
            //{
            //    Grid[size - 1, size / 2].Add(Defenders.Ships[i]);
            //}
        }

        private void PlaceAttackers()
        {
            int i, j, k;
            int offset;
            offset = size / 2 - Attackers.StartingLines / 2;
            for (i = 0; i < Attackers.StartingLines; i++)
            {
                for (j = 0; j < Attackers.StartingRows; j++)
                {
                    for (k = 0; k < Attackers.StartingGrid[i, j].Count; k++)
                    {
                        Grid[offset + i, j].Add(Attackers.StartingGrid[i, j][k]);
                    }
                }
            }
        }

        private void PlaceDefenders()
        {
            int i, j, k;
            int offset;
            offset = size / 2 - Defenders.StartingLines / 2;
            for (i = 0; i < Defenders.StartingLines; i++)
            {
                for (j = Defenders.StartingRows-1; j >=0; j--)
                {
                    for (k = 0; k < Defenders.StartingGrid[i, j].Count; k++)
                    {
                        Grid[offset + i, (size-1)-j].Add(Defenders.StartingGrid[i, j][k]);
                    }
                }
            }
        }

        private void InitGrid()
        {
            int i, j;
            for (i = 0; i < Size; i++)
            {
                for (j = 0; j < Size; j++)
                {
                    Grid[i, j] = new List<Ship>();
                }
            }
        }

        public Bitmap DrawBattleField(int bitmapSize, bool showDestroyedShips)
        {
            Bitmap bm = new Bitmap(bitmapSize, bitmapSize);
            Point p1, p2;
            int i, j, k, temp;
            int squareSize = bitmapSize/Size;
            int shipSize;
            int numberOfRows;
            using (Graphics g = Graphics.FromImage(bm))
            using (SolidBrush blackBrush = new SolidBrush(Color.DarkGray))
            using (SolidBrush whiteBrush = new SolidBrush(Color.White))
            using (Pen blackPen = new Pen(Color.DarkGray))
            using (Pen whitePen = new Pen(Color.White))
            using (Pen deadBluePen = new Pen(Color.Purple))
            using (Pen deadGreenPen = new Pen(Color.Brown))
            using (Pen aliveBluePen = new Pen(Color.LightSkyBlue))
            using (Pen aliveGreenPen = new Pen(Color.LightGreen))
            using (SolidBrush blackSolidBrush = new SolidBrush(Color.FromArgb(0xff,50,50,50)))
            using (SolidBrush deadBlueSolidBrush = new SolidBrush(Color.Purple))
            using (SolidBrush deadGreenSolidBrush = new SolidBrush(Color.Brown))
            using (SolidBrush aliveBlueSolidBrush = new SolidBrush(Color.LightSkyBlue))
            using (SolidBrush aliveGreenSolidBrush = new SolidBrush(Color.LightGreen))
            {
                Rectangle rect;
                rect = new Rectangle(0, 0, bitmapSize, bitmapSize);
                g.DrawRectangle(blackPen, rect);
                g.FillRectangle(blackSolidBrush, rect);


                //p1 = new Point(0, bitmapSize - 1);
                //p2 = new Point(bitmapSize - 1, bitmapSize - 1);
                //g.DrawLine(whitePen, p1, p2);

                //p1 = new Point(bitmapSize - 1, 0);
                //p2 = new Point(bitmapSize - 1, bitmapSize - 1);
                //g.DrawLine(whitePen, p1, p2);

                for (i = bitmapSize / squareSize; i >= 0; i--)
                {
                    p1 = new Point(0, i * squareSize);
                    p2 = new Point(bitmapSize, i * squareSize);
                    g.DrawLine(whitePen, p1, p2);
                }
                for (i = bitmapSize / squareSize; i >= 0; i--)
                {
                    p1 = new Point(i * squareSize, 0);
                    p2 = new Point(i * squareSize, bitmapSize);
                    g.DrawLine(whitePen, p1, p2);
                }
                for (i = 0; i < Size; i++)
                {
                    for (j = 0; j < Size; j++)
                    {
                        temp = Grid[i, j].Count;
                        if (temp > 0)
                        {
                            numberOfRows = temp / (int)Math.Floor(Math.Sqrt(temp));
                            for (k = 0; k < temp; k++)
                            {
                                shipSize = Grid[i, j][k].Type.Number*2 + 3;
                                if (Grid[i, j][k].ArmyName == Attackers.Name)
                                {
                                    //p1 = new Point(i * squareSize + squareSize / 2, j * squareSize + (k + 1) * (squareSize / (temp + 1) + 2));
                                    //p2 = new Point(i * squareSize + squareSize / 2, j * squareSize + (k + 1) * (squareSize / (temp + 1)));
                                    if (Grid[i, j][k].Alive)
                                    {
                                        rect = new Rectangle(i * squareSize + ((k + 1) % (temp / numberOfRows) + 1) * (squareSize / (numberOfRows + 1)), j * squareSize + (k / (temp / numberOfRows) + 1) * (squareSize / (numberOfRows + 2)), shipSize, shipSize);
                                        g.DrawRectangle(aliveGreenPen, rect);
                                        g.FillRectangle(aliveGreenSolidBrush, rect);
                                    }
                                    else
                                    {
                                        if (showDestroyedShips)
                                        {
                                            rect = new Rectangle(i * squareSize + ((k + 1) % (temp / numberOfRows) + 1) * (squareSize / (numberOfRows + 1)), j * squareSize + (k / (temp / numberOfRows) + 1) * (squareSize / (numberOfRows + 2)), shipSize, shipSize);
                                            g.DrawRectangle(aliveGreenPen, rect);
                                            //g.FillRectangle(deadGreenSolidBrush, rect);
                                        }
                                    }

                                }
                                else
                                {
                                    //p1 = new Point(i * squareSize + squareSize / 2, j * squareSize + (k + 1) * (squareSize / (temp + 1)) + 2);
                                    //p2 = new Point(i * squareSize + squareSize / 2, j * squareSize + (k + 1) * (squareSize / (temp + 1)));
                                    if (Grid[i, j][k].Alive)
                                    {
                                        rect = new Rectangle(i * squareSize + ((k + 1) % (temp / numberOfRows) + 1) * (squareSize / (numberOfRows + 1)), j * squareSize + (k / (temp / numberOfRows) + 1) * (squareSize / (numberOfRows + 2)), shipSize, shipSize);
                                        g.DrawRectangle(aliveBluePen, rect);
                                        g.FillRectangle(aliveBlueSolidBrush, rect);
                                    }
                                    else
                                    {
                                        if (showDestroyedShips)
                                        {
                                            rect= new Rectangle(i * squareSize + ((k + 1) % (temp / numberOfRows) + 1) * (squareSize / (numberOfRows + 1)), j * squareSize + (k / (temp / numberOfRows) + 1) * (squareSize / (numberOfRows + 2)), shipSize, shipSize);
                                            g.DrawRectangle(aliveBluePen, rect);
                                            //g.FillRectangle(deadBlueSolidBrush, rect);
                                        }
                                    }
                                }
                            }
                        }

                    }
                }
                return bm;
            }
        }

        public override string ToString()
        {
            string retval = "";
            int i, j;
            int number;
            for (i=0; i<Size;i++)
            {
                for(j=0; j<Size;j++)
                {
                    number = 0;
                    foreach(Ship s in Grid[i,j])
                    {
                        if(s.Alive)
                        {
                            number++;
                        }
                    }
                    retval += number.ToString("D2")+ " ";
                }
                retval += "\n";
            }
            return retval;
        }
    }
}