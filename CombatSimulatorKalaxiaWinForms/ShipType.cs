namespace Simulator
{
    class ShipType
    {
        private string name;
        private int number;

        public string Name { get => name; set => name = value; }
        public int Number { get => number; set => number = value; }

        public ShipType(string name, int number)
        {
            Name = name;
            Number = number;
        }
    }
}