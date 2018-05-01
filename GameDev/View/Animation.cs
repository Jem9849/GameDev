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
        private Rectangle distinationRect = new Rectangle();

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

        public void Initialize()
        {

        }



        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
