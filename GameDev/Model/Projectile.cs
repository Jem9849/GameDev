﻿// Projectile.cs
//Using declarations
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameDev.Model
{
	public class Projectile
	{
		public Projectile()
		{
			
		}

		// Image representing the Projectile
		private Texture2D texture;
		public Texture2D Texture
		{
			get { return texture; }
			set { texture = value; }
		}
		// Position of the Projectile relative to the upper left side of the screen
		public Vector2 Position;

		// State of the Projectile
		private bool active;
		public bool Active
		{
			get { return active; }
			set { active = value; }
		}

		// The amount of damage the projectile can inflict to an enemy
		public int damage;
		public int Damage
		{
			get { return damage; }
			set { damage = value; }
		}

		// Represents the viewable boundary of the game
		private Viewport viewport;


		// Get the width of the projectile ship
		public int Width
		{
			get { return Texture.Width; }
		}

		// Get the height of the projectile ship
		public int Height
		{
			get { return Texture.Height; }
		}

		// Determines how fast the projectile moves
		private float projectileMoveSpeed;


		public void Initialize()
		{
			
		}
		public void Update()
		{
			
		}
		public void Draw()
		{
			
		}
	}
}
