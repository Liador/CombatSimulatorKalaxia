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
        private int durability;
        private int maxDurability;
        private int blueShield;
        private int maxBlueShield;
        private int greyShield;
        private int maxGreyShield;
        private int moveSpeed;
        private int initiative;
        private List<Weapon> weapons;
        private int[] leftToFire;
        private bool alive;
        private List<Ship> hangar;
        private string armyName;
        private int armyId;
        private int missedMissiles;
        private int firedMissiles;
        private int movementLeft;

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
            leftToFire = new int[Weapons.Count];
            Reload();
            numberOfShips++;
        }
        public Ship(Ship ship)
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
            leftToFire = new int[Weapons.Count];
            Reload();
            numberOfShips++;
        }
        public void Refill()
        {
            movementLeft =1;
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
                            missedMissiles++;
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
                    retval += missedMissiles + "/" + firedMissiles + " missiles missed this ship";
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
                            for (l = 0; l < field.Grid[i, j].Count && leftToFire[m] > 0; l++)
                            {
                                if (field.Grid[i, j][l].ArmyName != this.ArmyName && field.Grid[i, j][l].Alive && leftToFire[m] > 0)
                                {
                                    if (field.Grid[i, j][l].Type.Number == SubType.FavoriteTargets[k].Number)
                                    {
                                        while (leftToFire[m] > 0 && field.Grid[i, j][l].Alive)
                                        {
                                            if (Simulation.numberOfBattles == 1)
                                            {
                                                if (!field.Grid[i, j][l].TakeDamage(Weapons[m]))
                                                {
                                                    //ship destroyed
                                                }
                                            }
                                            else
                                            {
                                                field.Grid[i, j][l].TakeDamage(Weapons[m]);
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
            Random rand;
            if (Simulation.testMode)
            {
                rand = new Random((int)DateTime.Now.Ticks + new Random(ID).Next());
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
            }
            
            MovementLeft--;
            return new int[2] { i, j };
        }
    }
}