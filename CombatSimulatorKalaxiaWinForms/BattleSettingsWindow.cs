using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CombatSimulatorKalaxiaWinForms
{
    public partial class BattleSettingsWindow : Form
    {
        private int numberOfBattles;
        private int numberOfShips;
        private int gridSize;
        private int bitmapSize;

        public int NumberOfBattles { get => numberOfBattles; set => numberOfBattles = value; }
        public int NumberOfShips { get => numberOfShips; set => numberOfShips = value; }
        public int GridSize { get => gridSize; set => gridSize = value; }
        public int BitmapSize { get => bitmapSize; set => bitmapSize = value; }

        public BattleSettingsWindow(int numberOfBat, int numberOfShi, int gridSiz, int bitmapSiz)
        {
            NumberOfBattles = numberOfBat;
            NumberOfShips = numberOfShi;
            GridSize = gridSiz;
            BitmapSize = bitmapSiz;
            InitializeComponent();
        }

        private void BattleSettingsWindow_Load(object sender, EventArgs e)
        {
            TBGridSize.Text = GridSize.ToString();
            TBNumberOfBattles.Text = NumberOfBattles.ToString();
            TBNumberOfShips.Text = NumberOfShips.ToString();
            TBBitmapSize.Text = BitmapSize.ToString();
        }
    }
}
