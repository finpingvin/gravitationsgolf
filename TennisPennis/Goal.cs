using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TennisPennis
{
    public class Goal
    {
        private readonly Vector2 _pos;
        private readonly Texture2D _sprite;
        
        public Goal(Vector2 pos, Texture2D sprite)
        {
            _pos = pos;
            _sprite = sprite;
        }
        
        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_sprite, _pos, Color.White);
        }

        public Vector2 CenterPos()
        {
            return new Vector2(_pos.X + (_sprite.Width / 2), _pos.Y + (_sprite.Width / 2));
        }

        public int Radius => _sprite.Width / 2;
    }
}