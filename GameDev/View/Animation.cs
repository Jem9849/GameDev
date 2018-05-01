// Animation.cs
//Using declarations
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameDev.View
{
    public class Animation
    {
        // Image representing collection images
        private Texture2D spriteStrip;

        // Scale used to display sprite strip
        private float scale;

        // Time since updated frame
        private int elapsedTime;

        // Time we display a frame
        private int frameTime;

        // Number of frames in animation
        private int frameCount;

        // Index of current frame
        private int currentFrame;

        // Color of frame
        private Color color;

        // Area of image strip
        private Rectangle sourceRect = new Rectangle();

        // Area of place to display
        private Rectangle destinationRect = new Rectangle();

        // Frame width
        private int frameWidth;
        public int FrameWidth
        {
            get { return frameWidth; }
            set { frameWidth = value; }
        }

        // Frame height
        private int frameHeight;
        public int FrameHeight
        {
            get { return frameHeight; }
            set { frameHeight = value; }
        }

        // Animation state
        private bool active;
        public bool Active
        {
            get { return active; }
            set { active = value; }
        }

        // Animation play or deactivate after run?
        private bool looping;
        public bool Looping
        {
            get { return looping; }
            set { looping = value; }
        }

        // Frame width?
        public Vector2 Position;

        // Use this for initialization
        public void Initialize(Texture2D texture, Vector2 position, int frameWidth, int frameHeight, int frameCount, int frametime, Color color, float scale, bool looping)
        {
            // Keep a local copy of the values passed in
            this.color = color;
            this.FrameWidth = frameWidth;
            this.FrameHeight = frameHeight;
            this.frameCount = frameCount;
            this.frameTime = frametime;
            this.scale = scale;

            Looping = looping;
            Position = position;
            spriteStrip = texture;

            // Set the time to zero
            elapsedTime = 0;
            currentFrame = 0;

            // Set the Animation to active by default
            Active = true;
        }

        // Draw the Animation Strip
        public void Draw(SpriteBatch spriteBatch)
        {
            // Only draw the animation when we are active
            if (Active)
            {
                spriteBatch.Draw(spriteStrip, destinationRect, sourceRect, color);
            }
        }

        // Update is called once per frame
        public void Update(GameTime gameTime)
        {
            // Do not update the game if we are not active
            if (Active == false)
            {
                return;
            }
            // Update the elapsed time
            elapsedTime += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

            // If the elapsed time is larger than the frame time
            // we need to switch frames
            if (elapsedTime > frameTime)
            {
                // Move to the next frame
                currentFrame++;

                // If the currentFrame is equal to frameCount reset currentFrame to zero
                if (currentFrame == frameCount)
                {
                    currentFrame = 0;
                    // If we are not looping deactivate the animation
                    if (Looping == false)
                    {
                        Active = false;
                    }
                }

                // Reset the elapsed time to zero
                elapsedTime = 0;
            }

            // Grab the correct frame in the image strip by multiplying the currentFrame index by the frame width
            sourceRect = new Rectangle(currentFrame * FrameWidth, 0, FrameWidth, FrameHeight);

            // Grab the correct frame in the image strip by multiplying the currentFrame index by the frame width
            destinationRect = new Rectangle((int)Position.X - (int)(FrameWidth * scale) / 2, (int)Position.Y - (int)(FrameHeight * scale) / 2, (int)(FrameWidth * scale), (int)(FrameHeight * scale));
        }

    }
}
