using System;
using System.Collections.Generic;
namespace Simulator
{
    class Ship : IComparable
    {
        private static int numberOfShips = 0; //used to give a unique ID to all the ships
        private int id;
        private ShipType type;
        private ShipSubType subType;
        private int health;
        private int maxHealth;
        //private int durability;
        //private int maxDurability;
        private int blueShield;
        private int maxBlueShield;
        //private int greyShield;
        //private int maxGreyShield;
        private int moral;
        private int moveSpeed;
        private int initiative;
        private List<Weapon> weapons;
        private readonly int[] leftToFire;
        private bool alive;
        private List<Ship> hangar;
        private string armyName;
        private int armyId;
        private int dodgedMissiles;
        private int firedMissiles;
        private int movementLeft;
        private bool attacking;

        public int Health
        {
            get
            {
                return health;
            }
            set
            {
                health = value;
            }
        }

        public int BlueShield
        {
            get
            {
                return blueShield;
            }
            set
            {
                blueShield = value;
            }
        }

        public int MoveSpeed
        {
            get
            {
                return moveSpeed;
            }
            set
            {
                moveSpeed = value;
            }
        }

        public List<Weapon> Weapons
        {
            get
            {
                return weapons;
            }
            set
            {
                weapons = value;
            }
        }

        public ShipType Type { get => type; set => type = value; }
        public int MaxBlueShield { get => maxBlueShield; set => maxBlueShield = value; }
        public int MaxHealth { get => maxHealth; set => maxHealth = value; }
        public bool Alive { get => alive; set => alive = value; }
        public int ID { get => id; set => id = value; }
        internal List<Ship> Hangar { get => hangar; set => hangar = value; }
        public string ArmyName { get => armyName; set => armyName = value; }
        public int Initiative { get => initiative; set => initiative = value; }
        public ShipSubType SubType { get => subType; set => subType = value; }
        public int MovementLeft { get => movementLeft; set => movementLeft = value; }
        public int ArmyId { get => armyId; set => armyId = value; }
        public int Moral { get => moral; set => moral = value; }
        public bool Attacking { get => attacking; set => attacking = value; }

        public Ship(int hp, int blueShield, int moveSpeed, Weapon[] weapons, ShipType type, ShipSubType subType)
        {
            Health = hp;
            BlueShield = blueShield;
            MaxBlueShield = blueShield;
            MoveSpeed = moveSpeed;
            Weapons = new List<Weapon>();
            foreach (Weapon w in weapons)
            {
                Weapons.Add(w);
            }
            Type = type;
            SubType = subType;
            Alive = true;
            ID = numberOfShips;
            moral = 100;
            leftToFire = new int[Weapons.Count];
            Reload();
            numberOfShips++;

        }
        public Ship(int hp, int blueShield, int moveSpeed, List<Weapon> weapons, ShipType type, ShipSubType subType)
        {
            Health = hp;
            BlueShield = blueShield;
            MaxBlueShield = blueShield;
            MoveSpeed = moveSpeed;
            Weapons = new List<Weapon>();
            foreach (Weapon w in weapons)
            {
                Weapons.Add(w);
            }
            Type = type;
            SubType = subType;
            Alive = true;
            ID = numberOfShips;
            moral = 100;
            leftToFire = new int[Weapons.Count];
            Reload();
            numberOfShips++;
        }
        public Ship(Ship ship) //create a new ship based on the model of the ship in parameter
        {
            Health = ship.Health;
            BlueShield = ship.BlueShield;
            MaxBlueShield = BlueShield;
            MoveSpeed = ship.MoveSpeed;
            Weapons = ship.Weapons;
            Type = ship.Type;
            SubType = ship.SubType;
            Alive = ship.Alive;
            ID = numberOfShips;
            moral = ship.moral;
            leftToFire = new int[Weapons.Count];
            Reload();
            numberOfShips++;
        }
        public Ship(Ship ship, int id) //create a copy of the ship in parameter (same unique ID)
        {
            Health = ship.Health;
            BlueShield = ship.BlueShield;
            MaxBlueShield = BlueShield;
            MoveSpeed = ship.MoveSpeed;
            Weapons = ship.Weapons;
            Type = ship.Type;
            SubType = ship.SubType;
            Alive = ship.Alive;
            ID = ship.ID;
            moral = ship.moral;
            leftToFire = new int[Weapons.Count];
            Reload();
        }
        public void Refill()
        {
            movementLeft = Type.NumberOfMovement + subType.NumberOfMovement;
        }
        public void Reload()
        {
            int i;
            for (i = 0; i < weapons.Count; i++)
            {
                leftToFire[i] = weapons[i].NumberOfShots;
            }
        }
        public void Reload(int weaponIndice)
        {
            if (weaponIndice > weapons.Count)
            {
                leftToFire[weaponIndice] = weapons[weaponIndice].NumberOfShots;
            }
            else
            {
                throw new Exception("the weapon you want to reload doesn't exist");
            }

        }
        public bool TakeDamage(Weapon weapon)
        {
            switch (weapon.Type)
            {
                case Weapon.LASER:
                    if (weapon.Damage / 2 <= BlueShield)
                    {
                        BlueShield = BlueShield - weapon.Damage / 2;
                        Health = Health - weapon.Damage / 2;
                    }
                    else
                    {
                        Health = Health - weapon.Damage + BlueShield;
                        BlueShield = 0;
                    }
                    break;
                case Weapon.MISSILE:
                    firedMissiles++;
                    if (MoveSpeed <= 70)
                    {
                        if (BlueShield - weapon.Damage >= 0)
                        {
                            BlueShield = BlueShield - weapon.Damage;
                        }
                        else
                        {
                            Health = Health - weapon.Damage + BlueShield;
                            BlueShield = 0;
                        }
                    }
                    else
                    {
                        if (((new Random().Next()) % 100) >= MoveSpeed - 70)
                        {
                            if (BlueShield - weapon.Damage >= 0)
                            {
                                BlueShield = BlueShield - weapon.Damage;
                            }
                            else
                            {
                                Health = Health - weapon.Damage + BlueShield;
                                BlueShield = 0;
                            }
                        }
                        else
                        {
                            if (Simulation.numberOfBattles == 1)
                            {
                                //Console.WriteLine("The missile missed its target.\n");
                            }
                            dodgedMissiles++;
                        }
                    }
                    break;
                case Weapon.ION:
                    if (MoveSpeed <= 100)
                    {
                        if (BlueShield - weapon.Damage * 4 >= 0)
                        {
                            BlueShield = BlueShield - weapon.Damage * 4;
                        }
                        else
                        {
                            Health = Health - (BlueShield - weapon.Damage * 4) / 8;
                            BlueShield = 0;
                        }
                    }
                    else
                    {
                        if (((new Random().Next()) % 100) >= MoveSpeed - 100)
                        {
                            if (BlueShield - weapon.Damage * 4 >= 0)
                            {
                                BlueShield = BlueShield - weapon.Damage * 4;
                            }
                            else
                            {
                                Health = Health - (BlueShield - weapon.Damage * 4) / 8;
                                BlueShield = 0;
                            }
                        }
                        else
                        {
                            if (Simulation.numberOfBattles == 1)
                            {
                                //Console.WriteLine("The ion shot missed its target.\n");
                            }
                        }
                    }
                    break;
                case Weapon.EMP:
                    if (MoveSpeed <= 70)
                    {
                        BlueShield = 0;
                    }
                    else
                    {
                        if (((new Random().Next()) % 100) >= MoveSpeed - 70)
                        {
                            BlueShield = 0;
                        }
                        else
                        {
                            if (Simulation.numberOfBattles == 1)
                            {
                                //Console.WriteLine("The EMP shot missed its target.\n");
                            }
                        }
                    }
                    break;
                case Weapon.GAUSS:
                //break;
                default:
                    Health = Health - weapon.Damage;
                    break;
            }

            if (Health > 0)
            {
                Alive = true;
            }
            else
            {
                Alive = false;
            }
            return Alive;
        }

        public void Add(Ship ship)
        {
            Hangar.Add(ship);
        }

        public override string ToString()
        {
            string retval = "";
            if (Simulation.numberOfBattles == 1)
            {
                retval = "ID:" + ID + " " + Type.Name + " " + SubType.Name + " " + Health + "hp " + BlueShield + "sp " + MoveSpeed + " moveSpeed ";

                if (firedMissiles > 0)
                    retval += dodgedMissiles + "/" + firedMissiles + " missiles missed this ship";
            }

            //for (i = 0; i < weapons.length; i++)
            //{
            //    retval += weapons[i] + " ";
            //}
            //retval += "weapons";
            return retval;
        }

        public override bool Equals(object obj)
        {
            Ship s;
            s = (Ship)obj;
            return s.ID.Equals(ID);
        }

        public int CompareTo(object obj)
        {
            Ship s;
            if (obj == null)
            {
                return 1;
            }
            s = obj as Ship;
            if (s != null)
            {
                return ID.CompareTo(s.ID);
            }
            else
            {
                throw new ArgumentException("The object is not a Ship.");
            }
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public int Attack(BattleField field, int i, int j)
        {
            int m, k, l;
            int limI, limJ;
            int positionX = i;
            int positionY = j;

            for (m = 0; m < Weapons.Count; m++)
            {
                i = positionX - Weapons[m].Range;
                if (i < 0)
                    i = 0;
                j = positionY - weapons[m].Range;
                if (j < 0)
                    j = 0;
                limI = positionX + weapons[m].Range;
                if (limI > field.Size - 1)
                    limI = field.Size - 1;
                limJ = positionY + weapons[m].Range;
                if (limJ > field.Size - 1)
                    limJ = field.Size - 1;
                for (k = 0; k < SubType.FavoriteTargets.Count; k++)
                {
                    i = positionX - Weapons[m].Range;
                    if (i < 0)
                        i = 0;
                    while (i <= limI)
                    {
                        j = positionY - weapons[m].Range;
                        if (j < 0)
                            j = 0;
                        while (j <= limJ)
                        {
                            for (l = 0; l < field.Grid[i, j].ShipsList.Count && leftToFire[m] > 0; l++)
                            {
                                if (field.Grid[i, j].ShipsList[l].ArmyName != this.ArmyName && field.Grid[i, j].ShipsList[l].Alive && leftToFire[m] > 0)
                                {
                                    if (field.Grid[i, j].ShipsList[l].Type.Number == SubType.FavoriteTargets[k].Number)
                                    {
                                        while (leftToFire[m] > 0 && field.Grid[i, j].ShipsList[l].Alive)
                                        {
                                            if (Simulation.numberOfBattles == 1)
                                            {
                                                if (!field.Grid[i, j].ShipsList[l].TakeDamage(Weapons[m]))
                                                {
                                                    //ship destroyed
                                                }
                                            }
                                            else
                                            {
                                                field.Grid[i, j].ShipsList[l].TakeDamage(Weapons[m]);
                                            }
                                            leftToFire[m]--;
                                        }
                                    }
                                }
                            }
                            j++;
                        }
                        i++;
                    }
                }
            }
            if (m < weapons.Count)
            {
                return leftToFire[m];
            }
            else
            {
                return 0;
            }
        }

        public int[] Move(BattleField field, int i, int j)
        {
            int[] coord = { i, j };
            int[] newCoord;
            int[] nextMove;
            int value;
            int valueTemp;
            int numberOfEnnemies;
            int numberOfAllies;
            int numberOfTargets;
            int numberOfThreats;
            int direction;
            Random rand;
            if (Simulation.testMode)
            {
                newCoord = new int[2];
                rand = new Random((int)DateTime.Now.Ticks + new Random(ID).Next());
                i = 0;
                j = 0;
                value = 0;
                for (i = 0; i < field.Size; i++)
                {
                    for (j = 0; j < field.Size; j++)
                    {
                        numberOfAllies = field.Grid[i, j].CountAlliedShips(Attacking);
                        numberOfEnnemies = field.Grid[i, j].CountAliveShips() - numberOfAllies;
                        numberOfTargets = field.Grid[i, j].CountShipsOfType(SubType.FavoriteTargets[0], !attacking);
                        numberOfThreats = field.Grid[i, j].CountShipsOfType(SubType.WorstEnnemies[0], !Attacking);
                        if(numberOfTargets>0)
                        {
                            valueTemp = numberOfTargets * 4 - numberOfThreats * 2 + numberOfAllies - numberOfEnnemies;
                        }
                        else
                        {
                            valueTemp = numberOfEnnemies;
                        }
                        if (valueTemp > value)
                        {
                            newCoord[0] = i;
                            newCoord[1] = j;
                            value = valueTemp;
                        }
                        else
                        {
                            if (value == valueTemp)
                            {
                                if (Calcul.Distance(coord[0], coord[1], i, j) < Calcul.Distance(coord[0], coord[1], newCoord[0], newCoord[1]))
                                {
                                    newCoord[0] = i;
                                    newCoord[1] = j;
                                }
                            }
                        }
                    }
                }
                nextMove = new int[] { coord[0], coord[1] };
                direction = 0;
                if (newCoord[0] > coord[0])
                {
                    nextMove[0]++;
                    direction += 0b1;
                }
                else
                {
                    if (newCoord[0] < coord[0])
                    {
                        nextMove[0]--;
                        direction += 0b10;
                    }
                    else
                    {
                        direction += 0b100;
                    }
                }
                if (newCoord[1] > coord[1])
                {
                    nextMove[1]++;
                    direction += 0b1000;
                }
                else
                {
                    if (newCoord[1] < coord[1])
                    {
                        nextMove[1]--;
                        direction += 0b10000;
                    }
                    else
                    {
                        direction += 0b100000;
                    }
                }
                numberOfAllies = field.Grid[nextMove[0], nextMove[1]].CountAlliedShips(Attacking);
                numberOfEnnemies = field.Grid[nextMove[0], nextMove[1]].CountAliveShips() - numberOfAllies;
                numberOfTargets = field.Grid[nextMove[0], nextMove[1]].CountShipsOfType(SubType.FavoriteTargets[0], !attacking);
                numberOfThreats = field.Grid[nextMove[0], nextMove[1]].CountShipsOfType(SubType.WorstEnnemies[0], !Attacking);
                value = numberOfTargets - numberOfThreats * 2 - numberOfEnnemies * 2;
                newCoord[0] = nextMove[0];
                newCoord[1] = nextMove[1];

                switch (direction)
                {
                    case 0b001001://+1;+1
                        numberOfAllies = field.Grid[coord[0] + 1, coord[1]].CountAlliedShips(Attacking);
                        numberOfEnnemies = field.Grid[coord[0] + 1, coord[1]].CountAliveShips() - numberOfAllies;
                        numberOfTargets = field.Grid[coord[0] + 1, coord[1]].CountShipsOfType(SubType.FavoriteTargets[0], !attacking);
                        numberOfThreats = field.Grid[coord[0] + 1, coord[1]].CountShipsOfType(SubType.WorstEnnemies[0], !Attacking);
                        valueTemp = numberOfTargets - numberOfThreats * 2 - numberOfEnnemies * 2;
                        if (value < valueTemp)
                        {
                            newCoord[0] = coord[0] + 1;
                            newCoord[1] = coord[1];
                            value = valueTemp;
                        }
                        numberOfAllies = field.Grid[coord[0], coord[1] + 1].CountAlliedShips(Attacking);
                        numberOfEnnemies = field.Grid[coord[0], coord[1] + 1].CountAliveShips() - numberOfAllies;
                        numberOfTargets = field.Grid[coord[0], coord[1] + 1].CountShipsOfType(SubType.FavoriteTargets[0], !attacking);
                        numberOfThreats = field.Grid[coord[0], coord[1] + 1].CountShipsOfType(SubType.WorstEnnemies[0], !Attacking);
                        valueTemp = numberOfTargets - numberOfThreats * 2 - numberOfEnnemies * 2;
                        if (value < valueTemp)
                        {
                            newCoord[0] = coord[0];
                            newCoord[1] = coord[1] + 1;
                            value = valueTemp;
                        }
                        break;
                    case 0b001010://-1;+1
                        numberOfAllies = field.Grid[coord[0] - 1, coord[1]].CountAlliedShips(Attacking);
                        numberOfEnnemies = field.Grid[coord[0] - 1, coord[1]].CountAliveShips() - numberOfAllies;
                        numberOfTargets = field.Grid[coord[0] - 1, coord[1]].CountShipsOfType(SubType.FavoriteTargets[0], !attacking);
                        numberOfThreats = field.Grid[coord[0] - 1, coord[1]].CountShipsOfType(SubType.WorstEnnemies[0], !Attacking);
                        valueTemp = numberOfTargets - numberOfThreats * 2 - numberOfEnnemies * 2;
                        if (value < valueTemp)
                        {
                            newCoord[0] = coord[0] - 1;
                            newCoord[1] = coord[1];
                            value = valueTemp;
                        }
                        numberOfAllies = field.Grid[coord[0], coord[1] + 1].CountAlliedShips(Attacking);
                        numberOfEnnemies = field.Grid[coord[0], coord[1] + 1].CountAliveShips() - numberOfAllies;
                        numberOfTargets = field.Grid[coord[0], coord[1] + 1].CountShipsOfType(SubType.FavoriteTargets[0], !attacking);
                        numberOfThreats = field.Grid[coord[0], coord[1] + 1].CountShipsOfType(SubType.WorstEnnemies[0], !Attacking);
                        valueTemp = numberOfTargets - numberOfThreats * 2 - numberOfEnnemies * 2;
                        if (value < valueTemp)
                        {
                            newCoord[0] = coord[0];
                            newCoord[1] = coord[1] + 1;
                            value = valueTemp;
                        }
                        break;
                    case 0b001100://+0;+1
                        if (coord[0]+1<field.Size)
                        {
                            numberOfAllies = field.Grid[coord[0] + 1, coord[1] + 1].CountAlliedShips(Attacking);
                            numberOfEnnemies = field.Grid[coord[0] + 1, coord[1] + 1].CountAliveShips() - numberOfAllies;
                            numberOfTargets = field.Grid[coord[0] + 1, coord[1] + 1].CountShipsOfType(SubType.FavoriteTargets[0], !attacking);
                            numberOfThreats = field.Grid[coord[0] + 1, coord[1] + 1].CountShipsOfType(SubType.WorstEnnemies[0], !Attacking);
                            valueTemp = numberOfTargets - numberOfThreats * 2 - numberOfEnnemies * 2;
                            if (value < valueTemp)
                            {
                                newCoord[0] = coord[0] + 1;
                                newCoord[1] = coord[1] + 1;
                                value = valueTemp;
                            }
                        }
                        if (coord[0] - 1 >=0)
                        {
                            numberOfAllies = field.Grid[coord[0] - 1, coord[1] + 1].CountAlliedShips(Attacking);
                            numberOfEnnemies = field.Grid[coord[0] - 1, coord[1] + 1].CountAliveShips() - numberOfAllies;
                            numberOfTargets = field.Grid[coord[0] - 1, coord[1] + 1].CountShipsOfType(SubType.FavoriteTargets[0], !attacking);
                            numberOfThreats = field.Grid[coord[0] - 1, coord[1] + 1].CountShipsOfType(SubType.WorstEnnemies[0], !Attacking);
                            valueTemp = numberOfTargets - numberOfThreats * 2 - numberOfEnnemies * 2;
                            if (value < valueTemp)
                            {
                                newCoord[0] = coord[0] - 1;
                                newCoord[1] = coord[1] + 1;
                                value = valueTemp;
                            }
                        }
                        break;
                    case 0b010001://+1;-1
                        numberOfAllies = field.Grid[coord[0] + 1, coord[1]].CountAlliedShips(Attacking);
                        numberOfEnnemies = field.Grid[coord[0] + 1, coord[1]].CountAliveShips() - numberOfAllies;
                        numberOfTargets = field.Grid[coord[0] + 1, coord[1]].CountShipsOfType(SubType.FavoriteTargets[0], !attacking);
                        numberOfThreats = field.Grid[coord[0] + 1, coord[1]].CountShipsOfType(SubType.WorstEnnemies[0], !Attacking);
                        valueTemp = numberOfTargets - numberOfThreats * 2 - numberOfEnnemies * 2;
                        if (value < valueTemp)
                        {
                            newCoord[0] = coord[0] + 1;
                            newCoord[1] = coord[1];
                            value = valueTemp;
                        }
                        numberOfAllies = field.Grid[coord[0], coord[1] - 1].CountAlliedShips(Attacking);
                        numberOfEnnemies = field.Grid[coord[0], coord[1] - 1].CountAliveShips() - numberOfAllies;
                        numberOfTargets = field.Grid[coord[0], coord[1] - 1].CountShipsOfType(SubType.FavoriteTargets[0], !attacking);
                        numberOfThreats = field.Grid[coord[0], coord[1] - 1].CountShipsOfType(SubType.WorstEnnemies[0], !Attacking);
                        valueTemp = numberOfTargets - numberOfThreats * 2 - numberOfEnnemies * 2;
                        if (value < valueTemp)
                        {
                            newCoord[0] = coord[0];
                            newCoord[1] = coord[1] - 1;
                            value = valueTemp;
                        }
                        break;
                    case 0b010010://-1;-1
                        numberOfAllies = field.Grid[coord[0] - 1, coord[1]].CountAlliedShips(Attacking);
                        numberOfEnnemies = field.Grid[coord[0] - 1, coord[1]].CountAliveShips() - numberOfAllies;
                        numberOfTargets = field.Grid[coord[0] - 1, coord[1]].CountShipsOfType(SubType.FavoriteTargets[0], !attacking);
                        numberOfThreats = field.Grid[coord[0] - 1, coord[1]].CountShipsOfType(SubType.WorstEnnemies[0], !Attacking);
                        valueTemp = numberOfTargets - numberOfThreats * 2 - numberOfEnnemies * 2;
                        if (value < valueTemp)
                        {
                            newCoord[0] = coord[0] - 1;
                            newCoord[1] = coord[1];
                            value = valueTemp;
                        }
                        numberOfAllies = field.Grid[coord[0], coord[1] - 1].CountAlliedShips(Attacking);
                        numberOfEnnemies = field.Grid[coord[0], coord[1] - 1].CountAliveShips() - numberOfAllies;
                        numberOfTargets = field.Grid[coord[0], coord[1] - 1].CountShipsOfType(SubType.FavoriteTargets[0], !attacking);
                        numberOfThreats = field.Grid[coord[0], coord[1] - 1].CountShipsOfType(SubType.WorstEnnemies[0], !Attacking);
                        valueTemp = numberOfTargets - numberOfThreats * 2 - numberOfEnnemies * 2;
                        if (value < valueTemp)
                        {
                            newCoord[0] = coord[0];
                            newCoord[1] = coord[1] - 1;
                            value = valueTemp;
                        }
                        break;
                    case 0b010100://+0;-1
                        if (coord[0] + 1 < field.Size)
                        {
                            numberOfAllies = field.Grid[coord[0] + 1, coord[1] - 1].CountAlliedShips(Attacking);
                            numberOfEnnemies = field.Grid[coord[0] + 1, coord[1] - 1].CountAliveShips() - numberOfAllies;
                            numberOfTargets = field.Grid[coord[0] + 1, coord[1] - 1].CountShipsOfType(SubType.FavoriteTargets[0], !attacking);
                            numberOfThreats = field.Grid[coord[0] + 1, coord[1] - 1].CountShipsOfType(SubType.WorstEnnemies[0], !Attacking);
                            valueTemp = numberOfTargets - numberOfThreats * 2 - numberOfEnnemies * 2;
                            if (value < valueTemp)
                            {
                                newCoord[0] = coord[0] + 1;
                                newCoord[1] = coord[1] - 1;
                                value = valueTemp;
                            }
                        }
                        if (coord[0] - 1 >= 0)
                        {
                            numberOfAllies = field.Grid[coord[0] - 1, coord[1] - 1].CountAlliedShips(Attacking);
                            numberOfEnnemies = field.Grid[coord[0] - 1, coord[1] - 1].CountAliveShips() - numberOfAllies;
                            numberOfTargets = field.Grid[coord[0] - 1, coord[1] - 1].CountShipsOfType(SubType.FavoriteTargets[0], !attacking);
                            numberOfThreats = field.Grid[coord[0] - 1, coord[1] - 1].CountShipsOfType(SubType.WorstEnnemies[0], !Attacking);
                            valueTemp = numberOfTargets - numberOfThreats * 2 - numberOfEnnemies * 2;
                            if (value < valueTemp)
                            {
                                newCoord[0] = coord[0] - 1;
                                newCoord[1] = coord[1] - 1;
                                value = valueTemp;
                            }
                        }
                        break;
                    case 0b100001://+1;+0
                        if (coord[1] + 1 < field.Size)
                        {
                            numberOfAllies = field.Grid[coord[0] + 1, coord[1] + 1].CountAlliedShips(Attacking);
                            numberOfEnnemies = field.Grid[coord[0] + 1, coord[1] + 1].CountAliveShips() - numberOfAllies;
                            numberOfTargets = field.Grid[coord[0] + 1, coord[1] + 1].CountShipsOfType(SubType.FavoriteTargets[0], !attacking);
                            numberOfThreats = field.Grid[coord[0] + 1, coord[1] + 1].CountShipsOfType(SubType.WorstEnnemies[0], !Attacking);
                            valueTemp = numberOfTargets - numberOfThreats * 2 - numberOfEnnemies * 2;
                            if (value < valueTemp)
                            {
                                newCoord[0] = coord[0] + 1;
                                newCoord[1] = coord[1] + 1;
                                value = valueTemp;
                            }
                        }
                        if (coord[1] - 1 >= 0)
                        {
                            numberOfAllies = field.Grid[coord[0] + 1, coord[1] - 1].CountAlliedShips(Attacking);
                            numberOfEnnemies = field.Grid[coord[0] + 1, coord[1] - 1].CountAliveShips() - numberOfAllies;
                            numberOfTargets = field.Grid[coord[0] + 1, coord[1] - 1].CountShipsOfType(SubType.FavoriteTargets[0], !attacking);
                            numberOfThreats = field.Grid[coord[0] + 1, coord[1] - 1].CountShipsOfType(SubType.WorstEnnemies[0], !Attacking);
                            valueTemp = numberOfTargets - numberOfThreats * 2 - numberOfEnnemies * 2;
                            if (value < valueTemp)
                            {
                                newCoord[0] = coord[0] + 1;
                                newCoord[1] = coord[1] - 1;
                                value = valueTemp;
                            }
                        }
                        break;
                    case 0b100010://-1;+0
                        if (coord[1] + 1 < field.Size)
                        {
                            numberOfAllies = field.Grid[coord[0] - 1, coord[1] + 1].CountAlliedShips(Attacking);
                            numberOfEnnemies = field.Grid[coord[0] - 1, coord[1] + 1].CountAliveShips() - numberOfAllies;
                            numberOfTargets = field.Grid[coord[0] - 1, coord[1] + 1].CountShipsOfType(SubType.FavoriteTargets[0], !attacking);
                            numberOfThreats = field.Grid[coord[0] - 1, coord[1] + 1].CountShipsOfType(SubType.WorstEnnemies[0], !Attacking);
                            valueTemp = numberOfTargets - numberOfThreats * 2 - numberOfEnnemies * 2;
                            if (value < valueTemp)
                            {
                                newCoord[0] = coord[0] - 1;
                                newCoord[1] = coord[1] + 1;
                                value = valueTemp;
                            }
                        }
                        if (coord[1] - 1 >= 0)
                        {
                            numberOfAllies = field.Grid[coord[0] - 1, coord[1] - 1].CountAlliedShips(Attacking);
                            numberOfEnnemies = field.Grid[coord[0] - 1, coord[1] - 1].CountAliveShips() - numberOfAllies;
                            numberOfTargets = field.Grid[coord[0] - 1, coord[1] - 1].CountShipsOfType(SubType.FavoriteTargets[0], !attacking);
                            numberOfThreats = field.Grid[coord[0] - 1, coord[1] - 1].CountShipsOfType(SubType.WorstEnnemies[0], !Attacking);
                            valueTemp = numberOfTargets - numberOfThreats * 2 - numberOfEnnemies * 2;
                            if (value < valueTemp)
                            {
                                newCoord[0] = coord[0] - 1;
                                newCoord[1] = coord[1] - 1;
                                value = valueTemp;
                            }
                        }
                        break;
                    case 0b100100://+0;+0
                        if (coord[1] + 1 < field.Size) 
                        {
                            if(coord[0]-1>=0)
                            {
                                numberOfAllies = field.Grid[coord[0] - 1, coord[1] + 1].CountAlliedShips(Attacking);
                                numberOfEnnemies = field.Grid[coord[0] - 1, coord[1] + 1].CountAliveShips() - numberOfAllies;
                                numberOfTargets = field.Grid[coord[0] - 1, coord[1] + 1].CountShipsOfType(SubType.FavoriteTargets[0], !attacking);
                                numberOfThreats = field.Grid[coord[0] - 1, coord[1] + 1].CountShipsOfType(SubType.WorstEnnemies[0], !Attacking);
                                valueTemp = numberOfTargets - numberOfThreats * 2 - numberOfEnnemies * 2;
                                if (value < valueTemp)
                                {
                                    newCoord[0] = coord[0] - 1;
                                    newCoord[1] = coord[1] + 1;
                                    value = valueTemp;
                                }
                            }
                            if (coord[0] + 1 < field.Size)
                            {
                                numberOfAllies = field.Grid[coord[0] + 1, coord[1] + 1].CountAlliedShips(Attacking);
                                numberOfEnnemies = field.Grid[coord[0] + 1, coord[1] + 1].CountAliveShips() - numberOfAllies;
                                numberOfTargets = field.Grid[coord[0] + 1, coord[1] + 1].CountShipsOfType(SubType.FavoriteTargets[0], !attacking);
                                numberOfThreats = field.Grid[coord[0] + 1, coord[1] + 1].CountShipsOfType(SubType.WorstEnnemies[0], !Attacking);
                                valueTemp = numberOfTargets - numberOfThreats * 2 - numberOfEnnemies * 2;
                                if (value < valueTemp)
                                {
                                    newCoord[0] = coord[0] + 1;
                                    newCoord[1] = coord[1] + 1;
                                    value = valueTemp;
                                }
                            }
                            numberOfAllies = field.Grid[coord[0] - 1, coord[1]].CountAlliedShips(Attacking);
                            numberOfEnnemies = field.Grid[coord[0] - 1, coord[1]].CountAliveShips() - numberOfAllies;
                            numberOfTargets = field.Grid[coord[0] - 1, coord[1]].CountShipsOfType(SubType.FavoriteTargets[0], !attacking);
                            numberOfThreats = field.Grid[coord[0] - 1, coord[1]].CountShipsOfType(SubType.WorstEnnemies[0], !Attacking);
                            valueTemp = numberOfTargets - numberOfThreats * 2 - numberOfEnnemies * 2;
                            if (value < valueTemp)
                            {
                                newCoord[0] = coord[0] - 1;
                                newCoord[1] = coord[1];
                                value = valueTemp;
                            }

                        }
                        if (coord[1] - 1 >= 0)
                        {
                            if (coord[0] - 1 >= 0)
                            {
                                numberOfAllies = field.Grid[coord[0] - 1, coord[1] - 1].CountAlliedShips(Attacking);
                                numberOfEnnemies = field.Grid[coord[0] - 1, coord[1] - 1].CountAliveShips() - numberOfAllies;
                                numberOfTargets = field.Grid[coord[0] - 1, coord[1] - 1].CountShipsOfType(SubType.FavoriteTargets[0], !attacking);
                                numberOfThreats = field.Grid[coord[0] - 1, coord[1] - 1].CountShipsOfType(SubType.WorstEnnemies[0], !Attacking);
                                valueTemp = numberOfTargets - numberOfThreats * 2 - numberOfEnnemies * 2;
                                if (value < valueTemp)
                                {
                                    newCoord[0] = coord[0] - 1;
                                    newCoord[1] = coord[1] - 1;
                                    value = valueTemp;
                                }
                            }
                            if (coord[0] + 1 < field.Size)
                            {
                                numberOfAllies = field.Grid[coord[0] + 1, coord[1] - 1].CountAlliedShips(Attacking);
                                numberOfEnnemies = field.Grid[coord[0] + 1, coord[1] - 1].CountAliveShips() - numberOfAllies;
                                numberOfTargets = field.Grid[coord[0] + 1, coord[1] - 1].CountShipsOfType(SubType.FavoriteTargets[0], !attacking);
                                numberOfThreats = field.Grid[coord[0] + 1, coord[1] - 1].CountShipsOfType(SubType.WorstEnnemies[0], !Attacking);
                                valueTemp = numberOfTargets - numberOfThreats * 2 - numberOfEnnemies * 2;
                                if (value < valueTemp)
                                {
                                    newCoord[0] = coord[0] + 1;
                                    newCoord[1] = coord[1] - 1;
                                    value = valueTemp;
                                }
                            }
                            numberOfAllies = field.Grid[coord[0], coord[1] - 1].CountAlliedShips(Attacking);
                            numberOfEnnemies = field.Grid[coord[0], coord[1] - 1].CountAliveShips() - numberOfAllies;
                            numberOfTargets = field.Grid[coord[0], coord[1] - 1].CountShipsOfType(SubType.FavoriteTargets[0], !attacking);
                            numberOfThreats = field.Grid[coord[0], coord[1] - 1].CountShipsOfType(SubType.WorstEnnemies[0], !Attacking);
                            valueTemp = numberOfTargets - numberOfThreats * 2 - numberOfEnnemies * 2;
                            if (value < valueTemp)
                            {
                                newCoord[0] = coord[0];
                                newCoord[1] = coord[1] - 1;
                                value = valueTemp;
                            }
                        }
                        if (coord[0] + 1 < field.Size)
                        {
                            numberOfAllies = field.Grid[coord[0] + 1, coord[1]].CountAlliedShips(Attacking);
                            numberOfEnnemies = field.Grid[coord[0] + 1, coord[1]].CountAliveShips() - numberOfAllies;
                            numberOfTargets = field.Grid[coord[0] + 1, coord[1]].CountShipsOfType(SubType.FavoriteTargets[0], !attacking);
                            numberOfThreats = field.Grid[coord[0] + 1, coord[1]].CountShipsOfType(SubType.WorstEnnemies[0], !Attacking);
                            valueTemp = numberOfTargets - numberOfThreats * 2 - numberOfEnnemies * 2;
                            if (value < valueTemp)
                            {
                                newCoord[0] = coord[0] + 1;
                                newCoord[1] = coord[1] ;
                                value = valueTemp;
                            }
                        }
                        if (coord[0] -1 >= 0)
                        {
                            numberOfAllies = field.Grid[coord[0] -1, coord[1]].CountAlliedShips(Attacking);
                            numberOfEnnemies = field.Grid[coord[0] - 1, coord[1]].CountAliveShips() - numberOfAllies;
                            numberOfTargets = field.Grid[coord[0] - 1, coord[1]].CountShipsOfType(SubType.FavoriteTargets[0], !attacking);
                            numberOfThreats = field.Grid[coord[0] - 1, coord[1]].CountShipsOfType(SubType.WorstEnnemies[0], !Attacking);
                            valueTemp = numberOfTargets - numberOfThreats * 2 - numberOfEnnemies * 2;
                            if (value < valueTemp)
                            {
                                newCoord[0] = coord[0] - 1;
                                newCoord[1] = coord[1];
                                value = valueTemp;
                            }
                        }
                        numberOfAllies = field.Grid[coord[0], coord[1]].CountAlliedShips(Attacking);
                        numberOfEnnemies = field.Grid[coord[0], coord[1]].CountAliveShips() - numberOfAllies;
                        numberOfTargets = field.Grid[coord[0], coord[1]].CountShipsOfType(SubType.FavoriteTargets[0], !attacking);
                        numberOfThreats = field.Grid[coord[0], coord[1]].CountShipsOfType(SubType.WorstEnnemies[0], !Attacking);
                        valueTemp = numberOfTargets - numberOfThreats * 2 - numberOfEnnemies * 2;
                        if (value < valueTemp)
                        {
                            newCoord[0] = coord[0] - 1;
                            newCoord[1] = coord[1];
                            value = valueTemp;
                        }
                        break;
                }
                coord = newCoord;

            }
            else
            {
                rand = new Random((int)DateTime.Now.Ticks + new Random(ID).Next());
                i = i + rand.Next() % 3 - 1;
                if (i < 0)
                {
                    i = 0;
                }
                else
                {
                    if (i >= field.Size)
                    {
                        i = field.Size - 1;
                    }
                }
                j = j + rand.Next() % 3 - 1;
                if (j < 0)
                {
                    j = 0;
                }
                else
                {
                    if (j >= field.Size)
                    {
                        j = field.Size - 1;
                    }
                }
                coord[0] = i;
                coord[1] = j;
            }

            MovementLeft--;
            return coord;
        }
    }
}