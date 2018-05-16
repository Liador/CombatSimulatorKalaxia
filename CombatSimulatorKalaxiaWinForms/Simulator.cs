using System;
using System.Collections.Generic;
namespace Simulator
{
    class Simulation
    {
        public static int numberOfBattles = 10;
        public static bool testMode = false;
        public static bool fightOnBattleField = true;
        public static int armySize = 10;
        public static int battleFieldSize=5;
        public static Army army1;
        public static Army army2;
        public static List<ShipType> shipTypes;
        public static List<ShipSubType> subTypesList;
        public static List<Ship> shipList;
        public static List<Weapon> weaponList;
        private static Battle battle;
        public static int Army1WinnerCounter = 0;
        public static int Army2WinnerCounter = 0;
        private static int startingGridRows = 1;
        private static int startingGridLines = 1;

        public Battle Battle { get => battle; }
        public static int StartingGridRows { get => startingGridRows; set => startingGridRows = value; }
        public static int StartingGridLines { get => startingGridLines; set => startingGridLines = value; }

        public Simulation(int numberOfBat, bool fightOnBattleF, int sizeOfArmy, int battlefieldSize, int startingGridLines, int startingGridRows)
        {
            numberOfBattles = numberOfBat;
            fightOnBattleField = fightOnBattleF;
            armySize = sizeOfArmy;
            battleFieldSize = battlefieldSize;
            StartingGridLines = startingGridLines;
            StartingGridRows = startingGridRows;
            Init();
        }

        private static void Init()
        {
            shipTypes = new List<ShipType>();
            AddShipTypes(shipTypes);

            subTypesList = new List<ShipSubType>();
            AddSubTypes(subTypesList, shipTypes);

            weaponList = new List<Weapon>();
            AddWeapons(weaponList, shipTypes);
        }

        public Battle PlayOneTurn()
        {
            int i;
            var watch = System.Diagnostics.Stopwatch.StartNew();
            for (i = 0; i < numberOfBattles; i++)
            {
                //Thread.Sleep(15);//jusqu'à ce que je trouve un moyen d'avoir des randoms différents aussi vite
                shipList = new List<Ship>();

                CreateShips(shipList, armySize, shipTypes, subTypesList, weaponList, i);

                army1 = new Army()
                {
                    Name = "Blue"
                };

                army2 = new Army()
                {
                    Name = "Green"
                };


                FillArmys(army1, army2, shipList);

                battle = new Battle(army1, army2, battleFieldSize);
                watch.Restart();
                Battle.Start();
                watch.Stop();
                if (Battle.Winner == army1.Name)
                {
                    Army1WinnerCounter++;
                }
                else
                {
                    Army2WinnerCounter++;
                }
            }
            return Battle;
        }

        public Battle Play()
        {
            int i;
            var watch = System.Diagnostics.Stopwatch.StartNew();
            for (i = 0; i < numberOfBattles; i++)
            {
                //Thread.Sleep(15);//jusqu'à ce que je trouve un moyen d'avoir des randoms différents aussi vite
                shipList = new List<Ship>();

                CreateShips(shipList, armySize, shipTypes, subTypesList, weaponList, i);

                army1 = new Army()
                {
                    Name = "Blue"
                };

                army2 = new Army()
                {
                    Name = "Green"
                };


                FillArmys(army1, army2, shipList);
                army1.PlaceShips(StartingGridLines,StartingGridRows);
                army2.PlaceShips(StartingGridLines, startingGridRows);

                battle = new Battle(army1, army2, battleFieldSize);
                watch.Restart();
                Battle.Start();
                Battle.Play();
                watch.Stop();
                if (Battle.Winner == army1.Name)
                {
                    Army1WinnerCounter++;
                }
                else
                {
                    Army2WinnerCounter++;
                }
            }
            return Battle;
        }

        private static void FillArmys(Army army1, Army army2, List<Ship> shipList)
        {
            int i;
            for (i = 0; i < shipList.Count; i++)
            {
                //if(i%99<=51)
                if(i%2<=0)
                {
                    army1.Add(shipList[i]);
                }
                else
                {
                    army2.Add(shipList[i]);
                }
            }
        }

        private static void CreateShips(List<Ship> shipList,int number, List<ShipType> shipTypes, List<ShipSubType> subTypeList, List<Weapon> weaponList, int numberOfGeneration)
        {
            int j;
            Ship s;
            Random rand;
            rand = new Random((int)DateTime.Now.Ticks+new Random(numberOfGeneration).Next());
            int randInd;
            randInd = rand.Next();

            for (j=0; j<number; j++)
            {
                //List<Weapon> weapons;
                //int i;
                //weapons = new List<Weapon>();
                //for (i=0;i<200;i++)
                //{
                //    weapons.Add(weaponList[0]);
                //}
                randInd=rand.Next(shipTypes.Count);
                if(shipTypes[randInd].Number==0)
                {
                    s = new Ship(550, 100, 150, new Weapon[] { weaponList[0], weaponList[0]}, shipTypes[randInd], subTypeList[0]);
                }
                else
                {
                    if (shipTypes[randInd].Number ==1)
                    {
                        s = new Ship(750, 200, 80, new Weapon[] { weaponList[1], weaponList[0] }, shipTypes[randInd], subTypeList[0]);
                    }
                    else
                    {
                        s = new Ship(750, 200, 100, new Weapon[] { weaponList[2], weaponList[2] }, shipTypes[1], subTypeList[1]);
                    }
                }
                shipList.Add(s);
            }
        }

        private static void AddShipTypes(List<ShipType> shipTypes)
        {
            shipTypes.Add(new ShipType("Light", 0));
            shipTypes.Add(new ShipType("Medium", 1));
            shipTypes.Add(new ShipType("Heavy", 2));
        }

        private static void AddSubTypes(List<ShipSubType> shipSubTypes, List<ShipType> types)
        {
            List<ShipType> favoriteTargets;
            favoriteTargets = new List<ShipType>
            {
                types[0],
                types[1],
                types[2]
            };
            shipSubTypes.Add(new ShipSubType("interceptor", 50, types));

            favoriteTargets = new List<ShipType>()
            {
                types[1],
                types[0],
                types[2],
            };
            shipSubTypes.Add(new ShipSubType("light fighter", 51, types));

            favoriteTargets = new List<ShipType>
            {
                types[1],
                types[2],
                types[0]
            };
            shipSubTypes.Add(new ShipSubType("heavy fighter", 52, types));

            favoriteTargets = new List<ShipType>
            {
                types[2],
                types[1],
                types[0]
            };
            shipSubTypes.Add(new ShipSubType("bomber", 53, types));

        }

        private static void AddWeapons(List<Weapon> weaponList, List<ShipType> shipTypes)
        {
            Weapon weapon;
            weapon = new Weapon("light laser cannon", "Laser", Weapon.LASER, 75, 1,2, new ShipType[] { shipTypes[0], shipTypes[1], shipTypes[2] }, new int[] { 0, 1});
            weaponList.Add(weapon);
            weapon = new Weapon("medium laser cannon", "Laser", Weapon.LASER, 150, 1,2, new ShipType[] { shipTypes[1], shipTypes[0], shipTypes[2] },new int[1] { 1 });
            weaponList.Add(weapon);
            weapon = new Weapon("medium missile launcher", "Missile", Weapon.MISSILE, 150, 2,1, new ShipType[] { shipTypes[1], shipTypes[0], shipTypes[2] }, new int[1] { 1 });
            weaponList.Add(weapon);
        }
    }
}