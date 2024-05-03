using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Timers;

namespace Monster_Rush_CPT
{
    class Program
    {
        //names: Adrian Kmiec, Matthew Ferreira
        //Purpose: to get a good mark for the cpt
        //Date: June 2, 2020

        //variables
        static int playerx = 10, playery = 10; //player positon x and y
        public static int width = 78, height = 24; //grid size
        static char character = 'O'; //chars
        static char coin = '¢';
        static int coinX = 15, coinY = 20;
        static int coincount = 0;
        static string Name;
        static string enemychars = "#@8LXB";
        static float enemyfreqmax = 2.0f;
        static float enemyfreqmin = 0.2f;
        static string input;
        static bool game = true;


        //arrays
        static List<Monster> enemies = new List<Monster>(); //makes an array/list from the class Monster and makes it a list. this is now list of monsters 

        //for system timer/delta time
        static long last_time = System.Environment.TickCount; // gets run time at this point
        public static float delta_time;
        private static int second; //counter to be displayed

        static bool gameactive = false;
        static ConsoleKeyInfo KeyInfo;

        //-------------------main code for game----------------------------------------------------------------
        static void Main(string[] args)
        {
            enemies.Clear(); //emptys list
            enemies = new List<Monster>(); //for safety. replaces list to empty list.

            Console.CursorVisible = false; //makes cursor white line invisible
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\t\t░██╗░░░░░░░██╗███████╗██╗░░░░░░█████╗░░█████╗░███╗░░░███╗███████╗ ████████╗░█████╗░");
            Console.WriteLine("\t\t░██║░░██╗░░██║██╔════╝██║░░░░░██╔══██╗██╔══██╗████╗░████║██╔════╝ ╚══██╔══╝██╔══██╗");
            Console.WriteLine("\t\t░╚██╗████╗██╔╝█████╗░░██║░░░░░██║░░╚═╝██║░░██║██╔████╔██║█████╗░░ ░░░██║░░░██║░░██║");
            Console.WriteLine("\t\t░░████╔═████║░██╔══╝░░██║░░░░░██║░░██╗██║░░██║██║╚██╔╝██║██╔══╝░░ ░░░██║░░░██║░░██║");
            Console.WriteLine("\t\t░░╚██╔╝░╚██╔╝░███████╗███████╗╚█████╔╝╚█████╔╝██║░╚═╝░██║███████╗ ░░░██║░░░╚█████╔╝");
            Console.WriteLine("\t\t░░░╚═╝░░░╚═╝░░╚══════╝╚══════╝░╚════╝░░╚════╝░╚═╝░░░░░╚═╝╚══════╝ ░░░╚═╝░░░░╚════╝░");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("");
            Console.WriteLine("       \t███╗░░░███╗░█████╗░███╗░░██╗░██████╗████████╗███████╗██████╗░  ██████╗░██╗░░░██╗░██████╗██╗░░██╗");
            Console.WriteLine("       \t████╗░████║██╔══██╗████╗░██║██╔════╝╚══██╔══╝██╔════╝██╔══██╗  ██╔══██╗██║░░░██║██╔════╝██║░░██║");
            Console.WriteLine("       \t██╔████╔██║██║░░██║██╔██╗██║╚█████╗░░░░██║░░░█████╗░░██████╔╝  ██████╔╝██║░░░██║╚█████╗░███████║");
            Console.WriteLine("       \t██║╚██╔╝██║██║░░██║██║╚████║░╚═══██╗░░░██║░░░██╔══╝░░██╔══██╗  ██╔══██╗██║░░░██║░╚═══██╗██╔══██║");
            Console.WriteLine("       \t██║░╚═╝░██║╚█████╔╝██║░╚███║██████╔╝░░░██║░░░███████╗██║░░██║  ██║░░██║╚██████╔╝██████╔╝██║░░██║");
            Console.WriteLine("       \t╚═╝░░░░░╚═╝░╚════╝░╚═╝░░╚══╝╚═════╝░░░░╚═╝░░░╚══════╝╚═╝░░╚═╝  ╚═╝░░╚═╝░╚═════╝░╚═════╝░╚═╝░░╚═╝");




            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("\t\t\t\t\t\tInfo:");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\t\t\t\t\t Red enemies are slow");
            Console.WriteLine("\t\t\t\t        Green enemies are fast");
            Console.WriteLine("\t\t\t           Magenta enemies are really fast");
            Console.WriteLine("\t\t        Characters move randomly, letters and numbers move towards you");

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("\t\t\t\t\t\tControls: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\t\t\tPress the arrow keys to move. Up for up, Right for right, etc.");
            Console.WriteLine("\t\t\t\t       Enter Your Name To begin.");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(50, 27);
            Name = Console.ReadLine();
            Console.Beep();
            Console.ForegroundColor = ConsoleColor.White;
            gamestart();


        }

        public static void spawn() //spawns everything in
        {
            //for reset
            Console.Clear(); //clears console
            enemies.Clear(); //emptys list
            enemies = new List<Monster>(); //for safety. replaces list to empty list.
            playerx = 10; //sets variables back to default value
            playery = 10;
            second = 0;
            coincount = 0;

            //spawns border
            for (int i = 0; i < width + 2; i++)
            {
                Console.Write("-");
            }
            for (int i = 0; i < height; i++)
            {
                Console.Write("\n-");
                for (int u = 0; u < 78; u++)
                {
                    Console.Write(" ");
                }
                Console.Write("-");
            }
            Console.Write("\n");
            for (int i = 0; i < width + 2; i++)
            {
                Console.Write("-");
            }

            //spawns character
            Console.SetCursorPosition(playerx, playery);
            Console.Write(character);

            //spawns enemy(s) , true for random movement, nothing for follow player movement
            newenemy('@', 1.0f, true); //char type and movement freq.
            newenemy('X', 0.5f);

            //main code for game
            gameupdate();

        }

        public static void gameupdate() //main code for game to constantly run
        {
            Console.CursorVisible = false; //makes cursor white line invisible
            bool activegame = true; //makes activegame true so that game movement can start
            gameactive = false;
            while (activegame)
            {
                Console.CursorVisible = false; //makes cursor white line invisible
                long current_tick = System.Environment.TickCount; //gets run time at this point
                delta_time = (current_tick - last_time) / 1000.0f;
                //gets delta time and converts it to seconds (from miliseconds)

                if (Console.KeyAvailable) //only enters movement loop if a key is pressed, allowing everything else to work even if no input is made
                {
                    gameactive = true; //makes gameactive to true so that code can continue
                    KeyInfo = Console.ReadKey(true);
                    pmovement();
                }
                if (!gameactive) //same as saying gameactive != true(only works if bool). makes it so that nothing can move until the player moves atleast once
                {
                    continue; //skips rest of code following it and starts next iteration of loop
                }
                //means that if you dont move, it will skip rest of while loop and keep on repeating this until you move, thus timer only activates when player moves

                infoboard(); //spawns info at bottom

                updatetimer(); //updates timer at bottom

                enemyupdateall(); //updates all enemy currently on board (location wise)

                CoinSpawn();

                if (enemyplayercollideall()) //tests for enemy player collision
                {
                    activegame = false; //if player collides into enemy, game stops
                }

                last_time = current_tick; //current tick is beginning of the cycle, last time is when code first started. being updated every loop.
                //keep this last please
            }

        }

        public static void pmovement() //movement input system for player
        {
            Clearplayer(); //deletes where player is currently at
            switch (KeyInfo.Key)
            {
                case ConsoleKey.RightArrow:
                    if (playerx != width) //border
                    {
                        playerx++;
                    }
                    else { }
                    break;

                case ConsoleKey.LeftArrow:
                    if (playerx != 1) //border
                    {
                        playerx--;
                    }
                    else { }
                    break;

                case ConsoleKey.UpArrow:
                    if (playery != 1) //border
                    {
                        playery--;
                    }
                    else { }
                    break;

                case ConsoleKey.DownArrow:
                    if (playery != height) //border
                    {
                        playery++;
                    }
                    else { }
                    break;

                case ConsoleKey.Escape:
                    pause();
                    break;


            }

            Console.SetCursorPosition(playerx, playery); //sets new position for player
            Console.ForegroundColor = ConsoleColor.Cyan; //sets colour for player
            Console.Write(character); //writes out character at new position in colour
        }

        public static void Clearplayer() //method to delete player at current location
        {
            Console.SetCursorPosition(playerx + 1, playery);
            Console.Write("\b "); //backspace
        }

        public static void infoboard() //info at bottom of game grid
        {
            int scorefinal = second;
            Console.SetCursorPosition(1, 27); //position where info is
            Console.ForegroundColor = ConsoleColor.White; //colour
            Console.Write("Seconds: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(10, 27);
            Console.Write(scorefinal);
            Console.SetCursorPosition(36, 27);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Coins:");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(43, 27);
            Console.Write(coincount);
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(62, 27);
            Console.WriteLine("Player:");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(70, 27);
            Console.WriteLine(Name);

            Console.SetCursorPosition(0, 27); //to update
            Console.Write("\b ");
        }

        static float timer = 0.0f; //sets timer at 0
        public static void updatetimer() //method to update infoboard(specifically timer)
        {
            if (timer >= 1.0f) //if time is at 1 second
            {
                // Add 1 to second
                second++;
                // Reset timer
                timer = 0.0f;
            }
            timer += delta_time; //updates timer by the delta time //ask for more clarification
        }

        public static void enemyupdateall() //updates enemy locations
        {
            for (int i = 0; i < enemies.Count; i++) //runs for amount of enemies on screen
            {
                enemies[i].monstermovementupdate(playerx, playery); //for each enemy, go to method monstermovementupdate with the current player location
            }
        }

        public static bool enemyplayercollideall() //goes through all enemies on board, if player touches enemy, will return a true value. false if no collision is happening
        {
            for (int i = 0; i < enemies.Count; i++) //runs for amount of enemies on board
            {
                if (enemies[i].playerenemycollision(playerx, playery)) //if enemy is touching player
                {
                    Console.Beep(500, 200);
                    return true;
                }

            }
            return false;
        }

        public static void newenemy(char enemycharacter, float enemymovementfrequency, bool enemyrnd = false) //enemy spawning system
        {
            bool enemyvalid = true;
            int enemyx = 0, enemyy = 0;
            do
            { //math on random makes it so that enemy spawning is truly random and not on top of the coin.
                Random rnd = new Random(System.Environment.TickCount + enemies.Count); //randomly spawns enemy at a different random every single time because tick count is constantly changing
                enemyx = rnd.Next(2, width);
                enemyy = rnd.Next(2, height); //sets new enemy location (x and y)

                enemyvalid = true;

                if (distancecalculator(playerx, playery, enemyx, enemyy) >= 5)
                {
                    enemyvalid = false;
                }

                if ((playerx == enemyx && playery == enemyy) || (enemyx == coinX && enemyy == coinY))
                {
                    enemyvalid = false;
                }

            } while (enemyvalid);

            Monster newmonster = new Monster(enemyx, enemyy, enemymovementfrequency, enemycharacter, enemyrnd); //makes a new enemy with the enemyx and enemyy calculated. Can set movement freq. and character in spawn method
            enemies.Add(newmonster); //adds enemy to the enemies list

            newmonster.monsterupdatevis(); //makes it so that it appears right away
        }

        static void clearcoin() //clears coin
        {
            Console.SetCursorPosition(coinX + 1, coinY);
            Console.ForegroundColor = ConsoleColor.Cyan; //sets colour for player
            Console.Write("\bO "); //backspace
        }

        static void CoinSpawn()
        {

            Console.SetCursorPosition(coinX, coinY);
            Console.Write(coin);
            if (coinX == playerx && coinY == playery)
            {
                clearcoin();
                Console.Beep(1000, 200);
                coincount++;
                Random random = new Random();
                coinX = random.Next(1, width);
                coinY = random.Next(1, height);
                newrandomenemy();
            }
        }

        static void gamestart() //determines if game should continue or not
        {

            do
            {
                spawn();

                //only fires once player loses (once activegame = false)
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine("..NNNO! ...                  ......    MNO! ...");
                Console.WriteLine("...NNMNO!! .........................MNNOO! ...");
                Console.WriteLine("....MMMMNO! ......................MNNOO!! .");
                Console.WriteLine("..... MNOONNOO! MMMMMMMMMMMMPP  MNNO!!!! .");
                Console.WriteLine("... !O! NNO! MMMMMMMMMMMMMPPPOOOII! NO! ....");
                Console.WriteLine("...... ! MMMMMMMMMMMMMPPPPOOOOIII!I! ...");
                Console.WriteLine("........ MMMMMMMMMMMMPPPPPOOOOOOIII! ...");
                Console.WriteLine("........NMMMMMOOOOOOPPPPPPPPOOO.MII!! ... ");
                Console.WriteLine("....... MMMMM.     OPPMMP      .MII!! ....  ██╗░░░██╗░█████╗░██╗░░░██╗  ██████╗░██╗███████╗██████╗░");
                Console.WriteLine("........NMMMM:     .,OPMP,     ::I!!! ...   ╚██╗░██╔╝██╔══██╗██║░░░██║  ██╔══██╗██║██╔════╝██╔══██╗");
                Console.WriteLine("........ NNNM:    ,OOPM!P,...::!!II! ....   ░╚████╔╝░██║░░██║██║░░░██║  ██║░░██║██║█████╗░░██║░░██║");
                Console.WriteLine("........ MMNNNNNOOOOPMO!!IIPPOP!O!! .....   ░░╚██╔╝░░██║░░██║██║░░░██║  ██║░░██║██║██╔══╝░░██║░░██║");
                Console.WriteLine("........ MMMMMNNNNOO:!!:!!IPPPPOOO! ....    ░░░██║░░░╚█████╔╝╚██████╔╝  ██████╔╝██║███████╗██████╔╝");
                Console.WriteLine("..........MMMMMNNOOMMNNIIIPPPOOO!! .....    ░░░╚═╝░░░░╚════╝░░╚═════╝░  ╚═════╝░╚═╝╚══════╝╚═════╝░");
                Console.WriteLine("........... MMMMONNMMNNNIIIOOO0!.OOO....    ░░░   ░░░░      ░░       ░         ░                  ░");
                Console.WriteLine(".......... MN MOMMMNNNNNNNNNMOOO OOOOO.....");
                Console.WriteLine("......... MNO! Iiiiiiiiiiiiiiii .OOOOOO......");
                Console.WriteLine("..... NNN.MNO! .O!!!!!!!!!!!!O ... OONONOO! ..");
                Console.WriteLine(".. MNNNNNO! ....oOOOOOOOOOOOO ....  MMNNON!...");
                Console.WriteLine("... MNNNNO! .... PPPPPPPPPP ......  MNON!....");
                Console.WriteLine("..... OO! .................................");
                Console.WriteLine("................................");
                Console.WriteLine("");
                Console.WriteLine("Game Over " + Name);
                Console.WriteLine("Score: " + coincount + " coins");
                Console.WriteLine("Time: " + second + " seconds");
                Console.WriteLine("Type 'yes' to play again.....");
                Console.ForegroundColor = ConsoleColor.White;
                input = Console.ReadLine(); //add int parse for this
                if (input == "yes" || input == "Yes" || input == "YES" ||input == "y" || input == "Y")
                {
                    game = true;

                }
                else
                {
                    game = false;
                }

            } while (game);

            System.Environment.Exit(0); //closes program

        }

        static void newrandomenemy() //spawns everytime coin is picked up
        {
            Random rnd = new Random(System.Environment.TickCount); //randomizer that randomizes all characteristics of the monster
            char enemycoinchar = enemychars[rnd.Next(0, enemychars.Length - 1)]; //enemy character 
            float enemycoinfreq = (enemyfreqmax - enemyfreqmin) * (float)rnd.NextDouble() + enemyfreqmin; //2 - 0.2, 1.8 x 0-1 + 0.2, makes it so that min is always 0.2 and max is alwauy 2

            bool enemycoinmovement;
            if (enemycoinchar == '#' || enemycoinchar == '@')
            {
                enemycoinmovement = true;
            }
            else
            {
                enemycoinmovement = false;
            }


            newenemy(enemycoinchar, enemycoinfreq, enemycoinmovement);

            //enmeies with speed above 1 - slow, blue
            //enemies with # = movement stupid
        }

        static int distancecalculator(int x, int y, int xtwo, int ytwo) //calculates distance between 2 points
        {
            return (int)(Math.Pow(x - xtwo, 2) + Math.Pow(y - ytwo, 2)); //(int) converts all to int. return means returns the value.
        }

        static void pause()
        {
            Console.SetCursorPosition(82, 10);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Controls: ");
            Console.SetCursorPosition(82, 11);
            Console.Write("Press the arrow keys to move");
            Console.SetCursorPosition(82, 12);
            Console.Write("Continue? (type 'yes' or 'no')");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(82, 15);
            input = Console.ReadLine(); //add int parse for this
            input = input.ToLower();

            if (input == "yes" || input == "y")
            {
                game = true;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(82, 10);
                Console.Write("Press the arrow keys to move))))))))))");
                Console.SetCursorPosition(82, 11);
                Console.Write("Press the arrow keys to move))))))))))");
                Console.SetCursorPosition(82, 12);
                Console.Write("Press the arrow keys to move))))))))))");
                Console.SetCursorPosition(82, 13);
                Console.Write("Press the arrow keys to move))))))))))");
                Console.SetCursorPosition(82, 14);
                Console.Write("Press the arrow keys to move))))))))))");
                Console.SetCursorPosition(82, 15);
                Console.Write("Press the arrow keys to move))))))))))");

            }

            else if (input == "no" || input == "n")
            {
                System.Environment.Exit(0);
            }

            else
            {
                pause();
                game = false;
            }


        }

    }

}