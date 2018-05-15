﻿using System.Collections.Generic;
namespace Simulator
{
    class Army
    {
        private static int numberOfArmies = 0; //used to give a unique ID to all the ships
        private string name;
        private int id;
        private List<Ship> ships;
        private int shipsAlive;
        private bool victory;

        public List<Ship> Ships { get => ships; set => ships = value; }
        public int ShipsAlive { get => shipsAlive; }
        public bool Victory { get => victory; set => victory = value; }
        public string Name { get => name; set => name = value; }
        public int Id { get => id; }

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

        public Army(Army army)
        {
            id = army.Id;
            Ships = new List<Ship>();
            this.Ships = army.Ships;
            shipsAlive = Ships.Count;
            Name = army.Name;
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

        public void ClaimVictory()
        {
            Victory = true;
        }

        public override string ToString()
        {
            int i;
            string retval;
            retval = "nbr ships alive :"+ShipsAlive+"\n";
            if (Simulation.numberOfBattles == 1)
                for (i = 0; i < Ships.Count; i++)
                {
                    retval += Ships[i].ToString() + "\n";
                }
            return retval;
        }
    }
}