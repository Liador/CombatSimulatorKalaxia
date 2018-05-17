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
        private int numberOfAliveShips;

        public List<Ship> ShipsList { get => shipsList;
            set
            {
                shipsList = value;
                NumberOfAliveShips = CountAliveShips();
                NumberOfDestroyedShips = shipsList.Count - NumberOfAliveShips;
            }
        }
        public int NumberOfDestroyedShips { get => numberOfDestroyedShips; set => numberOfDestroyedShips = value; }
        public int NumberOfAliveShips { get => numberOfAliveShips; set => numberOfAliveShips = value; }

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
        public int CountShipsFromArmy(string armyName)
        {
            int count = 0;
            foreach (Ship s in ShipsList)
            {
                if (armyName == s.ArmyName)
                {
                    count++;
                }
            }
            return count;
        }
        public int CountAliveShips()
        {
            int count = 0;
            foreach (Ship s in ShipsList)
            {
                if (s.Alive)
                {
                    count++;
                }
            }
            return count;
        }
        public int CountDestroyedShips()
        {
            int count = 0;
            foreach (Ship s in ShipsList)
            {
                if (!s.Alive)
                {
                    count++;
                }
            }
            return count;
        }
        public int CountAliveShips(string armyName)
        {
            int count = 0;
            foreach (Ship s in ShipsList)
            {
                if (armyName == s.ArmyName && s.Alive)
                {
                    count++;
                }
            }
            return count;
        }
        public int CountDestroyedShips(string armyName)
        {
            int count = 0;
            foreach (Ship s in ShipsList)
            {
                if (armyName == s.ArmyName && !s.Alive)
                {
                    count++;
                }
            }
            return count;
        }
        public int CountShipsOfType(ShipType type, string armyName)
        {
            int count = 0;
            foreach (Ship s in ShipsList)
            {
                if (armyName == s.ArmyName && s.Alive && s.Type.Number == type.Number)
                {
                    count++;
                }
            }
            return count;
        }
        public int CountShipsOfType(ShipType type, bool attackers)
        {
            int count = 0;
            foreach (Ship s in ShipsList)
            {
                if (s.Attacking==attackers && s.Alive && s.Type.Number == type.Number)
                {
                    count++;
                }
            }
            return count;
        }
        public int CountAttackingShips()
        {
            int count = 0;
            foreach(Ship s in ShipsList)
            {
                if(s.Attacking)
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
