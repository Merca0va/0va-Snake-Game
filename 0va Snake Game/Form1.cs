using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _0va_Snake_Game
{
    public partial class Form1 : Form
    {

        private List<Round> snake = new List<Round>();
        private Round snack = new Round();
        public Form1()
        {
            InitializeComponent();
            new Setting();
            GameTimer.Interval = 1000 / Setting.Speed;
            GameTimer.Start();

            StartGame();
        }

        private void UpdateScreen(object sender, EventArgs e)
        {
            //This code is linked to the gameTimer and each will run at each tick.

            if (Setting.GameOver == true)
            {
                if (Input.KeyPress(Keys.Space))
                {
                    StartGame();
                }
            }

            else
            {
                if (Input.KeyPress(Keys.Right) && Setting.directions != Directions.Left)
                {
                    Setting.directions = Directions.Right;
                }
                else if (Input.KeyPress(Keys.Left) && Setting.directions != Directions.Right)
                {
                    Setting.directions = Directions.Left;
                }
                else if (Input.KeyPress(Keys.Up) && Setting.directions != Directions.Down)
                {
                    Setting.directions = Directions.Up;
                }
                else if (Input.KeyPress(Keys.Down) && Setting.directions != Directions.Up)
                {
                    Setting.directions = Directions.Down;
                }

                PlayerMove();
                
            }

            Field.Invalidate(); // Update our pictureBox.
        }

        private void PlayerMove()
        {
            for (int i = snake.Count - 1; i >= 0; i--)
            {
                if (i == 0)
                {
                    switch(Setting.directions)
                    {
                        case Directions.Right: snake[i].X++;
                            break;

                        case Directions.Left: snake[i].X--;
                            break;

                        case Directions.Up: snake[i].Y--;
                            break;

                        case Directions.Down: snake[i].Y++;
                            break;
                    }

                    // Restrict the snake movements inside the field area.
                    int X_Max = Field.Size.Width / Setting.Width;
                    int Y_Max = Field.Size.Height / Setting.Height;

                    if (snake[i].X < 0 || snake[i].Y < 0 || snake[i].X > X_Max || snake[i].Y > Y_Max)
                    {
                        Dead(); // The player looses if the snake reaches the edge of the field.
                    }

                    for (int j = 1; j < snake.Count; j++)
                    {
                        if (snake[i].X == snake[j].X && snake[i].Y == snake[j].Y)
                        {
                            Dead(); // The player looses if the snake collide with its own body.
                        }
                    }

                    if(snake[0].X == snack.X && snake[0].Y == snack.Y)
                    {
                        consume(); // Execute the consummation if the head collide with the snack.
                    }                    
                }

                else
                {
                    //If there is no collision, the snake keeps moving.
                    snake[i].X = snake[i - 1].X;
                    snake[i].Y = snake[i - 1].Y;
                }
            }
        }

        private void keyIsDown(object sender, KeyEventArgs e)
        {
            // Trigger the change state from the Input class.
            Input.ChangeState(e.KeyCode, true);
        }

        private void keyIsUp(object sender, KeyEventArgs e)
        {
            Input.ChangeState(e.KeyCode, false);
        }

        private void UpdateGraphics(object sender, PaintEventArgs e)
        {
            // Allows to see the snake moving and growing.

            Graphics view = e.Graphics;

            if(Setting.GameOver == false)
            {
                Brush snake_color;

                for (int i = 0; i < snake.Count; i++) // Checks the snake parts.
                {
                    if (i == 0)
                    {
                        snake_color = Brushes.Red; // Set the color of the head
                    }
                    else
                    {
                        snake_color = Brushes.Black; // Set the color of the body parts.
                    }

                    //Drawing Snake's parts.
                    view.FillEllipse(snake_color, new Rectangle(snake[i].X * Setting.Width,
                                                                snake[i].Y * Setting.Height,
                                                                Setting.Width, Setting.Height));

                    //Drawing the snacks.
                    view.FillEllipse(Brushes.Orange, new Rectangle(snack.X * Setting.Width,
                                                                   snack.Y * Setting.Height,
                                                                   Setting.Width, Setting.Height));
                }
            }
            else
            {
                // When the player looses.
                string endGame = "GAME OVER \n" + "Your Final Score is " + Setting.Score + "\n Press Space to Restart \n";
                label3.Text = endGame;
                label3.Visible = true;
            }
        }

        private void StartGame()
        {
            // Setting the default items when the game is launched.
            label3.Visible = false;
            new Setting();
            snake.Clear();
            Round head = new Round { X = 10, Y = 5 };
            snake.Add(head);
            label2.Text = Setting.Score.ToString();

            RespawnSnack();
        }

        private void RespawnSnack()
        {

            int X_Max = Field.Size.Width / Setting.Width; // Set the maximum X position.
            int Y_Max = Field.Size.Height / Setting.Height; // Set the maximum Y position.
            Random random = new Random();
            snack = new Round { X = random.Next(0, X_Max), Y = random.Next(0, Y_Max) }; // Create a new snack at a random location.
        }

        private void consume ()
        {
            Round length = new Round { X = snake[snake.Count - 1].X, Y = snake[snake.Count - 1].Y};
            snake.Add(length);
            Setting.Score = Setting.Score + Setting.Points; // Increase the score value.
            label2.Text = Setting.Score.ToString();

            RespawnSnack();
        }

        private void Dead()
        {
            Setting.GameOver = true;
        }
    }
}
