namespace Dodgeball
{
    partial class Leaderbored
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
            this.leaderboredLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // leaderboredLabel
            // 
            this.leaderboredLabel.AutoSize = true;
            this.leaderboredLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.leaderboredLabel.Location = new System.Drawing.Point(13, 13);
            this.leaderboredLabel.Name = "leaderboredLabel";
            this.leaderboredLabel.Size = new System.Drawing.Size(44, 16);
            this.leaderboredLabel.TabIndex = 0;
            this.leaderboredLabel.Text = "label1";
            // 
            // Leaderbored
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.leaderboredLabel);
            this.Name = "Leaderbored";
            this.Text = "Leaderbored";
            this.Load += new System.EventHandler(this.Leaderbored_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label leaderboredLabel;
    }
}