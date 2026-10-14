using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
namespace TP_Physique
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D pixel;


        readonly Vector2 GRAVITY = new Vector2(0, 9.81f);
        public Mass mass = new Mass(5, new Vector2(3,3));
        public Vector2 pivot = new Vector2(8,3);

        public float epsilon = 0.00001f;

        public float max_distance = 5;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();

        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            pixel = new Texture2D(GraphicsDevice, 1, 1);
            pixel.SetData(new[] { Color.White });
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                Vector2 fromPivot = mass.position - pivot;


                float distance = fromPivot.Length();


                float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;


                float dot = Vector2.Dot(fromPivot, GRAVITY);


                Vector2 tension = -(dot/ fromPivot.LengthSquared()) * fromPivot;


                mass.acceleration = GRAVITY + tension*3;

                mass.velocity += mass.acceleration * dt;
                mass.position += mass.velocity * dt;

                fromPivot = mass.position - pivot;

                distance = fromPivot.Length();

                if (distance > max_distance+ epsilon || distance < max_distance- epsilon)
                {
                    mass.position = pivot + Vector2.Normalize(fromPivot) * max_distance;
                }

            }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();
            DrawCircle(mass.position * 72f, mass.mass*3, Color.White);
            DrawCircle(pivot * 72f, 10, Color.Red);
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        public void DrawCircle(
        Vector2 position,
        float radius,
        Color color,
        int segments = 64)
        {
            float step = MathHelper.TwoPi / segments;

            Vector2 previous = position + new Vector2(radius, 0);

            for (int i = 1; i <= segments; i++)
            {
                float angle = i * step;

                Vector2 current = position + new Vector2(
                    MathF.Cos(angle) * radius,
                    MathF.Sin(angle) * radius
                );

                DrawLine( previous, current, color);

                previous = current;
            }
        }

        public void DrawLine(
        Vector2 start,
        Vector2 end,
        Color color)
        {
            Vector2 direction = end - start;
            float length = direction.Length();
            float angle = MathF.Atan2(direction.Y, direction.X);

            _spriteBatch.Draw(
                pixel,
                start,
                null,
                color,
                angle,
                new Vector2(0, 0.5f),
                new Vector2(length, 1),
                SpriteEffects.None,
                0
            );
        }
    }
}
