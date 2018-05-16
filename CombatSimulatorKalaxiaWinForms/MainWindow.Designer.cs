namespace CombatSimulatorKalaxiaWinForms
{
    partial class SimulatorWindow
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.topMenu = new System.Windows.Forms.MenuStrip();
            this.playToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.battleSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modulesSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shipsSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PlayButton = new System.Windows.Forms.Button();
            this.Combo_Turns = new System.Windows.Forms.ComboBox();
            this.LblChooseTurn = new System.Windows.Forms.Label();
            this.BattleFieldImage = new System.Windows.Forms.PictureBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.LblWinner = new System.Windows.Forms.Label();
            this.ShowDestroyedShips = new System.Windows.Forms.CheckBox();
            this.topMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BattleFieldImage)).BeginInit();
            this.SuspendLayout();
            // 
            // topMenu
            // 
            this.topMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.topMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.playToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.quitToolStripMenuItem});
            this.topMenu.Location = new System.Drawing.Point(0, 0);
            this.topMenu.Name = "topMenu";
            this.topMenu.Size = new System.Drawing.Size(1182, 28);
            this.topMenu.TabIndex = 1;
            this.topMenu.Text = "menuStrip1";
            // 
            // playToolStripMenuItem
            // 
            this.playToolStripMenuItem.Name = "playToolStripMenuItem";
            this.playToolStripMenuItem.Size = new System.Drawing.Size(48, 24);
            this.playToolStripMenuItem.Text = "Play";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.battleSettingsToolStripMenuItem,
            this.modulesSettingsToolStripMenuItem,
            this.shipsSettingsToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(74, 24);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Visible = false;
            // 
            // battleSettingsToolStripMenuItem
            // 
            this.battleSettingsToolStripMenuItem.Name = "battleSettingsToolStripMenuItem";
            this.battleSettingsToolStripMenuItem.Size = new System.Drawing.Size(198, 26);
            this.battleSettingsToolStripMenuItem.Text = "Battle Settings";
            this.battleSettingsToolStripMenuItem.Click += new System.EventHandler(this.BattleSettingsToolStripMenuItem_Click);
            // 
            // modulesSettingsToolStripMenuItem
            // 
            this.modulesSettingsToolStripMenuItem.Name = "modulesSettingsToolStripMenuItem";
            this.modulesSettingsToolStripMenuItem.Size = new System.Drawing.Size(198, 26);
            this.modulesSettingsToolStripMenuItem.Text = "Modules Settings";
            // 
            // shipsSettingsToolStripMenuItem
            // 
            this.shipsSettingsToolStripMenuItem.Name = "shipsSettingsToolStripMenuItem";
            this.shipsSettingsToolStripMenuItem.Size = new System.Drawing.Size(198, 26);
            this.shipsSettingsToolStripMenuItem.Text = "Ships Settings";
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(49, 24);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.QuitToolStripMenuItem_Click);
            // 
            // PlayButton
            // 
            this.PlayButton.Location = new System.Drawing.Point(1017, 891);
            this.PlayButton.Name = "PlayButton";
            this.PlayButton.Size = new System.Drawing.Size(153, 40);
            this.PlayButton.TabIndex = 2;
            this.PlayButton.Text = "Play";
            this.PlayButton.UseVisualStyleBackColor = true;
            this.PlayButton.Click += new System.EventHandler(this.Play_Click);
            // 
            // Combo_Turns
            // 
            this.Combo_Turns.FormattingEnabled = true;
            this.Combo_Turns.Location = new System.Drawing.Point(918, 51);
            this.Combo_Turns.Name = "Combo_Turns";
            this.Combo_Turns.Size = new System.Drawing.Size(197, 24);
            this.Combo_Turns.TabIndex = 4;
            this.Combo_Turns.SelectedIndexChanged += new System.EventHandler(this.Combo_Turns_SelectedIndexChanged);
            // 
            // LblChooseTurn
            // 
            this.LblChooseTurn.AutoSize = true;
            this.LblChooseTurn.Location = new System.Drawing.Point(918, 31);
            this.LblChooseTurn.Name = "LblChooseTurn";
            this.LblChooseTurn.Size = new System.Drawing.Size(113, 17);
            this.LblChooseTurn.TabIndex = 5;
            this.LblChooseTurn.Text = "Choose the turn:";
            // 
            // BattleFieldImage
            // 
            this.BattleFieldImage.Location = new System.Drawing.Point(12, 31);
            this.BattleFieldImage.MaximumSize = new System.Drawing.Size(2000, 2000);
            this.BattleFieldImage.MinimumSize = new System.Drawing.Size(500, 500);
            this.BattleFieldImage.Name = "BattleFieldImage";
            this.BattleFieldImage.Size = new System.Drawing.Size(900, 900);
            this.BattleFieldImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.BattleFieldImage.TabIndex = 6;
            this.BattleFieldImage.TabStop = false;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // LblWinner
            // 
            this.LblWinner.AutoSize = true;
            this.LblWinner.Location = new System.Drawing.Point(918, 120);
            this.LblWinner.Name = "LblWinner";
            this.LblWinner.Size = new System.Drawing.Size(0, 17);
            this.LblWinner.TabIndex = 7;
            // 
            // ShowDestroyedShips
            // 
            this.ShowDestroyedShips.AutoSize = true;
            this.ShowDestroyedShips.Location = new System.Drawing.Point(921, 149);
            this.ShowDestroyedShips.Name = "ShowDestroyedShips";
            this.ShowDestroyedShips.Size = new System.Drawing.Size(192, 21);
            this.ShowDestroyedShips.TabIndex = 8;
            this.ShowDestroyedShips.Text = "Show the destroyed ships";
            this.ShowDestroyedShips.UseVisualStyleBackColor = true;
            this.ShowDestroyedShips.CheckedChanged += new System.EventHandler(this.ShowDestroyedShips_CheckedChanged);
            // 
            // SimulatorWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1182, 1055);
            this.Controls.Add(this.ShowDestroyedShips);
            this.Controls.Add(this.LblWinner);
            this.Controls.Add(this.BattleFieldImage);
            this.Controls.Add(this.LblChooseTurn);
            this.Controls.Add(this.Combo_Turns);
            this.Controls.Add(this.PlayButton);
            this.Controls.Add(this.topMenu);
            this.MainMenuStrip = this.topMenu;
            this.Name = "SimulatorWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Simulator";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.topMenu.ResumeLayout(false);
            this.topMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BattleFieldImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip topMenu;
        private System.Windows.Forms.ToolStripMenuItem playToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem battleSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modulesSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem shipsSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.Button PlayButton;
        private System.Windows.Forms.ComboBox Combo_Turns;
        private System.Windows.Forms.Label LblChooseTurn;
        private System.Windows.Forms.PictureBox BattleFieldImage;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label LblWinner;
        private System.Windows.Forms.CheckBox ShowDestroyedShips;
    }
}

