//Dodgeball
//Max Martin
//
//ICS3U Finel Project

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dodgeball
{
    public partial class gameForm : Form
    {



        //-------------------set up - varibels-------------------

        Rectangle bluePlayer = new Rectangle(670, 400, 35, 50);
        Rectangle redPlayer = new Rectangle(100, 400, 35, 50);//Players

        Rectangle bluePlayerWinner = new Rectangle(900, 0, 70, 100);
        Rectangle redPlayerWinner = new Rectangle(900, 0, 70, 100);//winners

        Rectangle middlebar = new Rectangle(408, 0, 10, 1000);//bar to divide screen

        SolidBrush blueBrush = new SolidBrush(Color.Blue);
        SolidBrush redBrush = new SolidBrush(Color.Red);
        SolidBrush blackBrush = new SolidBrush(Color.Black);//colors

        bool wHeld, upHeld;
        bool aPressed, dPressed;
        bool leftPressed, rightPressed;
        bool sHeld, downHeld;//basic controlls

        int gravity = 2;
        int jumpForce = -25;//jumping varibels

        int redVelocityY = 0;
        int blueVelocityY = 0;//if this ever goes above zero the player will start to move up

        int groundY = 400;//ground

        int ballSpeed = 28;

        int[] ballXs = new int[7];
        int[] ballYs = new int[7];
        bool[] ballActive = new bool[7];
        int[] ballDir = new int[7]; //ball controll and orginization

        bool throwDelay = true;
        Stopwatch throwDelayTime = new Stopwatch();

        bool blueThrowDelay = true;
        Stopwatch blueThrowDelayTime = new Stopwatch();//time between throws

        bool gameIsDone = false;
        Stopwatch gameTime = new Stopwatch();//game langth

        int redScore = 0;
        int blueScore = 0;//score

        string scoreFilePath = @"C:\Users\maxwmart244\Documents\DodgBall\Highscores.txt";//file handiling

        List<Player> players = new List<Player>()
        {
            new Player { Name = "Test", Score = 100 },
        };










        //-------------------set up -----------------------------

        private void gameForm_Paint(object sender, PaintEventArgs e)//creating balls, players, middle bar visibel bodey 
        {
            e.Graphics.FillRectangle(redBrush, redPlayer);
            e.Graphics.FillRectangle(blueBrush, bluePlayer);
            e.Graphics.FillRectangle(blackBrush, middlebar);

            e.Graphics.FillRectangle(redBrush, redPlayerWinner);
            e.Graphics.FillRectangle(blueBrush, bluePlayerWinner);



            using (SolidBrush ballBrush = new SolidBrush(Color.DarkRed))//creates balls 
            {
                for (int i = 0; i < ballXs.Length; i++)
                {
                    if (ballActive[i])
                        e.Graphics.FillEllipse(ballBrush, ballXs[i], ballYs[i], 27, 27);
                }
            }
        }



        public gameForm()
        {
            InitializeComponent();
            KeyPreview = true;

            for (int i = 0; i < ballXs.Length; i++)
                ballXs[i] = 900;//simple set up
        }

        public class Player
        {
            public string Name { get; set; }
            public int Score { get; set; }//for players and scores
        }


        private void gameForm_Load(object sender, EventArgs e)//checking varibels and starting game engine
        {
            gameTimer.Start();
            gameTime.Start();
            nameInputBox.Visible = false;
            saveWinButton.Visible = false;
        }









        //--------------big game timer-----------------

        private void gameTimer_Tick(object sender, EventArgs e)//lots of things to check every tick
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









        //----------------------player input------------------

        private void gameForm_KeyDown(object sender, KeyEventArgs e)//key controlls
        {
            switch (e.KeyCode)
            {
                // RED
                case Keys.A: aPressed = true; break;
                case Keys.D: dPressed = true; break;//left right

                case Keys.W://up
                    if (!wHeld)
                    {
                        redVelocityY = jumpForce;
                        wHeld = true;
                    }
                    break;

                case Keys.S://throw
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
                case Keys.Right: rightPressed = true; break;//left right

                case Keys.Up://jump
                    if (!upHeld)
                    {
                        blueVelocityY = jumpForce;
                        upHeld = true;
                    }
                    break;

                case Keys.Down://throw
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

        private void gameForm_KeyUp(object sender, KeyEventArgs e)//when relese button pressed = false
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









        //----------player output----------------

        private void RedMove()//left right with collision
        {
            if (aPressed)
            {
                redPlayer.X -= 9;
                if (redPlayer.X < 0)
                    redPlayer.X = 0;
            }

            if (dPressed)
            {
                redPlayer.X += 9;
                if (redPlayer.X > 410 - redPlayer.Width)
                    redPlayer.X = 410 - redPlayer.Width;
            }
        }

        private void BlueMove()//left right with collision
        {
            if (leftPressed)
            {
                bluePlayer.X -= 9;
                if (bluePlayer.X < 417)
                    bluePlayer.X = 417;
            }

            if (rightPressed)
            {
                bluePlayer.X += 9;
                if (bluePlayer.X > 800 - bluePlayer.Width)
                    bluePlayer.X = 800 - bluePlayer.Width;
            }
        }


        private void ApplyRedGravity()//jump and falling with collision
        {
            redVelocityY += gravity;
            redPlayer.Y += redVelocityY;//moves up

            if (redPlayer.Y >= groundY)//if hit roof or ground
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

        private void ApplyBlueGravity()//jump and falling with collision
        {
            blueVelocityY += gravity;
            bluePlayer.Y += blueVelocityY;//moves up

            if (bluePlayer.Y >= groundY)//if hits roof or ground
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









        //----------ball Output------------------

        private void BallMove()//move ball across screen
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









        //-----------Time Controll----------------

        private void DelayTime()//wait between throws
        {
            if (!throwDelay && throwDelayTime.ElapsedMilliseconds >= 500)
            {
                throwDelay = true;
                throwDelayTime.Reset();
            }
        }

        private void BlueDelayTime()//wait beetween throws
        {
            if (!blueThrowDelay && blueThrowDelayTime.ElapsedMilliseconds >= 500)
            {
                blueThrowDelay = true;
                blueThrowDelayTime.Reset();
            }
        }

        private void GameTime()
        {
            int seconds = (int)gameTime.Elapsed.TotalSeconds;
            gameTimeLabel.Text = seconds.ToString();
        }








        //------------hiting and winning------------------

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

        private void CheckForWin()
        {
            if (gameIsDone) return;

            if (gameTime.Elapsed.TotalSeconds > 30)
            {
                gameIsDone = true;
                gameTimer.Stop();
                if (redScore > blueScore)
                {
                    redPlayerWinner.X = 380;
                    redPlayerWinner.Y = 194;

                    winnerLabel.Visible = true;
                    winnerLabel.Text = "WINNER";

                    if (redScore > 10)
                    {
                        nameInputBox.Visible = true;
                        saveWinButton.Visible = true;
                    }
                }
                else if (blueScore > redScore)
                {
                    bluePlayerWinner.X = 380;
                    bluePlayerWinner.Y = 194;

                    winnerLabel.Visible = true;
                    winnerLabel.Text = "WINNER";

                    if (blueScore > 10)
                    {
                        nameInputBox.Visible = true;
                       saveWinButton.Visible = true;
                    }
                }
                else
                {
                    winnerLabel.Visible = true;
                    winnerLabel.Text = "Tie";
                }
            }
        }








        //-------------Finel save and close------------------

        private void saveWinButton_Click(object sender, EventArgs e)//save highscore
        {
            string playerName = nameInputBox.Text.Trim();

            if (playerName == "" || playerName == "Enter name")
                playerName = "Player";

            int finalScore;

            if (redScore > blueScore)
                finalScore = redScore;
            else
                finalScore = blueScore;

            // Only save if score > 10
            if (finalScore > 10)
            {
                using (StreamWriter writer = new StreamWriter(scoreFilePath, true))
                {
                    writer.WriteLine(playerName);
                    writer.WriteLine(finalScore);//writes score and name in the file
                }
            }

            // Close the game form after saving
            this.Close();
        }
    }
}
