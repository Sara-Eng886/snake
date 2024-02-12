using System;
using System.Collections.Generic;

namespace Snake
{
    class Program
    {
        static int screenwidth;  // Variable to store the width of the game screen
        static int screenheight; // Variable to store the height of the game screen

        static void DrawBorders()
        {
            // Draw the top border
            for (int i = 0; i < screenwidth; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write("■");
            }
            // Draw the bottom border
            for (int i = 0; i < screenwidth; i++)
            {
                Console.SetCursorPosition(i, screenheight - 1);
                Console.Write("■");
            }
            // Draw the left border
            for (int i = 0; i < screenheight; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("■");
            }
            // Draw the right border
            for (int i = 0; i < screenheight; i++)
            {
                Console.SetCursorPosition(screenwidth - 1, i);
                Console.Write("■");
            }
        }

        static void InitializeGame()
        {
            // Initialize the game by setting the screen width and height
            screenwidth = Console.WindowWidth;
            screenheight = Console.WindowHeight;
        }

        static void Main(string[] args)
        {
            // Initialize the game
            InitializeGame();

            Random randomnummer = new Random();
            int score = 5;
            int gameover = 0;
            pixel hoofd = new pixel();
            hoofd.xpos = screenwidth / 2;
            hoofd.ypos = screenheight / 2;
            hoofd.schermkleur = ConsoleColor.Red;
            string movement = "RIGHT";
            List<int> xposlijf = new List<int>();
            List<int> yposlijf = new List<int>();
            int berryx = randomnummer.Next(0, screenwidth);
            int berryy = randomnummer.Next(0, screenheight);
            DateTime tijd = DateTime.Now;
            DateTime tijd2 = DateTime.Now;
            string buttonpressed = "no";
            int highScore = 0;

            const int TimeDelay = 500;

            while (true)
            {
                // Clear the console and draw the game borders
                Console.Clear();
                InitializeGame();
                DrawBorders();

                // Check for collisions with borders
                if (hoofd.xpos == screenwidth - 1 || hoofd.xpos == 0 || hoofd.ypos == screenheight - 1 || hoofd.ypos == 0)
                {
                    gameover = 1;
                }

                Console.ForegroundColor = ConsoleColor.Green;

                // Check if the snake eats a berry and update the score
                if (berryx == hoofd.xpos && berryy == hoofd.ypos)
                {
                    score++;
                    berryx = randomnummer.Next(1, screenwidth - 2);
                    berryy = randomnummer.Next(1, screenheight - 2);
                }

                // Draw the snake body and check for collisions with itself
                for (int i = 0; i < xposlijf.Count; i++)
                {
                    Console.SetCursorPosition(xposlijf[i], yposlijf[i]);
                    Console.Write("■");
                    if (xposlijf[i] == hoofd.xpos && yposlijf[i] == hoofd.ypos)
                    {
                        gameover = 1;
                    }
                }

                // Exit the game loop if gameover is true
                if (gameover == 1)
                {
                    break;
                }

                // Draw the snake head and the berry
                Console.SetCursorPosition(hoofd.xpos, hoofd.ypos);
                Console.ForegroundColor = hoofd.schermkleur;
                Console.Write("■");
                Console.SetCursorPosition(berryx, berryy);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("■");

                // Move the snake based on user input
                tijd = DateTime.Now;
                buttonpressed = "no";
                while (true)
                {
                    tijd2 = DateTime.Now;
                    if (tijd2.Subtract(tijd).TotalMilliseconds > TimeDelay) { break; }
                    if (Console.KeyAvailable)
                    {
                        ConsoleKeyInfo toets = Console.ReadKey(true);
                        if (toets.Key.Equals(ConsoleKey.P))
                        {
                            Console.WriteLine("Game paused. Press any key to resume.");
                            Console.ReadKey(true);
                        }
                        if (toets.Key.Equals(ConsoleKey.UpArrow) && movement != "DOWN" && buttonpressed == "no")
                        {
                            movement = "UP";
                            buttonpressed = "yes";
                        }
                        if (toets.Key.Equals(ConsoleKey.DownArrow) && movement != "UP" && buttonpressed == "no")
                        {
                            movement = "DOWN";
                            buttonpressed = "yes";
                        }
                        if (toets.Key.Equals(ConsoleKey.LeftArrow) && movement != "RIGHT" && buttonpressed == "no")
                        {
                            movement = "LEFT";
                            buttonpressed = "yes";
                        }
                        if (toets.Key.Equals(ConsoleKey.RightArrow) && movement != "LEFT" && buttonpressed == "no")
                        {
                            movement = "RIGHT";
                            buttonpressed = "yes";
                        }
                    }
                }

                // Add the current position of the snake head to the body
                xposlijf.Add(hoofd.xpos);
                yposlijf.Add(hoofd.ypos);

                // Move the snake head based on the chosen direction
                switch (movement)
                {
                    case "UP":
                        hoofd.ypos--;
                        break;
                    case "DOWN":
                        hoofd.ypos++;
                        break;
                    case "LEFT":
                        hoofd.xpos--;
                        break;
                    case "RIGHT":
                        hoofd.xpos++;
                        break;
                }

                // Remove the oldest part of the snake body if it exceeds the score
                if (xposlijf.Count > score)
                {
                    xposlijf.RemoveAt(0);
                    yposlijf.RemoveAt(0);
                }
            }

            // Update the high score if the current score is higher
            if (score > highScore)
            {
                highScore = score;
            }

            // Display the game over message with the score and high score
            Console.SetCursorPosition(screenwidth / 5, screenheight / 2);
            Console.WriteLine($"Game over, Score: {score}, High Score: {highScore}");
        }

        class pixel
        {
            public int xpos { get; set; }
            public int ypos { get; set; }
            public ConsoleColor schermkleur { get; set; }
        }
    }
}
