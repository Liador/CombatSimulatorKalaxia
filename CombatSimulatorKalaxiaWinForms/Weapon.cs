namespace Simulator
{
    class Weapon
    {
        public const int LASER= 1;
        public const int MISSILE = 2;
        public const int ION = 3;
        public const int GAUSS = 4;
        public const int EMP = 5;
        private int numberOfShots;
        private int attackSpeed;
        private string name;
        private string typeName;
        private int type;
        private int[] compatibleFrameTypes;
        private int damage;
        private ShipType[] favoriteTargets;
        private int range;

        public string Name { get => name; set => name = value; }
        public string TypeName { get => typeName; set => typeName = value; }
        public int Type { get => type; set => type = value; }
        public int Damage { get => damage; set => damage = value; }
        public ShipType[] FavoriteTargets { get => favoriteTargets; set => favoriteTargets = value; }
        public int[] CompatibleFrameTypes { get => compatibleFrameTypes; set => compatibleFrameTypes = value; }
        public int NumberOfShots { get => numberOfShots; set => numberOfShots = value; }
        public int AttackSpeed { get => attackSpeed; set => attackSpeed = value; }
        internal int Range { get => range; set => range = value; }

        public Weapon(string name, string typeName, int type, int damage, int numberOfShots,int range, ShipType[] favoriteTargets, int[] compatibleFrameTypes)
        {
            Name = name;
            TypeName = typeName;
            Type = type;
            Damage = damage;
            NumberOfShots = numberOfShots;
            FavoriteTargets = favoriteTargets;
            CompatibleFrameTypes = compatibleFrameTypes;
            Range = range;
        }

        private void Fire(Ship ennemy)
        {

        }

        public override string ToString()
        {
            string retval;
            retval = Name;
            return retval;
        }
    }
}