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
            this.redScoreLabel.Location = new System.Drawing.Point(415, 13);
            this.redScoreLabel.Name = "redScoreLabel";
            this.redScoreLabel.Size = new System.Drawing.Size(35, 13);
            this.redScoreLabel.TabIndex = 0;
            this.redScoreLabel.Text = "label1";
            // 
            // blueScoreLabel
            // 
            this.blueScoreLabel.AutoSize = true;
            this.blueScoreLabel.Location = new System.Drawing.Point(292, 13);
            this.blueScoreLabel.Name = "blueScoreLabel";
            this.blueScoreLabel.Size = new System.Drawing.Size(35, 13);
            this.blueScoreLabel.TabIndex = 1;
            this.blueScoreLabel.Text = "label1";
            // 
            // gameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
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
    }
}