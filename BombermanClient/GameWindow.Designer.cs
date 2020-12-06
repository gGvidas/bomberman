namespace SnakeGame
{
    partial class GameWindow
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
            this.components = new System.ComponentModel.Container();
            this.screen = new System.Windows.Forms.PictureBox();
            this.textGameOver = new System.Windows.Forms.Label();
            this.buttonStart = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.winnerLabel = new System.Windows.Forms.Label();
            this.buttonRestart = new System.Windows.Forms.Button();
            this.score = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.screen)).BeginInit();
            this.SuspendLayout();
            // 
            // screen
            // 
            this.screen.Location = new System.Drawing.Point(341, 26);
            this.screen.Margin = new System.Windows.Forms.Padding(2);
            this.screen.Name = "screen";
            this.screen.Size = new System.Drawing.Size(1000, 692);
            this.screen.TabIndex = 0;
            this.screen.TabStop = false;
            // 
            // textGameOver
            // 
            this.textGameOver.AutoSize = true;
            this.textGameOver.BackColor = System.Drawing.Color.Transparent;
            this.textGameOver.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textGameOver.ForeColor = System.Drawing.Color.Red;
            this.textGameOver.Location = new System.Drawing.Point(1000, 97);
            this.textGameOver.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.textGameOver.Name = "textGameOver";
            this.textGameOver.Size = new System.Drawing.Size(443, 73);
            this.textGameOver.TabIndex = 1;
            this.textGameOver.Text = "You have died";
            this.textGameOver.Click += new System.EventHandler(this.textGameOver_Click);
            // 
            // buttonStart
            // 
            this.buttonStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 40F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonStart.Location = new System.Drawing.Point(575, 263);
            this.buttonStart.Margin = new System.Windows.Forms.Padding(2);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(272, 114);
            this.buttonStart.TabIndex = 2;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // winnerLabel
            // 
            this.winnerLabel.AutoSize = true;
            this.winnerLabel.BackColor = System.Drawing.Color.Transparent;
            this.winnerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.winnerLabel.ForeColor = System.Drawing.Color.Red;
            this.winnerLabel.Location = new System.Drawing.Point(1000, 165);
            this.winnerLabel.Name = "winnerLabel";
            this.winnerLabel.Size = new System.Drawing.Size(534, 73);
            this.winnerLabel.TabIndex = 3;
            this.winnerLabel.Text = "You are a winner!";
            // 
            // buttonRestart
            // 
            this.buttonRestart.Font = new System.Drawing.Font("Microsoft Sans Serif", 40.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonRestart.Location = new System.Drawing.Point(1138, 263);
            this.buttonRestart.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonRestart.Name = "buttonRestart";
            this.buttonRestart.Size = new System.Drawing.Size(272, 114);
            this.buttonRestart.TabIndex = 4;
            this.buttonRestart.Text = "Restart";
            this.buttonRestart.UseVisualStyleBackColor = true;
            this.buttonRestart.Click += new System.EventHandler(this.buttonRestart_Click);
            // 
            // score
            // 
            this.score.AutoSize = true;
            this.score.Location = new System.Drawing.Point(1000, 470);
            this.score.Name = "score";
            this.score.Size = new System.Drawing.Size(36, 15);
            this.score.TabIndex = 5;
            this.score.Text = "0";
            // 
            // GameWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1600, 791);
            this.Controls.Add(this.score);
            this.Controls.Add(this.buttonRestart);
            this.Controls.Add(this.winnerLabel);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.textGameOver);
            this.Controls.Add(this.screen);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "GameWindow";
            this.Text = "Bomberman by Twin Towers (c)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.screen)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox screen;
        private System.Windows.Forms.Label textGameOver;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label winnerLabel;
        private System.Windows.Forms.Button buttonRestart;
        private System.Windows.Forms.Label score;
    }
}

