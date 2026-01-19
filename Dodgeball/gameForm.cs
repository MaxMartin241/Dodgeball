using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dodgeball
{
    public partial class gameForm : Form
    {
        Rectangle bluePlayer = new Rectangle(670, 400, 35, 50);
        Rectangle redPlayer = new Rectangle(100, 400, 35, 50);

        Rectangle bluePlayerWinner = new Rectangle(900, 0, 70, 100);
        Rectangle redPlayerWinner = new Rectangle(900, 0, 70, 100);

        Rectangle middlebar = new Rectangle(408, 0, 10, 1000);

        SolidBrush blueBrush = new SolidBrush(Color.Blue);
        SolidBrush redBrush = new SolidBrush(Color.Red);
        SolidBrush blackBrush = new SolidBrush(Color.Black);

        bool wHeld, upHeld;
        bool aPressed, dPressed;
        bool leftPressed, rightPressed;
        bool sHeld, downHeld;

        int gravity = 2;
        int jumpForce = -25;

        int redVelocityY = 0;
        int blueVelocityY = 0;

        int groundY = 400;

        int ballSpeed = 28;

        int[] ballXs = new int[7];
        int[] ballYs = new int[7];
        bool[] ballActive = new bool[7];
        int[] ballDir = new int[7]; // 1 = right, -1 = left

        bool throwDelay = true;
        Stopwatch throwDelayTime = new Stopwatch();

        bool blueThrowDelay = true;
        Stopwatch blueThrowDelayTime = new Stopwatch();

        bool gameIsDone = false;
        Stopwatch gameTime = new Stopwatch();

        int redScore = 0;
        int blueScore = 0;

        string scoreFilePath = @"C:\Users\maxwmart244\Documents\DodgBall\Highscores.txt";

        public gameForm()
        {
            InitializeComponent();
            KeyPreview = true;

            for (int i = 0; i < ballXs.Length; i++)
                ballXs[i] = 900;
        }

        private void gameForm_Load(object sender, EventArgs e)
        {
            gameTimer.Start();
            gameTime.Start();
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
            CheckForHit();
            CheckForWin();
            GameTime();
            Invalidate();
        }

        private void gameForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(redBrush, redPlayer);
            e.Graphics.FillRectangle(blueBrush, bluePlayer);
            e.Graphics.FillRectangle(blackBrush, middlebar);

            e.Graphics.FillRectangle(redBrush, redPlayerWinner);
            e.Graphics.FillRectangle(blueBrush, bluePlayerWinner);



            using (SolidBrush ballBrush = new SolidBrush(Color.DarkRed))
            {
                for (int i = 0; i < ballXs.Length; i++)
                {
                    if (ballActive[i])
                        e.Graphics.FillEllipse(ballBrush, ballXs[i], ballYs[i], 27, 27);
                }
            }
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
                    }
                    break;

                case Keys.S:
                    if (throwDelay && !sHeld)
                    {
                        sHeld = true;

                        for (int i = 1; i <= 3; i++)
                        {
                            if (!ballActive[i])
                            {
                                ballXs[i] = redPlayer.Right;
                                ballYs[i] = redPlayer.Y + 10;
                                ballDir[i] = 1;
                                ballActive[i] = true;
                                break;
                            }
                        }

                        throwDelay = false;
                        throwDelayTime.Restart();
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
                    }
                    break;

                case Keys.Down:
                    if (blueThrowDelay && !downHeld)
                    {
                        downHeld = true;

                        for (int i = 4; i <= 6; i++)
                        {
                            if (!ballActive[i])
                            {
                                ballXs[i] = bluePlayer.Left - 27;
                                ballYs[i] = bluePlayer.Y + 10;
                                ballDir[i] = -1;
                                ballActive[i] = true;
                                break;
                            }
                        }

                        blueThrowDelay = false;
                        blueThrowDelayTime.Restart();
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

        private void RedMove()
        {
            if (aPressed)
            {
                redPlayer.X -= 5;
                if (redPlayer.X < 0)
                    redPlayer.X = 0;
            }

            if (dPressed)
            {
                redPlayer.X += 5;
                if (redPlayer.X > 410 - redPlayer.Width)
                    redPlayer.X = 410 - redPlayer.Width;
            }
        }

        private void BlueMove()
        {
            if (leftPressed)
            {
                bluePlayer.X -= 5;
                if (bluePlayer.X < 417)
                    bluePlayer.X = 417;
            }

            if (rightPressed)
            {
                bluePlayer.X += 5;
                if (bluePlayer.X > 800 - bluePlayer.Width)
                    bluePlayer.X = 800 - bluePlayer.Width;
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
            if (redPlayer.Y < 0)
            {
                redPlayer.Y = 0;
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
            if (bluePlayer.Y < 0)
            {
                bluePlayer.Y = 0;
                blueVelocityY = 0;

            }
        }

        private void BallMove()
        {
            for (int i = 0; i < ballXs.Length; i++)
            {
                if (!ballActive[i]) continue;

                ballXs[i] += ballDir[i] * ballSpeed;

                if (ballXs[i] > 900 || ballXs[i] < -50)
                {
                    ballActive[i] = false;
                    ballXs[i] = 900;
                }
            }
        }

        private void DelayTime()
        {
            if (!throwDelay && throwDelayTime.ElapsedMilliseconds >= 500)
            {
                throwDelay = true;
                throwDelayTime.Reset();
            }
        }

        private void BlueDelayTime()
        {
            if (!blueThrowDelay && blueThrowDelayTime.ElapsedMilliseconds >= 500)
            {
                blueThrowDelay = true;
                blueThrowDelayTime.Reset();
            }
        }

        private void CheckForHit()
        {
            Rectangle ballRect;

            for (int i = 0; i < ballXs.Length; i++)
            {
                if (!ballActive[i]) continue;

                ballRect = new Rectangle(ballXs[i], ballYs[i], 27, 27);

                if (ballDir[i] == 1 && bluePlayer.IntersectsWith(ballRect))
                {
                    redScore++;
                    redScoreLabel.Text = redScore.ToString();
                    ballActive[i] = false;
                }

                if (ballDir[i] == -1 && redPlayer.IntersectsWith(ballRect))
                {
                    blueScore++;
                    blueScoreLabel.Text = blueScore.ToString();
                    ballActive[i] = false;
                }
            }
        }

        private void GameTime()
        {
            int seconds = (int)gameTime.Elapsed.TotalSeconds;
            gameTimeLabel.Text = seconds.ToString();
        }

        private async void CheckForWin()
        {
            if (gameTime.Elapsed.Seconds > 30)
            {
                gameTimer.Stop();
                if (redScore > blueScore)
                {
                    redPlayerWinner.X = 380;
                    redPlayerWinner.Y = 194;
                    winnerLabel.Visible = true;
                    winnerLabel.Text = "W";
                    await Task.Delay(300);
                    winnerLabel.Text += "I";
                    await Task.Delay(300);
                    winnerLabel.Text += "N";
                    await Task.Delay(300);
                    winnerLabel.Text += "N";
                    await Task.Delay(300);
                    winnerLabel.Text += "E";
                    await Task.Delay(300);
                    winnerLabel.Text += "R";

                }
                else if (redScore < blueScore)
                {
                    bluePlayerWinner.X = 380;
                    bluePlayerWinner.Y = 194;
                    winnerLabel.Visible = true;
                    winnerLabel.Text = "W";
                    await Task.Delay(300);
                    winnerLabel.Text += "I";
                    await Task.Delay(300);
                    winnerLabel.Text += "N";
                    await Task.Delay(300);
                    winnerLabel.Text += "N";
                    await Task.Delay(300);
                    winnerLabel.Text += "E";
                    await Task.Delay(300);
                    winnerLabel.Text += "R";
                }
                else
                {
                    winnerLabel.Visible = true;
                    winnerLabel.Text = "Tie";
                }
            }
        }
    }
}