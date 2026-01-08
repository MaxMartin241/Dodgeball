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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void rulesButton_Click(object sender, EventArgs e)
        {
            rulesForm rules = new rulesForm();
            rules.Show();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            gameForm game = new gameForm();
            game.Show();
        }
    }
}
