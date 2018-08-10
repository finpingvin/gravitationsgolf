using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TennisPennis
{
    public class Player
    {
        private Vector2 _velocity;
        private Vector2 _pos;
        private readonly Texture2D _sprite;
        private KeyboardState _lastKeyboardState;
        private readonly Ball _ball;

        public Player(Texture2D sprite, Ball ball)
        {
            _velocity = new Vector2(0, 0);
            _pos = new Vector2(0, 0);
            _sprite = sprite;
            _ball = ball;
        }
        
        public void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();
            // TODO: Movement will need to be more solid, can end up in inconsistent state now
            if ((_lastKeyboardState != null && !_lastKeyboardState.IsKeyDown(Keys.Up) && keyboardState.IsKeyDown(Keys.Up)) || (_lastKeyboardState != null && _lastKeyboardState.IsKeyDown(Keys.Down) && keyboardState.IsKeyUp(Keys.Down)))
            {
                _velocity.Y -= 200 * (float) gameTime.ElapsedGameTime.TotalSeconds;
            }
            
            if ((_lastKeyboardState != null && !_lastKeyboardState.IsKeyDown(Keys.Down) && keyboardState.IsKeyDown(Keys.Down)) || (_lastKeyboardState != null && _lastKeyboardState.IsKeyDown(Keys.Up) && keyboardState.IsKeyUp(Keys.Up)))
            {
                _velocity.Y += 200 * (float) gameTime.ElapsedGameTime.TotalSeconds;
            }
            
            if ((_lastKeyboardState != null && !_lastKeyboardState.IsKeyDown(Keys.Left) && keyboardState.IsKeyDown(Keys.Left)) || (_lastKeyboardState != null && _lastKeyboardState.IsKeyDown(Keys.Right) && keyboardState.IsKeyUp(Keys.Right)))
            {
                _velocity.X -= 200 * (float) gameTime.ElapsedGameTime.TotalSeconds;
            }

            if ((_lastKeyboardState != null && !_lastKeyboardState.IsKeyDown(Keys.Right) && keyboardState.IsKeyDown(Keys.Right)) || (_lastKeyboardState != null && _lastKeyboardState.IsKeyDown(Keys.Left) && keyboardState.IsKeyUp(Keys.Left)))
            {
                _velocity.X += 200 * (float) gameTime.ElapsedGameTime.TotalSeconds;
            }

            _lastKeyboardState = keyboardState;

            _pos += _velocity;

            if (IsSwinging)
            {
                // Ball collision
                var distance = Vector2.Distance(_pos, _ball.Pos);
                // A bit larger collision area than sprite
                if (distance < ((_sprite.Width / 2) + 100) + (_ball.Radius))
                {
                    // Collision!
                    _ball.Swinging(this);
                }
            }
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_sprite, _pos, Color.White);
        }

        private static bool IsSwinging => Keyboard.GetState().IsKeyDown(Keys.Space);

        public Vector2 Pos => _pos;
        public int Width => _sprite.Width;
    }
}