using System.Collections.Generic;
using System.Drawing;

namespace Simulator
{
    class BattleField
    {
        private List<Ship>[,] grid;
        private Army attackers;
        private Army defenders;
        private int size;

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
            int i, j;
            for (i = 0; i < Size; i++)
            {
                for (j = 0; j < Size; j++)
                {
                    Grid[i, j] = new List<Ship>();
                }
            }
            Attackers = attackers;
            Defenders = defenders;
            //Grid = new List<Ship>[size, size];
            Grid[0, size/2] = new List<Ship>();


            for (i=0; i<Attackers.Ships.Count;i++)
            {
                Grid[0, size / 2].Add(Attackers.Ships[i]);
            }
            Grid[size - 1, size / 2] = new List<Ship>();
            for (i = 0; i < Defenders.Ships.Count; i++)
            {
                Grid[size-1, size/2].Add(Defenders.Ships[i]);
            }
        }

        public Bitmap DrawBattleField(int bitmapSize)
        {
            Bitmap bm = new Bitmap(bitmapSize, bitmapSize);
            Point p1, p2;
            int i, j, k, temp;
            int squareSize = bitmapSize/Size;
            int shipSize;
            using (Graphics g = Graphics.FromImage(bm))
            using (SolidBrush blackBrush = new SolidBrush(Color.Black))
            using (SolidBrush whiteBrush = new SolidBrush(Color.White))
            using (Pen blackPen = new Pen(Color.Black))
            using (Pen redPen = new Pen(Color.Blue))
            using (Pen greenPen = new Pen(Color.Green))
            using (Pen darkRedPen = new Pen(Color.DarkBlue))
            using (Pen darkGreenPen = new Pen(Color.DarkGreen))
            {
                p1 = new Point(0, bitmapSize - 1);
                p2 = new Point(bitmapSize - 1, bitmapSize - 1);
                g.DrawLine(blackPen, p1, p2);

                p1 = new Point(bitmapSize - 1, 0);
                p2 = new Point(bitmapSize - 1, bitmapSize - 1);
                g.DrawLine(blackPen, p1, p2);

                for (i = bitmapSize / squareSize; i >= 0; i--)
                {
                    p1 = new Point(0, i * squareSize);
                    p2 = new Point(bitmapSize, i * squareSize);
                    g.DrawLine(blackPen, p1, p2);
                }
                for (i = bitmapSize / squareSize; i >= 0; i--)
                {
                    p1 = new Point(i * squareSize, 0);
                    p2 = new Point(i * squareSize, bitmapSize);
                    g.DrawLine(blackPen, p1, p2);
                }
                for (i = 0; i < Size; i++)
                {
                    for (j = 0; j < Size; j++)
                    {
                        temp = Grid[i, j].Count;
                        for (k = 0; k < temp; k++)
                        {

                            if (Grid[i, j][k].ArmyName == Attackers.Name)
                            {
                                shipSize = Grid[i, j][k].Type.Number + 2;
                                //p1 = new Point(i * squareSize + squareSize / 2, j * squareSize + (k + 1) * (squareSize / (temp + 1) + 2));
                                //p2 = new Point(i * squareSize + squareSize / 2, j * squareSize + (k + 1) * (squareSize / (temp + 1)));
                                if(Grid[i, j][k].Alive)
                                {
                                    g.DrawRectangle(darkGreenPen, new Rectangle(i * squareSize + squareSize / 2, j * squareSize + (k + 1) * (squareSize / (temp + 1)), shipSize, shipSize));
                                }
                                else
                                {
                                    g.DrawRectangle(greenPen, new Rectangle(i * squareSize + squareSize / 2, j * squareSize + (k + 1) * (squareSize / (temp + 1)), shipSize, shipSize));
                                }
                                
                            }
                            else
                            {
                                //p1 = new Point(i * squareSize + squareSize / 2, j * squareSize + (k + 1) * (squareSize / (temp + 1)) + 2);
                                //p2 = new Point(i * squareSize + squareSize / 2, j * squareSize + (k + 1) * (squareSize / (temp + 1)));
                                if (Grid[i, j][k].Alive)
                                {
                                    g.DrawRectangle(darkRedPen, new Rectangle(i * squareSize + squareSize / 2, j * squareSize + (k + 1) * (squareSize / (temp + 1)), shipSize , shipSize));
                                }
                                else
                                {
                                    g.DrawRectangle(redPen, new Rectangle(i * squareSize + squareSize / 2, j * squareSize + (k + 1) * (squareSize / (temp + 1)), shipSize, shipSize));
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