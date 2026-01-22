using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Dodgeball
{
    public partial class Leaderbored : Form
    {
        string filePath = @"C:\Users\maxwmart244\Documents\DodgBall\Highscores.txt";//file to pull from

        List<Player> players = new List<Player>()
        {
           // new Player { Name = "Test", Score = 100 },
        };

        public Leaderbored()
        {
            InitializeComponent();
        }
        public class Player
        {
            public string Name { get; set; }
            public int Score { get; set; }
        }
        private void Leaderbored_Load(object sender, EventArgs e)//shows everything when loed
        {
            if (!File.Exists(filePath))
                return;

            string[] lines = File.ReadAllLines(filePath);

            for (int i = 0; i < lines.Length - 1; i += 2)
            {
                string name = lines[i];
                int score = int.Parse(lines[i + 1]);

                players.Add(new Player
                {
                    Name = name,
                    Score = score
                });
            }

            players = players//Sort best to worst
                .OrderByDescending(p => p.Score)
                .ToList();

            leaderboredLabel.Text = "";

            for (int i = 0; i < players.Count; i++)
            {
                leaderboredLabel.Text +=
                    (i + 1) + ".    " +
                    players[i].Name + " - " +
                    players[i].Score + Environment.NewLine;

            }
        }
    }
}