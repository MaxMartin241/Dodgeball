using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dodgeball
{
    public partial class rulesForm : Form
    {
        public rulesForm()
        {
            InitializeComponent();
        }

        private void rulesForm_Load(object sender, EventArgs e)
        {
            leftTextbox.Text = "Left player\r\n\n\nuse W to jump\r\nA and D to move back and forth\r\nuse S to throw the ball";
            leftTextbox.Text += "\n\n\n\nYou have 30 seconds to score\n as many points as possible.\n You can only stay on your side";
            leftTextbox.Text += "\n\nIf you score over ten and win\n you make it on to the leaderbored!!";
            rightTextbox.Text = "Right player\r\n\r\nuse UP to jump\r\nLEFT and RIGHT for back and forth\r\nuse DOWN to throw the ball";
        }
    }
}
