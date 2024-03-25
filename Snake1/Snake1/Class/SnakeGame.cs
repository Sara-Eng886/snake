using System;
using System.Collections.Generic;

namespace Snake1
{
    /// <summary>
    /// Represents the game logic for the Snake game.
    /// </summary>
    class SnakeGame
    {
        private readonly int screenwidth; // Width of the game screen
        private readonly int screenheight; // Height of the game screen
        private readonly Random random = new Random(); // Random number generator

        private List<int> xposlijf = new List<int>(); // X positions of the snake body segments
        private List<int> yposlijf = new List<int>(); // Y positions of the snake body segments

        private int score = 0; // Current score
        private int gameover; // Flag indicating game over
        private int berryx; // X position of the berry
        private int berryy; // Y position of the berry
        private int highScore; // Highest score achieved

        private string movement = "RIGHT"; // Direction of snake movement

        /// <summary>
        /// Initializes a new instance of the SnakeGame class with the specified screen width and height.
        /// </summary>
        /// <param name="width">Width of the game screen.</param>
        /// <param name="height">Height of the game screen.</param>
        public SnakeGame(int width, int height)
        {
            screenwidth = width;
            screenheight = height;
        }

        /// <summary>
        /// Starts the game.
        /// </summary>
        public void Play()
        {
            Initialize(); // Initialize the game
            while (gameover == 0) // Continue the game loop until gameover flag is set
            {
                Draw(); // Draw the game elements
                HandleInput(); // Handle user input
                Update(); // Update the game state
                CheckCollisions(); // Check for collisions
                System.Threading.Thread.Sleep(100); // Add a small delay for smoother gameplay
            }
            DisplayGameOver(); // Display game over message when game ends
        }

        /// <summary>
        /// Initializes the game state.
        /// </summary>
        private void Initialize()
        {
            score = 0; // Reset score
            gameover = 0; // Reset game over flag
            berryx = random.Next(1, screenwidth - 2); // Generate random X position for the berry
            berryy = random.Next(1, screenheight - 2); // Generate random Y position for the berry
            xposlijf.Clear(); // Clear the list of snake's X positions
            yposlijf.Clear(); // Clear the list of snake's Y positions
            xposlijf.Add(screenwidth / 2); // Add initial X position for the snake's head
            yposlijf.Add(screenheight / 2); // Add initial Y position for the snake's head
        }

        /// <summary>
        /// Draws the game elements on the console.
        /// </summary>
        private void Draw()
        {
            Console.Clear(); // Clear the console before drawing
            DrawBorders(); // Draw the game borders
            DrawSnake(); // Draw the snake
            DrawBerry(); // Draw the berry
            Console.SetCursorPosition(0, screenheight); // Move cursor to the bottom of the screen
            Console.WriteLine($"Score: {score} | High Score: {highScore}"); // Display score information
        }

        /// <summary>
        /// Draws the game borders on the console.
        /// </summary>
        private void DrawBorders()
        {
            // Draw top and bottom borders
            for (int i = 0; i < screenwidth; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write("■");
                Console.SetCursorPosition(i, screenheight - 1);
                Console.Write("■");
            }
            // Draw left and right borders
            for (int i = 0; i < screenheight; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("■");
                Console.SetCursorPosition(screenwidth - 1, i);
                Console.Write("■");
            }
        }

        /// <summary>
        /// Draws the snake on the console.
        /// </summary>
        private void DrawSnake()
        {
            for (int i = 0; i < xposlijf.Count; i++)
            {
                Console.SetCursorPosition(xposlijf[i], yposlijf[i]);
                Console.Write("■"); // Draw each segment of the snake
            }
        }

        /// <summary>
        /// Draws the berry on the console.
        /// </summary>
        private void DrawBerry()
        {
            Console.SetCursorPosition(berryx, berryy);
            Console.Write("■"); // Draw the berry
        }

        /// <summary>
        /// Handles user input for controlling the snake.
        /// </summary>
        private void HandleInput()
        {
            if (Console.KeyAvailable) // Check if a key is pressed
            {
                ConsoleKeyInfo key = Console.ReadKey(true); // Read the key pressed
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow: // Move up
                        if (movement != "DOWN")
                            movement = "UP";
                        break;
                    case ConsoleKey.DownArrow: // Move down
                        if (movement != "UP")
                            movement = "DOWN";
                        break;
                    case ConsoleKey.LeftArrow: // Move left
                        if (movement != "RIGHT")
                            movement = "LEFT";
                        break;
                    case ConsoleKey.RightArrow: // Move right
                        if (movement != "LEFT")
                            movement = "RIGHT";
                        break;
                    case ConsoleKey.P: // Pause the game
                        Console.WriteLine("Game Paused. Press any key to resume...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        /// <summary>
        /// Updates the game state based on the current movement direction.
        /// </summary>
        private void Update()
        {
            int nextX = xposlijf[0]; // Get the next X position of the snake's head
            int nextY = yposlijf[0]; // Get the next Y position of the snake's head
            switch (movement) // Update the next position based on the current movement direction
            {
                case "UP":
                    nextY--;
                    break;
                case "DOWN":
                    nextY++;
                    break;
                case "LEFT":
                    nextX--;
                    break;
                case "RIGHT":
                    nextX++;
                    break;
            }

            xposlijf.Insert(0, nextX); // Insert the new X position at the beginning of the list
            yposlijf.Insert(0, nextY); // Insert the new Y position at the beginning of the list

            // Check if the snake eats the berry
            if (nextX == berryx && nextY == berryy)
            {
                score++; // Increase the score
                berryx = random.Next(1, screenwidth - 2); // Generate a new X position for the berry
                berryy = random.Next(1, screenheight - 2); // Generate a new Y position for the berry
            }
            else
            {
                xposlijf.RemoveAt(xposlijf.Count - 1); // Remove the last X position from the list
                yposlijf.RemoveAt(yposlijf.Count - 1); // Remove the last Y position from the list
            }
        }

        /// <summary>
        /// Checks for collisions with the game borders and the snake's own body.
        /// </summary>
        private void CheckCollisions()
        {
            // Check for collisions with the game borders
            if (xposlijf[0] == 0 || xposlijf[0] == screenwidth - 1 || yposlijf[0] == 0 || yposlijf[0] == screenheight - 1)
                gameover = 1;

            // Check for collisions with the snake's own body
            for (int i = 1; i < xposlijf.Count; i++)
            {
                if (xposlijf[i] == xposlijf[0] && yposlijf[i] == yposlijf[0])
                {
                    gameover = 1;
                    break;
                }
            }
        }

        /// <summary>
        /// Displays the game over message with the final score and high score.
        /// </summary>
        private void DisplayGameOver()
        {
            Console.SetCursorPosition(screenwidth / 5, screenheight / 2);
            Console.WriteLine($"Game over, Score: {score}, High Score: {highScore}");
        }
    }
}
