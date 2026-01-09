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
    public partial class gameForm : Form
    {
        Rectangle bluePlayer = new Rectangle(670, 400, 35, 50);
        Rectangle redPlayer = new Rectangle(100, 400, 35, 50);

        Rectangle redPlayerBounds = new Rectangle(400, 200, 27, 27);

        Rectangle middlebar = new Rectangle(408, 0, 10, 1000);

        SolidBrush blueBrush = new SolidBrush(Color.Blue);
        SolidBrush redBrush = new SolidBrush(Color.Red);

        SolidBrush ballredBrush = new SolidBrush(Color.DarkRed);

        SolidBrush blackBrush = new SolidBrush(Color.Black);

        Boolean wPressed = false;//red control buttons

        Boolean sPressed = false;//red throw

        Boolean aPressed = false;
        Boolean dPressed = false;

        Boolean upPressed = false;//blue control buttons

        Boolean downPressed = false;//blue throw

        Boolean leftPressed = false;
        Boolean rightPressed = false;

        int redStepSize = 5;
        int blueStepSize = 5;

        int[] redjumpSpeeds = new int[5] { -15, -11, -8, -5, -2 };
        int[] bluejumpSpeeds = new int[5] { -15, -11, -8, -5, -2 };

        bool redIsJumping = false;
        bool redCanJump = true;

        bool blueIsJumping = false;
        bool blueCanJump = true;



        public gameForm()
        {
            InitializeComponent();
            this.KeyPreview = true;
        }

        private void gameForm_Load(object sender, EventArgs e)
        {
            gameTimer.Start();
        }

        private void gameForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(redBrush, redPlayer);
            e.Graphics.FillRectangle(blueBrush, bluePlayer);

            e.Graphics.FillRectangle(blackBrush, middlebar);

            using (SolidBrush ballRedBrush = new SolidBrush(Color.Red))//calling dodgeball to make 2
            {
                Dodgeball(e.Graphics, ballRedBrush, 100, 150);
                Dodgeball(e.Graphics, ballRedBrush, 200, 150);

            }


        }
        void Dodgeball(Graphics g, SolidBrush ballRedBrush, int x, int y)//creating as meany balls as i want to put anywhere
        {
            int size = 27; // diameter of the ball

            g.FillEllipse(ballRedBrush, x, y, size, size);//empty values for ball
        }



        private void gameTimer_Tick(object sender, EventArgs e)
        {
            redMove();
            blueMove();
            Invalidate();

        }


        private void redMove()
        {
            if (aPressed && redPlayer.X > 0)
                redPlayer.X -= redStepSize;

            if (dPressed && redPlayer.X < 406 - redPlayer.Width)
                redPlayer.X += redStepSize;
        }
        private void blueMove()
        {
            //run into walls
            if (leftPressed && bluePlayer.X > 415)
                bluePlayer.X -= blueStepSize;

            if (rightPressed && bluePlayer.X < Width - bluePlayer.Width - 20)
                bluePlayer.X += blueStepSize;
        }

        private void gameForm_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W: wPressed = false; break;
                case Keys.S: sPressed = false; break;
                case Keys.A: aPressed = false; break;
                case Keys.D: dPressed = false; break;

                case Keys.Up: upPressed = false; break;
                case Keys.Down: downPressed = false; break;
                case Keys.Left: leftPressed = false; break;
                case Keys.Right: rightPressed = false; break;
            }
        }

        private void gameForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                // RED
                case Keys.W:
                    if (redCanJump && !redIsJumping)
                    {
                        redCanJump = false;
                        redIsJumping = true;
                        _ = redJump(); // jump ONCE
                    }
                    break;

                case Keys.S: sPressed = true; break;
                case Keys.A: aPressed = true; break;
                case Keys.D: dPressed = true; break;

                // BLUE
                case Keys.Up:
                    if (blueCanJump && !blueIsJumping)
                    {
                        blueCanJump = false;
                        blueIsJumping = true;
                        _ = blueJump(); // jump ONCE
                    }
                    break;
                case Keys.Down: downPressed = true; break;
                case Keys.Left: leftPressed = true; break;
                case Keys.Right: rightPressed = true; break;
            }
        }
        private async Task redJump()
        {
            // jump up
            for (int i = 0; i < redjumpSpeeds.Length; i++)
            {
                redPlayer.Y += redjumpSpeeds[i];
                await Task.Delay(15);
            }

            // fall down
            Array.Reverse(redjumpSpeeds);
            for (int i = 0; i < redjumpSpeeds.Length; i++)
            {
                redPlayer.Y -= redjumpSpeeds[i];
                await Task.Delay(10);
            }
            Array.Reverse(redjumpSpeeds);

            redIsJumping = false;
            redCanJump = true;
        }
        private async Task blueJump()
        {
            // jump up
            for (int i = 0; i < bluejumpSpeeds.Length; i++)
            {
                bluePlayer.Y += bluejumpSpeeds[i];
                await Task.Delay(15);
            }

            // fall down
            Array.Reverse(bluejumpSpeeds);
            for (int i = 0; i < bluejumpSpeeds.Length; i++)
            {
                bluePlayer.Y -= bluejumpSpeeds[i];
                await Task.Delay(10);
            }
            Array.Reverse(bluejumpSpeeds);

            blueIsJumping = false;
            blueCanJump = true;
        }



        private void redThrow()
        {

        }
        private void blueThrow()
        {

        }

        private void gameForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }
    }
}
