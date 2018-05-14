using System.Collections.Generic;
using System.Threading;

namespace Simulator
{
    class Battle
    {
        private Army attackers;
        private int battleTime;
        private Army defenders;
        private string winner;
        private int numberOfTurns;
        List<Ship> shipList;
        BattleField field;
        List<BattleField> battleFieldHistory;

        public string Winner { get => winner; set => winner = value; }
        internal Army Attackers { get => attackers; set => attackers = value; }
        internal Army Defenders { get => defenders; set => defenders = value; }
        public int NumberOfTurns { get => numberOfTurns; set => numberOfTurns = value; }
        internal List<Ship> ShipList { get => shipList; }
        internal BattleField Field { get => field; set => field = value; }
        internal List<BattleField> BattleFieldHistory { get => battleFieldHistory; }

        public Battle (Army attackers, Army defenders ,int size)
        {
            Attackers = attackers;
            Defenders = defenders;
            Winner = "";
            shipList = new List<Ship>();
            foreach (Ship s in Attackers.Ships)
            {
                ShipList.Add(s);
            }
            foreach (Ship s in Defenders.Ships)
            {
                ShipList.Add(s);
            }
            ShipList.Sort();
            Field = new BattleField(size);// size odd = symetrical battlefield
            NumberOfTurns = 0;
            battleTime = 0;
        }

        public void Start()
        {
            battleFieldHistory = new List<BattleField>();
            if (Simulation.fightOnBattleField)
            {
                Field.Init(Attackers, Defenders);
                battleFieldHistory.Add(new BattleField(Field));
            }
        }

        public void Play(int numberOfTurns)
        {
            int i, j, k;
            Ship shipTemp;
            int turnPlayed = 0;
            if (Simulation.fightOnBattleField)
            {
                while (turnPlayed < numberOfTurns && !attackers.Victory && !defenders.Victory)
                {
                    for (i = 0; i < Field.Size; i++)
                    {
                        for (j = 0; j < Field.Size; j++)
                        {
                            foreach (Ship s in Field.Grid[i, j])
                            {
                                if (s.Alive)
                                {
                                    s.Attack(Field, i, j);
                                }
                            }
                        }
                    }
                    for (i = 0; i < Field.Size; i++)
                    {
                        for (j = 0; j < Field.Size; j++)
                        {
                            k = 0;
                            while (k < Field.Grid[i, j].Count)
                            {
                                shipTemp = Field.Grid[i, j][k];
                                if (Field.Grid[i, j][k].Alive && shipTemp.MovementLeft > 0)
                                {
                                    Field.Grid[i, j].Remove(shipTemp);

                                    Field.Grid[shipTemp.Move(Field, i, j)[0], shipTemp.Move(Field, i, j)[1]].Add(shipTemp);
                                }
                                else
                                {
                                    k++;
                                }
                            }
                        }
                    }
                    Attackers.CountShipsAlive();
                    Defenders.CountShipsAlive();
                    NumberOfTurns++;
                    if (Attackers.ShipsAlive == 0)
                    {
                        Defenders.ClaimVictory();
                    }
                    else
                    {
                        if (Defenders.ShipsAlive == 0)
                        {
                            Attackers.ClaimVictory();
                        }
                        else
                        {
                            foreach (Ship s in ShipList)
                            {
                                if (s.Alive)
                                {
                                    s.Reload();
                                    s.Refill();
                                }
                            }
                        }
                    }
                    if (Simulation.numberOfBattles == 1)
                    {

                    }
                    battleFieldHistory.Add(new BattleField(Field));
                }
            }
        }

        public void Play()
        {
            int i, j, k;
            Ship shipTemp;
            if (Simulation.fightOnBattleField)
            {
                while (!attackers.Victory && !defenders.Victory)
                {
                    Thread.Sleep(1);
                    for (i = 0; i < Field.Size; i++)
                    {
                        for (j = 0; j < Field.Size; j++)
                        {
                            foreach (Ship s in Field.Grid[i, j])
                            {
                                if (s.Alive)
                                {
                                    s.Attack(Field, i, j);
                                }
                            }
                        }
                    }
                    for (i = 0; i < Field.Size; i++)
                    {
                        for (j = 0; j < Field.Size; j++)
                        {
                            k = 0;
                            while (k < Field.Grid[i, j].Count)
                            {
                                shipTemp = Field.Grid[i, j][k];
                                if (Field.Grid[i, j][k].Alive && shipTemp.MovementLeft > 0)
                                {
                                    Field.Grid[i, j].Remove(shipTemp);

                                    Field.Grid[shipTemp.Move(Field, i, j)[0], shipTemp.Move(Field, i, j)[1]].Add(shipTemp);
                                }
                                else
                                {
                                    k++;
                                }
                            }
                        }
                    }
                    Attackers.CountShipsAlive();
                    Defenders.CountShipsAlive();
                    NumberOfTurns++;
                    if (Attackers.ShipsAlive == 0)
                    {
                        Defenders.ClaimVictory();
                        Winner = Defenders.Name;
                    }
                    else
                    {
                        if (Defenders.ShipsAlive == 0)
                        {
                            Attackers.ClaimVictory();
                            Winner = Attackers.Name;
                        }
                        else
                        {
                            foreach (Ship s in ShipList)
                            {
                                if (s.Alive)
                                {
                                    s.Reload();
                                    s.Refill();
                                }
                            }
                        }
                    }
                    if (Simulation.numberOfBattles == 1)
                    {

                    }
                    battleFieldHistory.Add(new BattleField(Field));
                }
            }
        }

        public override string ToString()
        {
            return "----------------------------------------------------\nAttackers :" + Attackers.ToString() + "\nDefenders :" + Defenders.ToString()+ "\n----------------------------------------------------\n"; 
        }
    }
}