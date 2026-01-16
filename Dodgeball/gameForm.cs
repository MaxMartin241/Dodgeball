using System;
using System.Diagnostics;
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

        int[] redStepSizes = new int[4] { 1, 2, 3, 4 };
        int[] blueStepSizes = new int[4] { 1, 2, 3, 4 };

        int ballX = 900;
        int ballY = 500;
        int ballSpeed = 20;

        bool ballNeedsMove = false;

        int[] balls = new int[7] { 1, 2, 3, 4, 5, 6, 7 };
        int[] ballXs = new int[7] { 900, 900, 900, 900, 900, 900, 900 };
        int[] ballYs = new int[7] { 500, 500, 500, 500, 500, 500, 500 };

        bool blueBallNeedsMove = false;

        bool throwDelay = true;
        Stopwatch throwDelayTime = Stopwatch.StartNew();

        bool blueThrowDelay = true;
        Stopwatch blueThrowDelayTime = Stopwatch.StartNew();

        int redScore = 0;
        int blueScore = 0;

        int ballThatHit = 0;

        bool redIsHit = false;

        int blueBallThatHit = 0;

        bool blueIsHit = false;




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
            DelayTime();
            BlueDelayTime();
            CheackForHit();
            Invalidate();
        }

        private void gameForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(redBrush, redPlayer);
            e.Graphics.FillRectangle(blueBrush, bluePlayer);
            e.Graphics.FillRectangle(blackBrush, middlebar);

            using (SolidBrush ballBrush = new SolidBrush(Color.DarkRed))
            {
                for (int i = 0; i < balls.Length; i++)
                {
                    DrawBall(e.Graphics, ballBrush, ballXs[i], ballYs[i]);
                }
                DrawBall(e.Graphics, ballBrush, ballX, ballY);

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
                    if (throwDelay)
                    {
                        if (!sHeld)
                        {
                            sHeld = true;
                            if (ballXs[1] == 900)
                            {
                                ballXs[1] = redPlayer.X;
                                ballYs[1] = redPlayer.Y;
                            }
                            else if (ballXs[2] == 900)
                            {
                                ballXs[2] = redPlayer.X;
                                ballYs[2] = redPlayer.Y;
                            }
                            else
                            {
                                ballXs[3] = redPlayer.X;
                                ballYs[3] = redPlayer.Y;
                            }
                            ballNeedsMove = true;
                            throwDelay = false;
                            throwDelayTime.Start();
                            BallMove();
                        }
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

                case Keys.Down:
                    if (blueThrowDelay)
                    {
                        if (!downHeld)
                        {
                            downHeld = true;
                            if (ballXs[4] == 900)
                            {
                                ballXs[4] = bluePlayer.X;
                                ballYs[4] = bluePlayer.Y;
                            }
                            else if (ballXs[5] == 900)
                            {
                                ballXs[5] = bluePlayer.X;
                                ballYs[5] = bluePlayer.Y;
                            }
                            else
                            {
                                ballXs[6] = bluePlayer.X;
                                ballYs[6] = bluePlayer.Y;
                            }
                            blueBallNeedsMove = true;
                            blueThrowDelay = false;
                            blueThrowDelayTime.Start();
                            BallMove();
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
                case Keys.Down: downHeld = false; break;
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


        private async void BallMove()
        {
            if (ballNeedsMove)
            {
                throwDelay = false;
                if (Convert.ToInt16(ballXs[1]) == redPlayer.X)
                {
                    if (redIsHit == true && ballThatHit == 1)
                    {
                        while (ballXs[1] < 900)
                        {

                            ballXs[1] += ballSpeed;
                            await Task.Delay(5);
                        }
                    }
                    ballXs[1] = 900;
                }
                if (Convert.ToInt16(ballXs[2]) == redPlayer.X)
                {
                    if (redIsHit == true && ballThatHit == 2)
                    {
                        while (ballXs[2] < 900)
                        {
                            ballXs[2] += ballSpeed;
                            await Task.Delay(5);
                        }
                    }
                    ballXs[2] = 900;
                }
                if (Convert.ToInt16(ballXs[3]) == redPlayer.X)
                {
                    if (redIsHit == true && ballThatHit == 3)
                    {
                        while (ballXs[3] < 900)
                        {
                            ballXs[3] += ballSpeed;
                            await Task.Delay(5);
                        }
                    }
                    ballXs[3] = 900;
                }

                ballNeedsMove = false;
            }


            if (blueBallNeedsMove)
            {
                blueThrowDelay = false;
                if (Convert.ToInt16(ballXs[4]) == bluePlayer.X)
                {
                    if (blueIsHit == true && blueBallThatHit == 4)
                    {
                        while (ballXs[4] > -50)
                        {
                            ballXs[4] -= ballSpeed;
                            await Task.Delay(5);
                        }
                    }
                    ballXs[4] = 900;
                }
                if (Convert.ToInt16(ballXs[5]) == bluePlayer.X)
                {
                    if (blueIsHit == true && blueBallThatHit == 5)
                    {
                        while (ballXs[5] > 50)
                        {
                            ballXs[5] -= ballSpeed;
                            await Task.Delay(5);
                        }
                    }
                    ballXs[5] = 900;
                }
                if (Convert.ToInt16(ballXs[6]) == bluePlayer.X)
                {
                    if (blueIsHit == true && blueBallThatHit == 6)
                    {
                        while (ballXs[6] > 50)
                        {
                            ballXs[6] -= ballSpeed;
                            await Task.Delay(5);
                        }
                    }
                    ballXs[6] = 900;
                }

                blueBallNeedsMove = false;
            }
        }

        private void DelayTime()
        {
            if (!throwDelay && throwDelayTime.ElapsedMilliseconds >= 500)
            {
                throwDelay = true;
                throwDelayTime.Stop();
                throwDelayTime.Reset();
                throwDelay = true;
            }
        }
        private void BlueDelayTime()
        {
            if (!blueThrowDelay && blueThrowDelayTime.ElapsedMilliseconds >= 500)
            {
                blueThrowDelay = true;
                blueThrowDelayTime.Stop();
                blueThrowDelayTime.Reset();
                blueThrowDelay = true;
            }
        }
        private void CheackForHit()
        {
            int ballWidth = 27;
            int ballHeight = 27;
            redIsHit = false;
            blueIsHit = false;


            for (int i = 0; i < balls.Length; i++)
            {
                Rectangle ballRect = new Rectangle(ballXs[i], ballYs[i], ballWidth, ballHeight);

                if (redPlayer.IntersectsWith(ballRect))
                {
                    // Hit detected!
                    redScore++;
                    redScoreLabel.Text = redScore.ToString();
                    ballThatHit = i;
                    // Reset the ball off-screen (optional)
                    redIsHit = true;
                    ballXs[i] = 900;

                }
                if (bluePlayer.IntersectsWith(ballRect))
                {
                    blueScore++;
                    blueScoreLabel.Text = blueScore.ToString();
                    blueBallThatHit = i;
                    // Reset the ball off-screen (optional)
                    blueIsHit = true;
                    ballXs[i] = 900;
                }
            }
        }
    }
}