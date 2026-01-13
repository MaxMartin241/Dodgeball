using System;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dodgeball
{
    public partial class gameForm : Form
    {
        Rectangle bluePlayer = new Rectangle(670, 400, 35, 50);
        Rectangle redPlayer = new Rectangle(100, 400, 35, 50);

        Rectangle middlebar = new Rectangle(408, 0, 10, 1000);

        SolidBrush blueBrush = new SolidBrush(Color.Blue);
        SolidBrush redBrush = new SolidBrush(Color.Red);
        SolidBrush blackBrush = new SolidBrush(Color.Black);

        bool wHeld = false;
        bool upHeld = false;

        bool aPressed, dPressed;
        bool leftPressed, rightPressed;

        bool sHeld = false;
        bool downHeld = false;

        int gravity = 2;
        int jumpForce = -25;

        int redVelocityY = 0;
        int blueVelocityY = 0;

        int groundY = 400;
        int roofY = 140;

        int[] redStepSizes = new int[4] { 1, 2, 3, 4};
        int[] blueStepSizes = new int[4] { 1, 2, 3, 4 };

        int ballX = 900;
        int ballY = 500;
        int ballSpeed = 6;

        bool ballNeedsMove = false;

        public gameForm()
        {
            InitializeComponent();
            KeyPreview = true;
        }

        private void gameForm_Load(object sender, EventArgs e)
        {
            gameTimer.Start();
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            RedMove();
            BlueMove();
            ApplyRedGravity();
            ApplyBlueGravity();
            BallMove();
            Invalidate();
        }

        private void gameForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(redBrush, redPlayer);
            e.Graphics.FillRectangle(blueBrush, bluePlayer);
            e.Graphics.FillRectangle(blackBrush, middlebar);

            using (SolidBrush ballBrush = new SolidBrush(Color.DarkRed))
            {
                DrawBall(e.Graphics, ballBrush, ballX, ballY);
                DrawBall(e.Graphics, ballBrush, 200, 150);
                
            }
            
        }

        private void DrawBall(Graphics g, SolidBrush brush, int x, int y)
        {
            g.FillEllipse(brush, x, y, 27, 27);
        }

        private void gameForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                // RED
                case Keys.A: aPressed = true; break;
                case Keys.D: dPressed = true; break;

                case Keys.W:
                    if (!wHeld)
                    {
                        redVelocityY = jumpForce;
                        wHeld = true;
                        if (redPlayer.Y < roofY)
                        {
                            redPlayer.Y = roofY;
                        }
                    }
                    break;

                case Keys.S:
                    if (!sHeld)
                    {
                        sHeld = true;
                        ballX = redPlayer.X;
                        ballY = redPlayer.Y;
                        BallMove();
                        ballNeedsMove = true;
                    }
                    break;
                // BLUE
                case Keys.Left: leftPressed = true; break;
                case Keys.Right: rightPressed = true; break;

                case Keys.Up:
                    if (!upHeld)
                    {
                        blueVelocityY = jumpForce;
                        upHeld = true;
                        if (bluePlayer.Y < roofY)
                        {
                            bluePlayer.Y = roofY;
                        }
                    }
                    break;
            }
        }

        private void gameForm_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A: aPressed = false; break;
                case Keys.D: dPressed = false; break;
                case Keys.W: wHeld = false; break;
                case Keys.S: sHeld = false; break;

                case Keys.Left: leftPressed = false; break;
                case Keys.Right: rightPressed = false; break;
                case Keys.Up: upHeld = false; break;
            }
        }

        private async Task RedMove()
        {
            if (aPressed)
            {
                for (int i = 0; i < redStepSizes.Length; i++)
                {
                    if (redPlayer.X <= 0)
                    {
                        redPlayer.X = 0;
                        break;
                    }
                    redPlayer.X -= redStepSizes[i];
                    await Task.Delay(5);
                }
            }

            if (dPressed)
            {
                for (int i = 0; i < redStepSizes.Length; i++)
                {
                    if (redPlayer.X >= 410 - redPlayer.Width)
                    {
                        redPlayer.X = 410 - redPlayer.Width;
                        break;
                    }
                    redPlayer.X += redStepSizes[i];
                    await Task.Delay(5);
                }
            }
        }

        private async void BlueMove()
        {
            if (leftPressed)
            {
                for (int i = 0; i < blueStepSizes.Length; i++)
                {
                    if (bluePlayer.X <= 417)
                    {
                        bluePlayer.X = 417;
                        break;
                    }
                    bluePlayer.X -= blueStepSizes[i];
                    await Task.Delay(5);
                }
            }

            if (rightPressed)
            {
                for (int i = 0; i < blueStepSizes.Length; i++)
                {
                    if (bluePlayer.X >= 800 - bluePlayer.Width)
                    {
                        bluePlayer.X = 800 - bluePlayer.Width;
                        break;
                    }
                    bluePlayer.X += blueStepSizes[i];
                    await Task.Delay(5);
                }
            }
        }
        

        private void ApplyRedGravity()
        {
            redVelocityY += gravity;
            redPlayer.Y += redVelocityY;

            if (redPlayer.Y >= groundY)
            {
                redPlayer.Y = groundY;
                redVelocityY = 0;
            }
        }

        private void ApplyBlueGravity()
        {
            blueVelocityY += gravity;
            bluePlayer.Y += blueVelocityY;

            if (bluePlayer.Y >= groundY)
            {
                bluePlayer.Y = groundY;
                blueVelocityY = 0;
            }

        }


        private async Task BallMove()
        {
            if (ballNeedsMove)
            {
                for (int i = 0; i < ((800 - ballX)/ballSpeed) + 5; i++)
                {
                    ballX += ballSpeed;
                    await Task.Delay(15);
                }
                ballNeedsMove = false;
            }
        }
    }
}