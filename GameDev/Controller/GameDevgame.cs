using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

// Reference for all Model objects
using GameDev.Model;
using GameDev.View;


namespace GameDev.Controller
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GameDevgame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // Represents the player 
        private Player player;

        // Keyboard states used to determine key presses
        private KeyboardState currentKeyboardState;
        private KeyboardState previousKeyboardState;

        // Gamepad states used to determine button presses
        private GamePadState currentGamePadState;
        private GamePadState previousGamePadState;

        // A movement speed for the player
        private float playerMoveSpeed;

		// Image used to display the static background
		private Texture2D mainBackground;

		// Parallaxing Layers
		private ParallaxingBackground bgLayer1;
		private ParallaxingBackground bgLayer2;

		// Enemies
		private Texture2D enemyTexture;
		private List<Enemy> enemies;

		// The rate at which the enemies appear
		private TimeSpan enemySpawnTime;
		private TimeSpan previousSpawnTime;

		// A random number generator
		private Random random;

		private Texture2D projectileTexture;
		private List<Projectile> projectiles;

		// Rate of fire
		private TimeSpan fireTime;
		private TimeSpan previousFireTime;

        public GameDevgame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            // Initialize the player class
            player = new Player();

            // Set a constant move speed
            playerMoveSpeed = 8.0f;

			bgLayer1 = new ParallaxingBackground();
			bgLayer2 = new ParallaxingBackground();

			// Load the parallaxing background
			bgLayer1.Initialize(Content, "Texture/bgLayer1", GraphicsDevice.Viewport.Width, -1);
			bgLayer2.Initialize(Content, "Texture/bgLayer2", GraphicsDevice.Viewport.Width, -2);

			mainBackground = Content.Load<Texture2D>("Texture/mainbackground");

			// Initialize the enemies list
			enemies = new List<Enemy> ();

			// Set the time keepers to zero
			previousSpawnTime = TimeSpan.Zero;

			// Used to determine how fast enemy respawns
			enemySpawnTime = TimeSpan.FromSeconds(1.0f);

			// Initialize our random number generator
			random = new Random();

			projectiles = new List<Projectile>();

			// Set the laser to fire every quarter second
			fireTime = TimeSpan.FromSeconds(.15f);



            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load the player resources 
            // Load the player resources
            Animation playerAnimation = new Animation();
            Texture2D playerTexture = Content.Load<Texture2D>("Animation/shipAnimation");
            playerAnimation.Initialize(playerTexture, Vector2.Zero, 115, 69, 8, 30, Color.White, 1f, true);

            Vector2 playerPosition = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X, GraphicsDevice.Viewport.TitleSafeArea.Y + GraphicsDevice.Viewport.TitleSafeArea.Height / 2);
            player.Initialize(playerAnimation, playerPosition);

			enemyTexture = Content.Load<Texture2D>("Animation/mineAnimation");

			projectileTexture = Content.Load<Texture2D>("Texture/laser");


            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        /// 

        private void UpdatePlayer(GameTime gameTime)
        {
            player.Update(gameTime);

			// Update the parallaxing background
			bgLayer1.Update();
			bgLayer2.Update();

			// Fire Interval
			if (gameTime.TotalGameTime - previousFireTime > fireTime)
			{
				// Reset time
				previousFireTime = gameTime.TotalGameTime;

				// Projectile at front and center
				AddProjectile(player.Position + new Vector2(player.Width / 2, 0));
			}

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            //Thumbstick Controls
            player.Position.X += currentGamePadState.ThumbSticks.Left.X
                * playerMoveSpeed;
            player.Position.Y += currentGamePadState.ThumbSticks.Left.Y
                * playerMoveSpeed;
            // Use keyboard/dpad
            if (currentKeyboardState.IsKeyDown(Keys.Left) ||
                currentGamePadState.DPad.Left == ButtonState.Pressed)
            {
                player.Position.X -= playerMoveSpeed;
            }
            
            if (currentKeyboardState.IsKeyDown(Keys.Right) ||
                currentGamePadState.DPad.Right == ButtonState.Pressed)
            {
                player.Position.X += playerMoveSpeed;
            }

            if (currentKeyboardState.IsKeyDown(Keys.Up) ||
                currentGamePadState.DPad.Up == ButtonState.Pressed)
            {
                player.Position.Y -= playerMoveSpeed;
            }

            if (currentKeyboardState.IsKeyDown(Keys.Down) ||
                currentGamePadState.DPad.Down == ButtonState.Pressed)
            {
                player.Position.Y += playerMoveSpeed;
            }

            // No out of bounds
            player.Position.X = MathHelper.Clamp(player.Position.X, 0,
                GraphicsDevice.Viewport.Width - player.Width);
            player.Position.Y = MathHelper.Clamp(player.Position.Y,
                0, GraphicsDevice.Viewport.Height - player.Height);

            // Save the previous state of keys to determine single key presses
            previousGamePadState = currentGamePadState;
            previousKeyboardState = currentKeyboardState;

            // Read current keys and store
            currentKeyboardState = Keyboard.GetState();
            currentGamePadState = GamePad.GetState(PlayerIndex.One);

            // Updates player
            UpdatePlayer(gameTime);
			// Update the enemies
			UpdateEnemies(gameTime);

			// Updates the collision
			UpdateCollision();

			// Updates projectile
			UpdateProjectiles();


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Start drawing 
            spriteBatch.Begin();

			spriteBatch.Draw(mainBackground, Vector2.Zero, Color.White);

			// Draw the moving background
			bgLayer1.Draw(spriteBatch);
			bgLayer2.Draw(spriteBatch);

            // Draw the Player 
            player.Draw(spriteBatch);

			// Draw the Enemies
			for (int i = 0; i<enemies.Count; i++)
			{
				enemies[i].Draw(spriteBatch);
			}

			// Draw Projectiles
			for (int i = 0; i < projectiles.Count; i++)
			{
				projectiles[i].Draw(spriteBatch);
			}


            // Stop drawing
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

		private void AddEnemy()
		{
			// Create the animation object
			Animation enemyAnimation = new Animation();

			// Initialize the animation with the correct animation information
			enemyAnimation.Initialize(enemyTexture, Vector2.Zero, 47, 61, 8, 30, Color.White, 1f, true);

			// Randomly generate the position of the enemy
			Vector2 position = new Vector2(GraphicsDevice.Viewport.Width + enemyTexture.Width / 2, random.Next(100, GraphicsDevice.Viewport.Height - 100));

			// Create an enemy
			Enemy enemy = new Enemy();

			// Initialize the enemy
			enemy.Initialize(enemyAnimation, position);

			// Add the enemy to the active enemies list
			enemies.Add(enemy);
		}

		private void UpdateEnemies(GameTime gameTime)
		{
			// Spawn a new enemy enemy every 1.5 seconds
			if (gameTime.TotalGameTime - previousSpawnTime > enemySpawnTime)
			{
				previousSpawnTime = gameTime.TotalGameTime;

				// Add an Enemy
				AddEnemy();
			}

			// Update the Enemies
			for (int i = enemies.Count - 1; i >= 0; i--)
			{
				enemies[i].Update(gameTime);

				if (enemies[i].Active == false)
				{
					enemies.RemoveAt(i);
				}
			}
		}

		private void UpdateCollision()
		{
			// Use the Rectangle's built-in intersect function to 
			// determine if two objects are overlapping
			Rectangle rectangle1;
			Rectangle rectangle2;

			// Only create the rectangle once for the player
			rectangle1 = new Rectangle((int)player.Position.X, (int)player.Position.Y, player.Width, player.Height);

			// Do the collision between the player and the enemies
			for (int i = 0; i < enemies.Count; i++)
			{
				rectangle2 = new Rectangle((int)enemies[i].Position.X, (int)enemies[i].Position.Y, enemies[i].Width, enemies[i].Height);

				// Determine if the two objects collided with each other
				if (rectangle1.Intersects(rectangle2))
				{
					// Subtract the health from the player based on
					// the enemy damage
					player.Health -= enemies[i].Damage;

					// Since the enemy collided with the player
					// destroy it
					enemies[i].Health = 0;

					// If the player health is less than zero we died
					if (player.Health <= 0)
					{
						player.Active = false;
					}
				}
			}

			for (int i = 0; i < projectiles.Count; i++)
			{
				for (int j = 0; j < enemies.Count; j++)
				{
					// Create rectangles to determine collision
					rectangle1 = new Rectangle((int)projectiles[i].Position.X -
											   projectiles[i].Width / 2,
											   (int)projectiles[i].Position.Y - 
					                           projectiles[i].Height / 2,
											   projectiles[i].Width, projectiles[i].Height);
					rectangle2 = new Rectangle((int)enemies[j].Position.X -
											   enemies[j].Width / 2,
											   (int)enemies[j].Position.Y -
											   enemies[j].Height / 2,
											   enemies[j].Width,
											   enemies[j].Height);
					// Determine collision
					if (rectangle1.Intersects(rectangle2))
					{
						enemies[j].Health -= projectiles[i].Damage;
						projectiles[i].Active = false;
					}
				}
			}
		}

		private void AddProjectile(Vector2 position)
		{
			Projectile projectile = new Projectile();
			projectile.Initialize(GraphicsDevice.Viewport, projectileTexture, position);
			projectiles.Add(projectile);
		}

		private void UpdateProjectiles()
		{
			// Update projectiles
			for (int i = projectiles.Count - 1; i >= 0; i--)
			{
				projectiles[i].Update();

				if (projectiles[i].Active == false)
				{
					projectiles.RemoveAt(i);
				}
			}
		}
    }
}
