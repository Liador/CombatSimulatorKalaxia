namespace CombatSimulatorKalaxiaWinForms
{
    partial class BattleSettingsWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.TBNumberOfBattles = new System.Windows.Forms.TextBox();
            this.TBGridSize = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TBNumberOfShips = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.TBBitmapSize = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Number of Battles";
            // 
            // TBNumberOfBattles
            // 
            this.TBNumberOfBattles.Enabled = false;
            this.TBNumberOfBattles.Location = new System.Drawing.Point(214, 9);
            this.TBNumberOfBattles.Name = "TBNumberOfBattles";
            this.TBNumberOfBattles.Size = new System.Drawing.Size(100, 22);
            this.TBNumberOfBattles.TabIndex = 1;
            this.TBNumberOfBattles.Text = "1";
            // 
            // TBGridSize
            // 
            this.TBGridSize.Location = new System.Drawing.Point(214, 55);
            this.TBGridSize.Name = "TBGridSize";
            this.TBGridSize.Size = new System.Drawing.Size(100, 22);
            this.TBGridSize.TabIndex = 2;
            this.TBGridSize.Text = "5";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Size of the Grid";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(126, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Total Ship Number";
            // 
            // TBNumberOfShips
            // 
            this.TBNumberOfShips.Location = new System.Drawing.Point(214, 111);
            this.TBNumberOfShips.Name = "TBNumberOfShips";
            this.TBNumberOfShips.Size = new System.Drawing.Size(100, 22);
            this.TBNumberOfShips.TabIndex = 5;
            this.TBNumberOfShips.Text = "20";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 177);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "Bitmap Size";
            // 
            // TBBitmapSize
            // 
            this.TBBitmapSize.Location = new System.Drawing.Point(214, 172);
            this.TBBitmapSize.Name = "TBBitmapSize";
            this.TBBitmapSize.Size = new System.Drawing.Size(100, 22);
            this.TBBitmapSize.TabIndex = 7;
            // 
            // BattleSettingsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(762, 517);
            this.Controls.Add(this.TBBitmapSize);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.TBNumberOfShips);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TBGridSize);
            this.Controls.Add(this.TBNumberOfBattles);
            this.Controls.Add(this.label1);
            this.Name = "BattleSettingsWindow";
            this.Text = "Battle Settings";
            this.Load += new System.EventHandler(this.BattleSettingsWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TBNumberOfBattles;
        private System.Windows.Forms.TextBox TBGridSize;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TBNumberOfShips;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TBBitmapSize;
    }
}