﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Simulator;

namespace CombatSimulatorKalaxiaWinForms
{
    

    public partial class SimulatorWindow : Form
    {
        private int numberOfBattles = 1;
        public int numberOfShips= 1000;
        public int gridSize=11;
        public int bitmapSize = 1000;
        public int startingGridRows=3;
        public int startingGridLines=9;
        Simulation sim;

        public int NumberOfBattles { get => numberOfBattles; set => numberOfBattles = value; }

        public SimulatorWindow()
        {
            InitializeComponent();
        }

        private void Label1_Click(object sender, EventArgs e)
        {
            
        }

        private void Play_Click(object sender, EventArgs e)
        {
            int i;
            
            sim = new Simulation(NumberOfBattles, true, numberOfShips, gridSize, startingGridLines,startingGridRows);
            sim.Play();
            Combo_Turns.Items.Clear();
            for (i=0; i<sim.Battle.BattleFieldHistory.Count;i++)
            {
                Combo_Turns.Items.Add("Turn " + (i + 1));
            }
            Combo_Turns.SelectedIndex = 0;
            LblWinner.Text = sim.Battle.Winner;
            Combo_Turns.Select();

            //BattleField bf = new BattleField(5);
            //bf.DrawBattleField(BattleFieldImage.Height);
            //BattleFieldImage.Image = bf.BattleFieldBitmap;
            
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            Bitmap bm = new Bitmap(bitmapSize, bitmapSize);
            BattleFieldImage.Image = bm;
        }

        private void BattleSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BattleSettingsWindow battleSettingsWindow;
            battleSettingsWindow = new BattleSettingsWindow(NumberOfBattles, numberOfShips, gridSize, bitmapSize);
            battleSettingsWindow.Show();

        }

        private void Combo_Turns_SelectedIndexChanged(object sender, EventArgs e)
        {
            //show the grid of the selected turn
            ShowBattlefield();
            
        }

        private void ShowBattlefield()
        {
            BattleFieldImage.Image.Dispose();
            BattleFieldImage.Image = sim.Battle.BattleFieldHistory[Combo_Turns.SelectedIndex].DrawBattleField(bitmapSize, ShowDestroyedShips.Checked);
            BattleFieldImage.Refresh();
        }

        private void QuitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void ShowDestroyedShips_CheckedChanged(object sender, EventArgs e)
        {
            ShowBattlefield();
            Combo_Turns.Select();
        }
    }
}
