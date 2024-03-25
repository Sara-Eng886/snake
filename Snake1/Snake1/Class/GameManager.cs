using System;

namespace Snake1.Class
{
    /// <summary>
    /// Manages the execution of the Snake game.
    /// </summary>
    class GameManager
    {
        private const int TimeDelay = 500; // Delay between each game loop iteration
        private int screenwidth; // Width of the game screen
        private int screenheight; // Height of the game screen

        /// <summary>
        /// Starts the Snake game.
        /// </summary>
        public void Run()
        {
            InitializeGame(); // Initialize the game
            SnakeGame snakeGame = new SnakeGame(screenwidth, screenheight); // Create a new instance of SnakeGame
            snakeGame.Play(); // Start playing the game
        }

        /// <summary>
        /// Initializes the game by setting the screen width and height.
        /// </summary>
        private void InitializeGame()
        {
            screenwidth = Console.WindowWidth; // Get the width of the console window
            screenheight = Console.WindowHeight; // Get the height of the console window
        }
    }
}
