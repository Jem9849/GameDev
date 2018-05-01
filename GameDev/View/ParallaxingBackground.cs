// ParallaxingBackground.cs
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameDev.View
{
    public class ParallaxingBackground
    {
        // The image representing the parallaxing background
        private Texture2D texture;

        // An array of positions of the parallaxing background
        private Vector2[] positions;

        // The speed which the background is moving
        private int speed;

        public void Initialize(ContentManager content, String texturePath, int screenWidth, int speed)
		{
			// Load background texture
			texture = content.Load<Texture2D>(texturePath);

			// Speed set
			this.speed = speed;

			// Screen tile needed by dividing screen with texture width.
			// Add 1 to it so no gap
			positions = new Vector2[screenWidth / texture.Width + 1];

			// Inital positions
			for (int i = 0; i < positions.Length; i++)
			{
				// Tiles side by side
				positions[i] = new Vector2(i * texture.Width, 0);
			}
        }

        public void Update()
        {
			// Update positions
			for (int i = 0; i < positions.Length; i++)
			{
				// Update position of screen by adding speed
				positions[i].X += speed;

				// If speed move background left
				if (speed <= 0)
				{
					// Check and replace texture if out of screen
					if (positions[i].X <= -texture.Width)
					{
						positions[i].X = texture.Width * (positions.Length - 1);
					}
				}

				// If speed move background right
				else
				{
					// Check and replace texture if out of screen
					if (positions[i].X >= texture.Width * (positions.Length - 1))
					{
						positions[i].X = -texture.Width;
					}
				}
			}
        }


		public void Draw(SpriteBatch spriteBatch)
		{
			for (int i = 0; i < positions.Length; i++)
			{
				spriteBatch.Draw(texture, positions[i], Color.White);
			}
		}
    }
}