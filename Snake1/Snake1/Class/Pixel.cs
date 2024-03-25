using System;

namespace Snake1.Class
{
    /// <summary>
    /// Represents a single pixel in the Snake game.
    /// </summary>
    class Pixel
    {
        /// <summary>
        /// Gets or sets the X position of the pixel.
        /// </summary>
        public int XPos { get; set; }

        /// <summary>
        /// Gets or sets the Y position of the pixel.
        /// </summary>
        public int YPos { get; set; }

        /// <summary>
        /// Gets or sets the color of the pixel.
        /// </summary>
        public ConsoleColor Color { get; set; }
    }
}
