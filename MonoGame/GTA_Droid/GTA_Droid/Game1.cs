using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System;

namespace GTA_Droid
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SoundEffect jetSound;
        SoundEffect tableSaw;
        public static Mission mission;
        public static Player pl;
        public static SpriteFont font1;
        SoundEffectInstance jetSoundInstance;
        public Camera camera;
        public float menuScale;
        public AnalogStick analog_stick;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";

            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = graphics.PreferredBackBufferWidth;
            graphics.PreferredBackBufferHeight = graphics.PreferredBackBufferHeight;
            graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft;//| DisplayOrientation.LandscapeRight;

            if (graphics.PreferredBackBufferWidth / 1920f > graphics.PreferredBackBufferHeight / 1080f)
                menuScale = graphics.PreferredBackBufferHeight / 1080f;
            else
                menuScale = graphics.PreferredBackBufferWidth / 1920f;

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
            font1 = Content.Load<SpriteFont>("small");
            // TODO: use this.Content to load your game content here

            analog_stick = new AnalogStick(new Vector2(270 * menuScale, 850 * menuScale));
            new LevelLoader("s");
            
            for (int i = 0; i < 12; i++)
                new AI();

            for (int i = 0; i < 20; i++)
                new Bird(new String[3] { "Birds/Bird1/bird0", "Birds/Bird1/bird1", "Birds/Bird1/bird2", });
            camera = new Camera(GraphicsDevice.Viewport, new Player());
            mission = new Mission();
        }
        public Texture2D LoadContent(String s)
        {
            return Content.Load<Texture2D>(s);
        }
        public SoundEffect LoadSound(String s)
        {
            return Content.Load<SoundEffect>("Sounds/" + s);
        }
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        int frameRate = 0;
        int frameCounter = 0;
        TimeSpan elapsedTime = TimeSpan.Zero;
        protected override void Update(GameTime gameTime)
        {
            elapsedTime += gameTime.ElapsedGameTime;

            if (elapsedTime > TimeSpan.FromSeconds(1))
            {
                elapsedTime -= TimeSpan.FromSeconds(1);
                frameRate = frameCounter;
                frameCounter = 0;
            }
            float speed = gameTime.ElapsedGameTime.Milliseconds / 100.0f;
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            GamePadState gamePad = GamePad.GetState(PlayerIndex.One);
            KeyboardState kb = Keyboard.GetState();
            for (int i = 0; i < GameObject.gameObjects.Count; i++)
                GameObject.gameObjects[i].Update(gameTime);
            camera.Update(gameTime);
            mission.Update(gameTime);
            analog_stick.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        Helicopter h;
        protected override void Draw(GameTime gameTime)
        {
            frameCounter++;
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, null, null, null, null, camera.getTransform());
            //TODO: Replace draw calls with Z-ordering
            for (int i = 0; i < GameObject.gameObjects.Count; i++)
                if (GameObject.gameObjects[i].GetType() == typeof(Tile))
                    spriteBatch.Draw(GameObject.gameObjects[i].getTex(),
                        GameObject.gameObjects[i].getPos(), null, Color.White,
                        GameObject.gameObjects[i].getRotation(),
                        GameObject.gameObjects[i].getCenter(),
                        GameObject.gameObjects[i].getScale(),
                        SpriteEffects.None, 1.0f);

            for (int i = 0; i < GameObject.gameObjects.Count; i++)
                if (GameObject.gameObjects[i].GetType() == typeof(BloodSplatter))
                    spriteBatch.Draw(GameObject.gameObjects[i].getTex(),
                        GameObject.gameObjects[i].getPos(), null, Color.White,
                        GameObject.gameObjects[i].getRotation(),
                        GameObject.gameObjects[i].getCenter(),
                        GameObject.gameObjects[i].getScale(),
                        SpriteEffects.None, 1.0f);
            for (int i = 0; i < GameObject.gameObjects.Count; i++)
                if (GameObject.gameObjects[i].GetType() == typeof(Bomb))
                    spriteBatch.Draw(GameObject.gameObjects[i].getTex(),
                        GameObject.gameObjects[i].getPos(), null, Color.White,
                        GameObject.gameObjects[i].getRotation(),
                        GameObject.gameObjects[i].getCenter(),
                        GameObject.gameObjects[i].getScale(),
                        SpriteEffects.None, 1.0f);
            for (int i = 0; i < GameObject.gameObjects.Count; i++)
                if (GameObject.gameObjects[i].GetType() == typeof(AI))
                    spriteBatch.Draw(GameObject.gameObjects[i].getTex(),
                        GameObject.gameObjects[i].getPos(), null, Color.White,
                        GameObject.gameObjects[i].getRotation(),
                        GameObject.gameObjects[i].getCenter(),
                        GameObject.gameObjects[i].getScale(),
                        SpriteEffects.None, 1.0f);

            for (int i = 0; i < GameObject.gameObjects.Count; i++)
                if (GameObject.gameObjects[i].GetType() != typeof(AI) && GameObject.gameObjects[i].GetType() != typeof(BloodSplatter)
                    && GameObject.gameObjects[i].GetType() != typeof(Tile) && GameObject.gameObjects[i].GetType() != typeof(ColBox) &&
                    GameObject.gameObjects[i].GetType() != typeof(Waypoint) &&
                    GameObject.gameObjects[i].GetType() != typeof(Bird) &&
                    GameObject.gameObjects[i].GetType() != typeof(Helicopter) &&
                    GameObject.gameObjects[i].GetType() != typeof(Ring)
                    && GameObject.gameObjects[i].GetType() != typeof(Bomb))
                    spriteBatch.Draw(GameObject.gameObjects[i].getTex(),
                        GameObject.gameObjects[i].getPos(), null, Color.White,
                        GameObject.gameObjects[i].getRotation(),
                        GameObject.gameObjects[i].getCenter(),
                        GameObject.gameObjects[i].getScale(),
                        SpriteEffects.None, 1.0f);

            /*string fps = string.Format("fps: {0}", frameRate);
             spriteBatch.DrawString(font1, fps, new Vector2(33+512, 33+32), Color.Black);
             spriteBatch.DrawString(font1, fps, new Vector2(32+512, 32+32), Color.White);
             spriteBatch.DrawString(font1, "Hello World", new Vector2(32 + 256, 32 + 96), Color.White);
            */
            spriteBatch.End();

            mission.Draw(spriteBatch);
            base.Draw(gameTime);
        }
    }
}
