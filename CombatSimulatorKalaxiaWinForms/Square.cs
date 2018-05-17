using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator
{
    class Square
    {
        private List<Ship> shipsList;
        private int numberOfDestroyedShips;

        public List<Ship> ShipsList { get => shipsList; set => shipsList = value; }
        public int NumberOfDestroyedShips { get => numberOfDestroyedShips; set => numberOfDestroyedShips = value; }

        public Square()
        {
            shipsList = new List<Ship>();
            numberOfDestroyedShips = 0;
        }

        public Square(Square sq)
        {
            shipsList = sq.shipsList;
            numberOfDestroyedShips = sq.numberOfDestroyedShips;
        }
        public int CountShipsFromArmy(string armyname)
        {
            int count=0;
            foreach(Ship s in ShipsList)
            {
                if(armyname == s.ArmyName)
                {
                    count++;
                }
            }
            return count;
        }
        public override string ToString()
        {
            return shipsList.Count.ToString();
        }
    }
}
