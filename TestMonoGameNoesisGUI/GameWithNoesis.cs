﻿namespace TestMonoGameNoesisGUI
{
    #region

    using System;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    using NoesisGUI.MonoGameWrapper;

    #endregion

    /// <summary>
    /// This is an example MonoGame game using NoesisGUI
    /// </summary>
    public class GameWithNoesis : Game
    {
        #region Fields

        readonly GraphicsDeviceManager graphics;

        private TimeSpan lastUpdateTotalGameTime;

        private NoesisWrapper noesisGUIWrapper;

        SpriteBatch spriteBatch;

        #endregion

        #region Constructors and Destructors

        public GameWithNoesis()
        {
            graphics = new GraphicsDeviceManager(this);
            this.graphics.GraphicsProfile = GraphicsProfile.HiDef;
            Content.RootDirectory = "Content";
        }

        #endregion

        #region Methods

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            this.noesisGUIWrapper.PreRender();

            this.GraphicsDevice.Clear(
                ClearOptions.Target | ClearOptions.DepthBuffer | ClearOptions.Stencil,
                Color.CornflowerBlue,
                1,
                0);

            // TODO: Add your drawing code here

            this.noesisGUIWrapper.PostRender();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            this.IsMouseVisible = true;

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

            CreateNoesisGUI();

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            DestroyNoesisGUI();
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            this.lastUpdateTotalGameTime = gameTime.TotalGameTime;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
                || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            this.noesisGUIWrapper.Update(gameTime, isWindowActive: this.IsActive);

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        private void CreateNoesisGUI()
        {
            var config = new NoesisConfig(
                this.Window,
                this.graphics,
                rootXamlFilePath: "TextBox.xaml",
                themeXamlFilePath: "NoesisStyle.xaml",
                currentTotalGameTime: this.lastUpdateTotalGameTime);
            config.SetupInputFromWindows();
            config.SetupProviderSimpleFolder("Data");

            this.noesisGUIWrapper = new NoesisWrapper(config);
        }

        private void DestroyNoesisGUI()
        {
            if (this.noesisGUIWrapper == null)
            {
                return;
            }

            this.noesisGUIWrapper.Dispose();
            this.noesisGUIWrapper = null;
        }

        #endregion
    }
}