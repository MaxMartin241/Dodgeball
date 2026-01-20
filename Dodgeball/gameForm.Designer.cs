namespace Dodgeball
{
    partial class gameForm
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
            this.gameTimer = new System.Windows.Forms.Timer(this.components);
            this.redScoreLabel = new System.Windows.Forms.Label();
            this.blueScoreLabel = new System.Windows.Forms.Label();
            this.gameTimeLabel = new System.Windows.Forms.Label();
            this.winnerLabel = new System.Windows.Forms.Label();
            this.nameInputBox = new System.Windows.Forms.TextBox();
            this.saveWinButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // gameTimer
            // 
            this.gameTimer.Enabled = true;
            this.gameTimer.Interval = 16;
            this.gameTimer.Tick += new System.EventHandler(this.gameTimer_Tick);
            // 
            // redScoreLabel
            // 
            this.redScoreLabel.AutoSize = true;
            this.redScoreLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.redScoreLabel.Location = new System.Drawing.Point(323, 9);
            this.redScoreLabel.Name = "redScoreLabel";
            this.redScoreLabel.Size = new System.Drawing.Size(0, 18);
            this.redScoreLabel.TabIndex = 0;
            // 
            // blueScoreLabel
            // 
            this.blueScoreLabel.AutoSize = true;
            this.blueScoreLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.blueScoreLabel.Location = new System.Drawing.Point(479, 9);
            this.blueScoreLabel.Name = "blueScoreLabel";
            this.blueScoreLabel.Size = new System.Drawing.Size(0, 18);
            this.blueScoreLabel.TabIndex = 1;
            // 
            // gameTimeLabel
            // 
            this.gameTimeLabel.AutoSize = true;
            this.gameTimeLabel.Location = new System.Drawing.Point(13, 13);
            this.gameTimeLabel.Name = "gameTimeLabel";
            this.gameTimeLabel.Size = new System.Drawing.Size(0, 13);
            this.gameTimeLabel.TabIndex = 2;
            // 
            // winnerLabel
            // 
            this.winnerLabel.AutoSize = true;
            this.winnerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.winnerLabel.Location = new System.Drawing.Point(367, 150);
            this.winnerLabel.Name = "winnerLabel";
            this.winnerLabel.Size = new System.Drawing.Size(66, 24);
            this.winnerLabel.TabIndex = 3;
            this.winnerLabel.Text = "label1";
            this.winnerLabel.Visible = false;
            // 
            // nameInputBox
            // 
            this.nameInputBox.Location = new System.Drawing.Point(367, 255);
            this.nameInputBox.Name = "nameInputBox";
            this.nameInputBox.Size = new System.Drawing.Size(100, 20);
            this.nameInputBox.TabIndex = 4;
            this.nameInputBox.Visible = false;
            // 
            // saveWinButton
            // 
            this.saveWinButton.Location = new System.Drawing.Point(377, 281);
            this.saveWinButton.Name = "saveWinButton";
            this.saveWinButton.Size = new System.Drawing.Size(75, 23);
            this.saveWinButton.TabIndex = 5;
            this.saveWinButton.Text = "Save";
            this.saveWinButton.UseVisualStyleBackColor = true;
            this.saveWinButton.Click += new System.EventHandler(this.saveWinButton_Click);
            // 
            // gameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.saveWinButton);
            this.Controls.Add(this.nameInputBox);
            this.Controls.Add(this.winnerLabel);
            this.Controls.Add(this.gameTimeLabel);
            this.Controls.Add(this.blueScoreLabel);
            this.Controls.Add(this.redScoreLabel);
            this.DoubleBuffered = true;
            this.Name = "gameForm";
            this.Text = "gameForm";
            this.Load += new System.EventHandler(this.gameForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.gameForm_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gameForm_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.gameForm_KeyUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer gameTimer;
        private System.Windows.Forms.Label redScoreLabel;
        private System.Windows.Forms.Label blueScoreLabel;
        private System.Windows.Forms.Label gameTimeLabel;
        private System.Windows.Forms.Label winnerLabel;
        private System.Windows.Forms.TextBox nameInputBox;
        private System.Windows.Forms.Button saveWinButton;
    }
}