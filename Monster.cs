using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monster_Rush_CPT
{
    class Monster
    {
        //making a class so that we can add different enemies with different attributes (location, movement freq, character type)
        //almost like a datatype, it just makes it easier to deal with multiple enemies
        //variables
        int RandomMove;
        int enemyposx, enemyposy;
        float enemymovementtimer = 0.0f;
        float enemymovementfreq;
        char enemychar = 'x';
        bool enemyrnd;

        //constructor, declaring what the class is producing. the properties of the class
        public Monster(int enemypositionx, int enemypositiony, float enemymovementfrequency, char enemycharacter, bool enemyrandom = false)
        {
            enemyposx = enemypositionx; //positioning
            enemyposy = enemypositiony;
            enemymovementfreq = enemymovementfrequency; //freq of moves
            enemychar = enemycharacter; //character on screen
            enemyrnd = enemyrandom;
        }

        void Clearmonster() //gets rid of enemy at current position on screen
        {
            Console.SetCursorPosition(enemyposx + 1, enemyposy);
            Console.Write("\b ");
        }

        public void monstermovementupdate(int playerposx, int playerposy) //monster movement system
        {
            if (enemymovementtimer >= enemymovementfreq) //if the timer is equal to or greather than the freq we set it to. it will activiate.
            {
                Clearmonster(); //gets rid of enemy at current position
                if (enemyrnd)
                {
                    enemyrandommovement();  //only calls randommovement if enemy is declared to move randomly
                }
                else
                {
                    if (enemyposx > playerposx && enemyposx != 1) //movement system to make monster move towards player
                    {
                        enemyposx--;
                    }
                    else if (enemyposx < playerposx)
                    {
                        enemyposx++;
                    }

                    if (enemyposy > playerposy && enemyposy != 1)
                    {
                        enemyposy--;
                    }
                    else if (enemyposy < playerposy)
                    {
                        enemyposy++;
                    }
                }

                monsterupdatevis(); //updates visibility of monsters onscreen
                enemymovementtimer = 0.0f; //resets timer
            }
            enemymovementtimer += Program.delta_time; //connects delta timer from the og program to this class so that timer can work properly
        }

        public void monsterupdatevis() //updates visibility of monsters onscreen
        {
            if (enemymovementfreq <= 0.25f)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
            }
            else if (enemymovementfreq <= 0.5f)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            Console.SetCursorPosition(enemyposx, enemyposy); //gets location it should be at
            Console.Write(enemychar); //writes enemy at that location
        }

        public bool playerenemycollision(int playerposx, int playerposy) //void means it doesnt return, bool means it returns a bool datatype
        {
            if (playerposx == enemyposx && playerposy == enemyposy) //tests if player is touching enemy
            {
                return true;
            }
            return false;
        }

        public void enemyrandommovement() //for random enemy movement
        {
            Random Random = new Random();
            RandomMove = Random.Next(1, 5); //this was put after every key that was pressed by user

            if (RandomMove == 1 && enemyposx != 1)
            {
                enemyposx--;
            }
            else if (RandomMove == 2 && enemyposx != Program.width)
            {
                enemyposx++;
            }
            if (RandomMove == 3 && enemyposy != 1)
            {
                enemyposy--;
            }
            else if (RandomMove == 4 && enemyposy != Program.height)
            {
                enemyposy++;
            }

        }
    }
}