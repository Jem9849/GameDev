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
    }
   
}