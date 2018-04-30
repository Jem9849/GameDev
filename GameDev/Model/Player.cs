using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameDev.Model
{
    public class Player
    {
        public void Initialize()
        {

        }

        public void Update()
        {

        }

        public void Draw()
        {

        }

        // Animation for Player
        private Texture2D playerTexture;
        public Texture2D PlayerTexture
        {
            get { return playerTexture; }
            set { playerTexture = value; }
        }
    }
   
}