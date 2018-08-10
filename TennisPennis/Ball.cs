using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TennisPennis
{
    public class Ball
    {
        private Vector2 _pos;
        private Vector2 _velocity;
        private readonly Texture2D _sprite;
        //private GraphicsDevice _graphicsDevice;
        private Player _swingingPlayer;
        private Vector2 _force;
        
        //public Ball(Texture2D sprite, GraphicsDevice graphicsDevice)
        public Ball(Vector2 pos, Texture2D sprite)
        {
            _sprite = sprite;
            //_graphicsDevice = graphicsDevice;
            //_pos = new Vector2(10, _graphicsDevice.Viewport.Height - _sprite.Height - 10);
            _pos = pos;
            //_velocity = new Vector2(50, 0);
        }

        public void Update(GameTime gameTime, IEnumerable<Planet> planets, Goal goal)
        {
            // Ball collision
            var distanceGoal = Vector2.Distance(_pos, goal.CenterPos());
            // A bit larger collision area than sprite
            if (distanceGoal < ((_sprite.Width / 2)) + (goal.Radius))
            {
                Console.WriteLine("Goaaaaal!");
            }
            
            //var gameHeight = _graphicsDevice.Viewport.Height;
            //var gameWidth = _graphicsDevice.Viewport.Width;
            var centerPos = new Vector2(_pos.X + (_sprite.Width / 2), _pos.Y + (_sprite.Width / 2));
            _force = new Vector2(0, 0);

            foreach (var p in planets)
            {
                var planetCenterPos = p.CenterPos();
                var delta = (planetCenterPos - centerPos);
                var distance = delta.Length();
                var g = 10;
                var m1 = 10000;
                var m2 = 1;
                var f = g * m1 * m2 / Math.Pow(distance, 2);
                var theta = Math.Atan2(delta.Y, delta.X);
                var force = new Vector2((float) (Math.Cos(theta) * f), (float) (Math.Sin(theta) * f));
                _force += force;
            }
            
            _velocity += _force * new Vector2((float) gameTime.ElapsedGameTime.TotalSeconds, (float) gameTime.ElapsedGameTime.TotalSeconds);
            
            /*if (_swingingPlayer != null)
            {
                var centerPos = new Vector2(_pos.X + (_sprite.Width / 2), _pos.Y + (_sprite.Width / 2));
                var playerCenterPos = new Vector2(_swingingPlayer.Pos.X + (_swingingPlayer.Width / 2), _swingingPlayer.Pos.Y + (_swingingPlayer.Width / 2));
                //var direction = centerPos - playerCenterPos;
                //direction.Normalize();
                //var rotation = new Vector2(-direction.Y, direction.X);
                //_velocity = rotation * 200;
                var delta = (playerCenterPos - centerPos);
                var distance = delta.Length();
                var g = 10;
                var m1 = 10000;
                var m2 = 1;
                var f = g * m1 * m2 / Math.Pow(distance, 2);
                var theta = Math.Atan2(delta.Y, delta.X);
                var force = new Vector2((float) (Math.Cos(theta) * f), (float) (Math.Sin(theta) * f));
                _velocity += force * new Vector2((float) gameTime.ElapsedGameTime.TotalSeconds * 3, (float) gameTime.ElapsedGameTime.TotalSeconds * 3);
            }*/
            
            /*if (_pos.X <= 0 || _pos.X >= gameWidth)
            {
                _velocity.X *= -1;
            }

            if (_pos.Y <= 0 || _pos.Y >= gameHeight)
            {
                _velocity.Y *= -1;
            }*/

            _pos += _velocity * (float) gameTime.ElapsedGameTime.TotalSeconds;
           // _swingingPlayer = null;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_sprite, _pos, Color.White);
        }

        public void Swinging(Player player)
        {
            _swingingPlayer = player;
        }

        public int Radius => _sprite.Width / 2;
        public Vector2 Pos => _pos;

        public Vector2 Velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        }
    }
}