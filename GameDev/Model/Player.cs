using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameDev.View;

namespace GameDev.Model
{
    public class Player
    {
        // Initialize the player
        public void Initialize(Animation animation, Vector2 position)
        {
            playerAnimation = animation;

            // Set the starting position of the player around the middle of the screen and to the back
            Position = position;

            // Set the player to be active
            Active = true;

            // Set the player health
            Health = 100;
        }

        // Update the player animation
        public void Update(GameTime gameTime)
        {
            playerAnimation.Position = Position;
            playerAnimation.Update(gameTime);
        }

        // Draw the player
        public void Draw(SpriteBatch spriteBatch)
        {
            playerAnimation.Draw(spriteBatch);
        }

        // Animation for Player
        private Animation playerAnimation;
        public Animation PlayerAnimation
        {
            get { return playerAnimation; }
            set { playerAnimation = value; }
        }

        // Relative position of player to upper left. Also structs can't be used as a property.
        public Vector2 Position;

        // Player state
        private bool active;
        public bool Active
        {
            get { return active; }
            set { active = value; }
        }

        // Hit points of player
        private int health;
        public int Health
        {
            get { return health; }
            set { health = value; }
        }

        // Get the width of the player ship
        public int Width
        {
            get { return playerAnimation.FrameWidth; }
        }

        // Get the height of the player ship
        public int Height
        {
            get { return playerAnimation.FrameHeight; }
        }
    }

}