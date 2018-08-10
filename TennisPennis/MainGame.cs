using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TennisPennis
{
    public class MainGame : Game
    {
        private GraphicsDeviceManager _graphicsDevice;
        private SpriteBatch _spriteBatch;
        private Texture2D _earthImg;
        private Texture2D _ballImg;
        private Texture2D _pointerImg;
        private Texture2D _redoImg;
        private Texture2D _goalImg;
        private Texture2D _aimImg;
        //private Player _player;
        private List<Planet> _planets;
        private Ball _ball;
        private Goal _goal;
        private Redo _redo;
        private Aim _aim;
        private float _speed = 0.0F;
        
        private enum ClickState
        {
            None,
            Direction,
            Speed,
            AddingSpeed,
            Launched
        }

        private ClickState _currentClickState = ClickState.None;
        
        public MainGame()
        {
            _graphicsDevice = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content/bin";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _earthImg = Content.Load<Texture2D>("Earth");
            _ballImg = Content.Load<Texture2D>("Ball");
            _pointerImg = Content.Load<Texture2D>("Pointer");
            _redoImg = Content.Load<Texture2D>("Redo");
            _goalImg = Content.Load<Texture2D>("Goal");
            _aimImg = Content.Load<Texture2D>("Aim");
            
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _ball = new Ball(new Vector2(_graphicsDevice.GraphicsDevice.Viewport.Width / 2, _graphicsDevice.GraphicsDevice.Viewport.Height - 10), _ballImg);
            _planets = new List<Planet>(new Planet[]
            {
                new Planet(new Vector2(100, 100), _earthImg),
                new Planet(new Vector2(200, 200), _earthImg),
                new Planet(new Vector2(300, 300), _earthImg)
            });
            _goal = new Goal(new Vector2(150, 150), _goalImg);
            _redo = new Redo(new Vector2(0, 0), _redoImg);
            _aim = new Aim(new Vector2(_graphicsDevice.GraphicsDevice.Viewport.Width / 2, _graphicsDevice.GraphicsDevice.Viewport.Height - 10), _aimImg);
            //_player = new Player(_earthImg, _ball);
        }

        protected override void UnloadContent()
        {
            Content.Unload();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back ==
                ButtonState.Pressed || Keyboard.GetState().IsKeyDown(
                    Keys.Escape))
            {
                Exit();
            }
            else
            {
                MouseState mouse = Mouse.GetState();
                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    //_planets.Add(new Planet(new Vector2(mouse.X, mouse.Y), _earthImg));
                    /*if (_currentClickState == ClickState.Direction)
                    {
                        _currentClickState = ClickState.Speed;
                    }*/

                    if (_currentClickState == ClickState.None)
                    {
                        _currentClickState = ClickState.AddingSpeed;
                    }
                }

                if (mouse.LeftButton == ButtonState.Released)
                {
                    if (_currentClickState == ClickState.AddingSpeed)
                    {
                        _currentClickState = ClickState.Launched;
                        _ball.Velocity =  Vector2.Transform(new Vector2(0.0F, -_speed), Matrix.CreateRotationZ(_aim.Rotation));
                    }
                }

                if (_currentClickState == ClickState.AddingSpeed)
                {
                    _speed += 60 * (float) gameTime.ElapsedGameTime.TotalSeconds;
                }
                
                _goal.Update(gameTime);
                _redo.Update(gameTime);
                _aim.Update(gameTime);
                
                //_player.Update(gameTime);
                foreach (var p in _planets)
                {
                    p.Update(gameTime);
                }
                
                if (_currentClickState == ClickState.Launched)
                {
                    _ball.Update(gameTime, _planets, _goal);
                }
            }
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();
            //_player.Draw(_spriteBatch);
            foreach (var p in _planets)
            {
                p.Draw(_spriteBatch);
            }

            if (_currentClickState == ClickState.Launched)
            {
                _ball.Draw(_spriteBatch);    
            }
            
            _goal.Draw(_spriteBatch);
            _redo.Draw(_spriteBatch);
            _aim.Draw(_spriteBatch);

            MouseState mouse = Mouse.GetState();
            _spriteBatch.Draw(_pointerImg, new Vector2(mouse.X, mouse.Y), Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}