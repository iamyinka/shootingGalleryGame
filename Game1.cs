﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace ShootingGallery
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D targetSprite;
        Texture2D crosshairsSprite;
        Texture2D backgroundSprite;
        SpriteFont gameFont;

        Vector2 targetPosition = new Vector2 (300, 300);
        const int targetRadius = 45;
        int score = 0;

        MouseState mState;
        bool mReleased = true;

        double timer = 10;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            targetSprite = Content.Load<Texture2D>("target");
            crosshairsSprite = Content.Load<Texture2D>("crosshairs");
            backgroundSprite = Content.Load<Texture2D>("sky");
            gameFont = Content.Load<SpriteFont>("galleryFont");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            if (timer > 0)
            {
                timer -= gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (timer < 0)
            {
                timer = 0;
            }

            mState = Mouse.GetState();

            if (mState.LeftButton == ButtonState.Pressed && mReleased)
            {
                float mouseTargetPosition = Vector2.Distance(targetPosition, mState.Position.ToVector2());
                if (mouseTargetPosition < targetRadius && timer > 0)
                {
                    score++;

                    Random rand = new Random();

                    targetPosition.X = rand.Next(0, _graphics.PreferredBackBufferWidth - 1);
                    targetPosition.Y = rand.Next(0, _graphics.PreferredBackBufferHeight - 1);
                }
                mReleased = false;
            }

            if (mState.LeftButton == ButtonState.Released)
            {
                mReleased = true;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(backgroundSprite, new Vector2(0, 0), Color.White);
            _spriteBatch.DrawString(gameFont, $"Your current score is {score}", new Vector2(3, 3), Color.White);
            if(timer > 0)
            {
                _spriteBatch.DrawString(gameFont, $"You have {Math.Ceiling(timer)} secs left", new Vector2(3, 30), Color.White);
                _spriteBatch.Draw(targetSprite, new Vector2(targetPosition.X - targetRadius, targetPosition.Y - targetRadius), Color.White);
            } else
            {
                _spriteBatch.DrawString(gameFont, $"Game Over!!!", new Vector2(3, 30), Color.White);
            }

            _spriteBatch.Draw(crosshairsSprite, new Vector2(mState.X - 25, mState.Y - 25), Color.White);  
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}