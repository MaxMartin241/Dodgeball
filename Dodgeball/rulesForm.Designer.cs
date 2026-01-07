namespace Dodgeball
{
    partial class rulesForm
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
            this.leftTextbox = new System.Windows.Forms.Label();
            this.rightTextbox = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(390, -9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 471);
            this.label1.TabIndex = 0;
            // 
            // leftTextbox
            // 
            this.leftTextbox.AutoSize = true;
            this.leftTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.leftTextbox.Location = new System.Drawing.Point(12, 41);
            this.leftTextbox.Name = "leftTextbox";
            this.leftTextbox.Size = new System.Drawing.Size(106, 24);
            this.leftTextbox.TabIndex = 1;
            this.leftTextbox.Text = "Left player";
            // 
            // rightTextbox
            // 
            this.rightTextbox.AutoSize = true;
            this.rightTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rightTextbox.Location = new System.Drawing.Point(425, 41);
            this.rightTextbox.Name = "rightTextbox";
            this.rightTextbox.Size = new System.Drawing.Size(66, 24);
            this.rightTextbox.TabIndex = 2;
            this.rightTextbox.Text = "label3";
            // 
            // rulesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.rightTextbox);
            this.Controls.Add(this.leftTextbox);
            this.Controls.Add(this.label1);
            this.Name = "rulesForm";
            this.Text = "rulesForm";
            this.Load += new System.EventHandler(this.rulesForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label leftTextbox;
        private System.Windows.Forms.Label rightTextbox;
    }
}