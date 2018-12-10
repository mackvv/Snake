using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mackenzie_Van_Vliet___Final_Project
{


    public partial class GameForm : Form
    {
        public GameForm()
        {
            InitializeComponent();
        }

        public static int difficulty;
        static int timerTick;

        //rectangle array that holds snake body and head
        Rectangle[] recArray = new Rectangle[1000];
        //rock array
        Rectangle[] rockArray = new Rectangle[4];   
 
        
        //rectangle guides 
        Rectangle rect = new Rectangle(0, 0, 20, 20);
        Rectangle food = new Rectangle(0, 0, 10, 10);
   
        static int x = 20;      //snake head x
        static int y = 20;      //snake head y 
        static int xy = 0;      //up/down =2 or left/right = 1, 0 = not moving
        static int incret;      //how many spaces snake will move
        
        static int lastY;       //holds onto last body/head Y cooridnate 
        static int lastX;       //holds onto last body/head X cooridnate

        static int foodX;       //foodx coordinate 
        static int foodY;       //foody coordinate

        
        static int foodEaten = 1;   //1 = food eater, 0 = food not eaten 
        static int snakeLength = 2; //snake starts with head only

        static int points;          //players points

        static int rocksToDraw;     //how many rocks will be drawn 

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //sets timer to correct difficulty
        private void GameForm_Load(object sender, EventArgs e)
        {
            if (difficulty == 1)
            {
                gameTimer.Interval = 35;

            }
            else if (difficulty == 2)
            {
                gameTimer.Interval = 25;

            }
            else if (difficulty == 3)
            {
                gameTimer.Interval = 15;

            }



        }

        //timer tick event
        private void gameTimer_Tick(object sender, EventArgs e)
        {
            timerTick++;

            //gets new x/y pos for head
            if (xy == 1)
            {

                x = x + incret;
            }

            else if (xy == 2)
            {

                y = y + incret;
            }

            //if x/y collides with wall 
            if (x < 0 || y < 0 || y >= 480 || x >= 480)
            {
                GameOver();

            }

            //draws screen
            DrawIt(x, y);


        }

        //draws screen 
        private void DrawIt(int x, int y)
        {
            //creates graphic screen in picture box
            System.Drawing.Graphics graphics = pbGame.CreateGraphics();
            //clears screen
            graphics.Clear(Color.Pink);

            //holds values until assigned to lastX, lastY
            int currentX;
            int currentY;


            //postions snake
            for (int i = 1; i < snakeLength; i++)
            {
                //if head
                if (i == 1)
                {
                    //stores last snake head position bc this will be the next circles position
                    lastX = recArray[i].X;
                    lastY = recArray[i].Y;

                    //places snake head at new postion 
                    rect.X = x;
                    rect.Y = y;

                    recArray[i] = rect;

                }
                else
                {
                    //stores current body position 
                    currentX = recArray[i].X;
                    currentY = recArray[i].Y;

                    //makes body position, last snake position 
                    rect.X = lastX;
                    rect.Y = lastY;

                    //draws body piece
                    recArray[i] = rect;
                    graphics.FillEllipse(new SolidBrush(Color.Green), recArray[i]);

                    //moves current body position to lastpos
                    lastX = currentX;
                    lastY = currentY;

                    //if snake piece is greater than 6 (becasue if less than 6 the piece already intercects with head)
                    if (i > 6)
                    {
                        //if snake collides into iteself
                        if (collision(recArray[i].X, recArray[i].Y, 10, recArray[1].X, recArray[1].Y, 10) == true)
                        {
                            //ends game
                            GameOver();
                        }
                    }
                }
            }

            //draws head (draws head after draws body so body pieces dont cover head)
            graphics.FillEllipse(new SolidBrush(Color.DarkGreen), recArray[1]);

            DrawFood();     //draws food

            FormatRock();   //draws rocks


            //if snake eats food
            if (collision(foodX, foodY, 5, recArray[1].X, recArray[1].Y, 10) == true)
            {
                //increase snake length
                snakeLength = snakeLength + 6;
                foodEaten = 1;

                //sets players points and displays them 
                points = points + 100;
                lblPoints.Text = "POINTS: " + points;


            }

            

            for (int i = 1; i <= rocksToDraw; i++)
            {
                //if snake hits rock, game over
                if (collision(rockArray[i].X, rockArray[i].Y, 25, recArray[1].X, recArray[1].Y, 10) == true)
                {
                    GameOver();

                }
            }





        }


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //draws food
        private void DrawFood()
        {
            //draws new food on screen with new X and Y if food is gone
            if (foodEaten == 1)
            {
                foodEaten = 0;
                Food();


            }
            else
            {
                //draws food 
                System.Drawing.Graphics graphics = pbGame.CreateGraphics();
                graphics.FillEllipse(new SolidBrush(Color.DarkSalmon), food);
            }

        }

        //gets random food cooridinates 
        private void Food()
        {
            Random rnd = new Random();
            foodX = rnd.Next(10, 490);
            foodY = rnd.Next(10, 490);
            food.X = foodX;
            food.Y = foodY;
            CheckFood();
        }

        //checks if food intercepts with snake already on the game board + rocks
        private void CheckFood()
        {
            //checks snake 
            for (int i = 1; i < snakeLength; i++)
            {
                if (collision(foodX, foodY, 5, recArray[i].X, recArray[i].Y, 10) == true)
                {
                    Food();
                    
                }

            }

            //checks rocks
            for (int i = 1; i <= rocksToDraw; i++)
            {
                if (collision(foodX, foodY, 5, rockArray[i].X, rockArray[i].Y, 15) == true)
                {
                    Food();
         
                }

            }
            

            

        }

        
        
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //formats when rocks show and change pos and how many rocks appear per difficulty
        private void FormatRock()
        {
            if (timerTick % 500 == 0)
            {
                if (rocksToDraw == 0)
                {
                    rocksToDraw = 1;
                }


                Rock(1);

            }

            if (timerTick % 800 == 0 && difficulty != 1)
            {
                if (rocksToDraw == 1)
                {
                    rocksToDraw = 2;
                }
                Rock(2);

            }

            if (timerTick % 1100 == 0 && difficulty == 3)
            {
                if (rocksToDraw == 2)
                {
                    rocksToDraw = 3;
                }
                Rock(3);

            }

            //draws rocks
            for (int i = 1; i <= rocksToDraw; i++)
            {
                System.Drawing.Graphics graphics = pbGame.CreateGraphics();
                graphics.FillEllipse(new SolidBrush(Color.IndianRed), rockArray[i]);

            }

        }

        //gets points for rock
        private void Rock(int rock)
        {
            //sets new x y points for rock
            Random rnd = new Random();
            rockArray[rock] = new Rectangle(50, 50, 50, 50);
            rockArray[rock].X = rnd.Next(50, 450);
            rockArray[rock].Y = rnd.Next(50, 450);
            CheckRock(rock);
        }

        //makes sure when rock is placed it is not hitting anything
        private void CheckRock(int rock)
        {
            //checks for snake collision
            for (int i = 1; i < snakeLength; i++)
            {
                if (collision(rockArray[rock].X, rockArray[rock].Y, 25, recArray[i].X, recArray[i].Y, 10) == true)
                {
                    Rock(rock);

                }

            }

            //checks for foor collision 
            if (collision(foodX, foodY, 5, rockArray[rock].X, rockArray[rock].Y, 25) == true)
            {
                Rock(rock);

            }


        }
     

      
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //ends game
        private void GameOver()
        {
            gameTimer.Enabled = false;          //stops timer
            FormEnd.points = points;            //passes points to end form
            //opens form end
            this.Hide();
            var frmEnd = new FormEnd();
            frmEnd.Closed += (s, args) => this.Close();
            frmEnd.Show();

        }

       
        //returns true if collide, false if don't
        bool collision(int x1, int y1, int radius1, int x2, int y2, int radius2)
        {
            //compares the distance apart to the combined radiuses
            int distanceX = x2 - x1;
            int distanceY = y2 - y1;
            int radii = radius1 + radius2;
            if ((distanceX * distanceX) + (distanceY * distanceY) < radii * radii)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

       
        //key movement
        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            //determines which way the snake moves
            if (e.KeyCode == Keys.Down)
            {   
                incret = +5;
                xy = 2;
            }
            if (e.KeyCode == Keys.Up)
            {
                incret = -5;
                xy = 2;
            }
            if (e.KeyCode == Keys.Left)
            {
                incret = -5;
                xy = 1;
            }
            if (e.KeyCode == Keys.Right)
            {
                incret = +5;
                xy = 1;
            }

        }


    }
}

