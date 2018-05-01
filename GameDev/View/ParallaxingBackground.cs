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

        public void Initialize(ContentManger content, String texturePath, int screenWidth, int speed)
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

        }
        public void Draw()
        {

        }
    }
}