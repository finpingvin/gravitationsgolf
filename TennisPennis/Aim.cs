using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TennisPennis
{
    public class Aim
    {
        private readonly Vector2 _pos;
        private readonly Texture2D _sprite;
        private float _rotation = 0.0F;
        
        public Aim(Vector2 pos, Texture2D sprite)
        {
            _pos = pos;
            _sprite = sprite;
        }
        
        public void Update(GameTime gameTime)
        {
            KeyboardState keyboard = Keyboard.GetState();
            
            if (keyboard.IsKeyDown(Keys.Left))
            {
                _rotation -= 1 * (float) gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (keyboard.IsKeyDown(Keys.Right))
            {
                _rotation += 1 * (float) gameTime.ElapsedGameTime.TotalSeconds;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_sprite, _pos, null, Color.White, _rotation, new Vector2(_sprite.Width / 2, _sprite.Height), 1.0F, SpriteEffects.None, 1.0F);
        }

        public float Rotation => _rotation;
    }
}