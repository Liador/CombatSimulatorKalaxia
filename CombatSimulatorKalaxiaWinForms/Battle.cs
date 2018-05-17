using System;
using System.Collections.Generic;
using System.Threading;

namespace Simulator
{
    class Battle
    {
        private Army attackers;
        //private int battleTime;
        private Army defenders;
        private string winner;
        private int numberOfTurns;
        readonly List<Ship> shipList;
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
            //battleTime = 0;
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
            int turnPlayed = 0;
            if (Simulation.fightOnBattleField)
            {
                while (turnPlayed < numberOfTurns && !attackers.Victory && !defenders.Victory)
                {
                    AllShipsAttack();
                    AllShipsMove();
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
            if (Simulation.fightOnBattleField)
            {
                while (!attackers.Victory && !defenders.Victory)
                {
                    Thread.Sleep(1);
                    AllShipsAttack();
                    AllShipsMove();
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

        private void AllShipsMove()
        {
            int i, j, k;
            Ship shipTemp;
            for (i = 0; i < Field.Size; i++)
            {
                for (j = 0; j < Field.Size; j++)
                {
                    k = 0;
                    while (k < Field.Grid[i, j].ShipsList.Count)
                    {
                        shipTemp = Field.Grid[i, j].ShipsList[k];
                        if(shipTemp.Alive)
                        {
                            MakeShipMove(shipTemp, i, j);
                        }
                        k++;
                    }
                }
            }
        }

        private void MakeShipMove(Ship shipTemp, int i, int j)
        {
            int[] nextSquare = { i, j };
            while(shipTemp.MovementLeft > 0)
            {
                Field.Grid[nextSquare[0], nextSquare[1]].ShipsList.Remove(shipTemp);
                nextSquare = shipTemp.Move(Field, nextSquare[0], nextSquare[1]);
                Field.Grid[nextSquare[0], nextSquare[1]].ShipsList.Add(shipTemp);
            }
        }

        private void AllShipsAttack()
        {
            int i;
            int j;
            for (i = 0; i < Field.Size; i++)
            {
                for (j = 0; j < Field.Size; j++)
                {
                    foreach (Ship s in Field.Grid[i, j].ShipsList)
                    {
                        if (s.Alive)
                        {
                            s.Attack(Field, i, j);
                        }
                    }
                }
            }
        }

        public override string ToString()
        {
            return "----------------------------------------------------\nAttackers :" + Attackers.ToString() + "\nDefenders :" + Defenders.ToString()+ "\n----------------------------------------------------\n"; 
        }
    }
}