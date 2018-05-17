namespace Simulator
{
    class ShipType
    {
        private string name;
        private int number;
        private int numberOfMovement;

        public string Name { get => name; set => name = value; }
        public int Number { get => number; set => number = value; }
        public int NumberOfMovement { get => numberOfMovement; set => numberOfMovement = value; }

        public ShipType(string name, int number, int numberOfMovement)
        {
            Name = name;
            Number = number;
            NumberOfMovement = numberOfMovement;
        }
    }
}