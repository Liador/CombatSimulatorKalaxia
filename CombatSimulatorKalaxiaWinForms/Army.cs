using System;
using System.Collections.Generic;
namespace Simulator
{
    class Army
    {
        private static int numberOfArmies = 0; //used to give a unique ID to all the ships
        private string name;
        private readonly int id;
        private List<Ship> ships;
        private int shipsAlive;
        private bool victory;
        private List<Ship>[,] startingGrid;
        private int startingLines;
        private int startingRows;
        private bool attacking;


        public List<Ship> Ships { get => ships; set => ships = value; }
        public int ShipsAlive { get => shipsAlive; }
        public bool Victory { get => victory; set => victory = value; }
        public string Name { get => name; set => name = value; }
        public int Id { get => id; }
        public List<Ship>[,] StartingGrid { get => startingGrid; set => startingGrid = value; }
        public int StartingLines { get => startingLines; set => startingLines = value; }
        public int StartingRows { get => startingRows; set => startingRows = value; }
        public bool Attacking { get => attacking; set => attacking = value; }

        public Army()
        {
            id = numberOfArmies;
            numberOfArmies++;
            shipsAlive = 0;
            Ships = new List<Ship>();
            
        }

        public Army(Ship[] ships)
        {
            id = numberOfArmies;
            numberOfArmies++;
            Ships = new List<Ship>();
            foreach (Ship s in ships)
            {
                Ships.Add(s);
            }
            shipsAlive = Ships.Count;
        }

        public Army(List<Ship> ships)
        {
            id = numberOfArmies;
            numberOfArmies++;
            Ships = new List<Ship>();
            foreach (Ship s in ships)
            {
                Ships.Add(s);
            }
            shipsAlive = Ships.Count;
        }

        public Army(Army army)//creates a copy of the army (same ID)
        {
            id = army.Id;
            Ships = new List<Ship>();
            this.Ships = army.Ships;
            shipsAlive = Ships.Count;
            Name = army.Name;
            StartingRows = army.StartingRows;
            startingLines = army.startingLines;
            Attacking = army.attacking;
        }

        public void Add(Ship ship)
        {
            ship.ArmyName = Name;
            ship.ArmyId = Id;
            Ships.Add(ship);
            shipsAlive++;
        }

        public void CountShipsAlive()
        {
            shipsAlive = 0;
            foreach (Ship s in Ships)
            {
                if(s.Alive)
                {
                    shipsAlive++;
                }
            }
        }

        public void Move(int battleTime)
        {
            int i;
            for(i=0;i<Ships.Count;i++)
            {
                if(Ships[i].Alive)
                {
                    if (Ships[i].BlueShield <= (int)(Ships[i].MaxBlueShield * 0.9))
                    {
                        Ships[i].BlueShield = Ships[i].BlueShield + (int)(Ships[i].MaxBlueShield * 0.1);
                    }
                    else
                    {
                        Ships[i].BlueShield = Ships[i].MaxBlueShield;
                    }
                }
            }
        }

        public void Fight(int battleTime, Army ennemy)
        {
            int leftToFire;
            int i, j, k, l;
            if(ennemy.ShipsAlive>0)
            {
                for (i = 0; i < Ships.Count; i++)
                {
                    if(Ships[i].Alive)
                    {
                        for (j = 0; j < Ships[i].Weapons.Count; j++)
                        {
                            
                            for (k = 0; k < Ships[i].SubType.FavoriteTargets.Count; k++)
                            {
                                leftToFire = Ships[i].Weapons[j].NumberOfShots;
                                for (l = 0; l < ennemy.Ships.Count && leftToFire > 0; l++)
                                {
                                    if (ennemy.Ships[l].Alive && leftToFire>0)
                                    {
                                        if (ennemy.ships[l].Type.Number == Ships[i].SubType.FavoriteTargets[k].Number)
                                        {
                                            while (leftToFire > 0 && ennemy.Ships[l].Alive)
                                            {
                                                if (Simulation.numberOfBattles == 1)
                                                {
                                                    if (!ennemy.Ships[l].TakeDamage(Ships[i].Weapons[j]))
                                                    {
                                                        //ship destroyed
                                                    }
                                                }
                                                else
                                                {
                                                    ennemy.Ships[l].TakeDamage(Ships[i].Weapons[j]);
                                                }
                                                leftToFire--;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                ennemy.CountShipsAlive();
                
                if (ennemy.ShipsAlive<=0)
                {
                    this.ClaimVictory();
                }
            }
            else
            {
                //victory
            }
        }

        public void PlaceShips( int lines, int rows)
        {
            int i, j;
            StartingLines = lines;
            StartingRows = rows;
            InitStartingGrid();
            i = 0;
            j = 0;
            foreach(Ship s in Ships)
            {
                StartingGrid[i, j].Add(s);
                i++;
                if (i >= StartingLines)
                {
                    j++;
                    i = 0;
                    if (j >= StartingRows)
                    {
                        j = 0;
                    }
                }
            }
        }

        private void InitStartingGrid()
        {
            int i, j;
            StartingGrid = new List<Ship>[StartingLines, StartingRows];
            for (i = 0; i < StartingLines; i++)
            {
                for (j = 0; j < StartingRows; j++)
                {
                    StartingGrid[i, j] = new List<Ship>();
                }
            }
        }

        public void ClaimVictory()
        {
            Victory = true;
        }

        public void IsAttacker()
        {
            Attacking = true;
            foreach(Ship s in Ships)
            {
                s.Attacking = true;
            }
        }

        public void IsDefender()
        {
            Attacking = false;
            foreach(Ship s in Ships)
            {
                s.Attacking = false;
            }
        }

        public override string ToString()
        {
            //int i;
            string retval;
            retval = "nbr ships alive :"+ShipsAlive+"\n";
            //if (Simulation.numberOfBattles == 1)
            //    for (i = 0; i < Ships.Count; i++)
            //    {
            //        retval += Ships[i].ToString() + "\n";
            //    }
            return retval;
        }
    }
}