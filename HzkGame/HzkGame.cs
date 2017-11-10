using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoHzk;
namespace HzkGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class HzkGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont smallFont;
        byte[] hzkData;
        byte[] ascData;
        string debugWord;
        Hzk Hzk16;
        Texture2D hzkT2D;
        
        public HzkGame()
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
            // TODO: use this.Content to load your game content here
            ascData= Content.Load<byte[]>("kaitiasc64");
            hzkData = Content.Load<byte[]>("kaiti64");
            smallFont = Content.Load<SpriteFont>("smallfont");
            
            Hzk16 = new Hzk(hzkData, ascData,64);
            hzkT2D = Hzk16.DrawText(GraphicsDevice, "鞍美噗婆凹\n等猫喵\n空山新雨后，天气晚来秋。\n明月松间照，清泉石上流。",
                TextAlignEnum.Middle | TextAlignEnum.Center, 0, 0);
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
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

            spriteBatch.Draw(hzkT2D, new Rectangle(11, 11, hzkT2D.Width / 2, hzkT2D.Height / 2), Color.Black);
            spriteBatch.Draw(hzkT2D, new Rectangle(10, 10, hzkT2D.Width / 2, hzkT2D.Height / 2), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}