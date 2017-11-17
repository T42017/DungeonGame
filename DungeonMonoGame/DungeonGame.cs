using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DungeonMonoGame
{
    public class DungeonGame : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _dungeonTile;

        private Vector2Int _position = new Vector2Int(1, 1);
        private Vector2Int _direction = new Vector2Int(0, -1);
        readonly List<ViewPortPositions> _viewPortPositions = new List<ViewPortPositions>();
        private Rectangle _rectVoid = new Rectangle(88, 0, 16, 32);
        private Vector2 _positionVoid = new Vector2(16, 0);

        private const int MapWidth = 10;
        private const int MapHeight = 10;
        private const float Scale = 10f;

        private readonly int[,] _map = 
        {
            {2,2,1,2,1,2,1,2,1,2},
            {1,0,0,0,0,0,0,0,0,1},
            {2,0,0,2,1,3,1,2,1,2},
            {1,0,1,2,1,0,2,0,0,1},
            {2,0,2,0,2,0,1,0,0,2},
            {1,0,1,0,1,0,3,0,0,1},
            {2,0,2,0,2,0,1,0,0,2},
            {1,0,1,0,1,0,2,1,2,1},
            {2,0,2,0,0,0,1,1,1,2},
            {1,2,1,2,1,2,2,1,2,1}
        };

        public DungeonGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            Components.Add(new KeyboardComponent(this));
        }

        protected override void Initialize()
        {
            var rectFront = new Rectangle(0, 0, 32, 32);
            var rectFrontLeft = new Rectangle(0, 0, 8, 32);
            var rectFrontRight = new Rectangle(24, 0, 8, 32);
            var rectFrontFar = new Rectangle(32, 0, 16, 32);
            var rectLeft = new Rectangle(48, 0, 8, 32);
            var rectRight = new Rectangle(58, 0, 8, 32);
            var rectLeftFar = new Rectangle(68, 0, 8, 32);
            var rectRightFar = new Rectangle(78, 0, 8, 32);
            var rectFrontFarFar = new Rectangle(116, 0, 10, 32);
            var rectFrontFarLeft = new Rectangle(106, 0, 3, 32);
            var rectFrontFarRight = new Rectangle(111, 0, 3, 32);

            //Back row
            _viewPortPositions.Add(new ViewPortPositions() { DrawPosition = new Vector2(0, 0), SourceRect = new Rectangle(117, 0, 9, 32) });
            _viewPortPositions.Add(new ViewPortPositions() { DrawPosition = new Vector2(9, 0), SourceRect = rectFrontFarFar });
            _viewPortPositions.Add(new ViewPortPositions() { DrawPosition = new Vector2(19, 0), SourceRect = rectFrontFarFar });
            _viewPortPositions.Add(new ViewPortPositions() { DrawPosition = new Vector2(29, 0), SourceRect = rectFrontFarFar });
            _viewPortPositions.Add(new ViewPortPositions() { DrawPosition = new Vector2(39, 0), SourceRect = new Rectangle(116, 0, 9, 32) });

            //Back sides
            _viewPortPositions.Add(new ViewPortPositions() { DrawPosition = new Vector2(0, 0), SourceRect = new Rectangle(128, 0, 9, 32) });
            _viewPortPositions.Add(new ViewPortPositions() { DrawPosition = new Vector2(39, 0), SourceRect = new Rectangle(139, 0, 9, 32) });
            _viewPortPositions.Add(new ViewPortPositions() { DrawPosition = new Vector2(16, 0), SourceRect = rectFrontFarLeft });
            _viewPortPositions.Add(new ViewPortPositions() { DrawPosition = new Vector2(29, 0), SourceRect = rectFrontFarRight });

            // Position F
            _viewPortPositions.Add(new ViewPortPositions() {DrawPosition = new Vector2(0, 0), SourceRect = rectFrontFar});
            // Position D
            _viewPortPositions.Add(new ViewPortPositions() {DrawPosition = new Vector2(16, 0), SourceRect = rectFrontFar});
            // Position E
            _viewPortPositions.Add(new ViewPortPositions() {DrawPosition = new Vector2(32, 0), SourceRect = rectFrontFar});
            // Position C
            _viewPortPositions.Add(new ViewPortPositions() {DrawPosition = new Vector2(8, 0), SourceRect = rectLeftFar});
            // Position CF
            _viewPortPositions.Add(new ViewPortPositions() { DrawPosition = new Vector2(0, 0), SourceRect = rectFrontLeft });
            // Position B
            _viewPortPositions.Add(new ViewPortPositions() {DrawPosition = new Vector2(32, 0), SourceRect = rectRightFar});
            // Position BF
            _viewPortPositions.Add(new ViewPortPositions() { DrawPosition = new Vector2(40, 0), SourceRect = rectFrontRight });
            // Position A
            _viewPortPositions.Add(new ViewPortPositions() {DrawPosition = new Vector2(8, 0), SourceRect = rectFront});
            // Position L
            _viewPortPositions.Add(new ViewPortPositions() {DrawPosition = new Vector2(0, 0), SourceRect = rectLeft});
            // Position R
            _viewPortPositions.Add(new ViewPortPositions() {DrawPosition = new Vector2(40, 0), SourceRect = rectRight});
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _dungeonTile = Content.Load<Texture2D>("dungeon");

            ScreenManager.Instance.LoadContent(Content);
        }

        protected override void UnloadContent()
        {
            ScreenManager.Instance.UnloadContent();

        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            if ((KeyboardComponent.KeyPressed(Keys.Delete) || KeyboardComponent.KeyPressed(Keys.Q)) &&
                _map.Index(_position + _direction.Rotate90DegreesLeft()) != (int)TileType.Wall1 &&
                _map.Index(_position + _direction.Rotate90DegreesLeft()) != (int)TileType.Wall2)
                _position += _direction.Rotate90DegreesLeft();
            if ((KeyboardComponent.KeyPressed(Keys.PageDown) || KeyboardComponent.KeyPressed(Keys.E)) &&
                _map.Index(_position + _direction.Rotate90DegreesRight()) != (int)TileType.Wall1 &&
                _map.Index(_position + _direction.Rotate90DegreesRight()) != (int)TileType.Wall2)
                _position += _direction.Rotate90DegreesRight();
            if ((KeyboardComponent.KeyPressed(Keys.Up) || KeyboardComponent.KeyPressed(Keys.W)) &&
                _map.Index(_position + _direction) != (int)TileType.Wall1 &&
                _map.Index(_position + _direction) != (int)TileType.Wall2)
                _position += _direction;
            if ((KeyboardComponent.KeyPressed(Keys.Down) || KeyboardComponent.KeyPressed(Keys.S)) &&
                _map.Index(_position - _direction) != (int)TileType.Wall1 &&
                _map.Index(_position - _direction) != (int)TileType.Wall2)
                _position -= _direction;
            if (KeyboardComponent.KeyPressed(Keys.Left) || KeyboardComponent.KeyPressed(Keys.A))
                _direction = _direction.Rotate90DegreesLeft();
            if (KeyboardComponent.KeyPressed(Keys.Right) || KeyboardComponent.KeyPressed(Keys.D))
                _direction = _direction.Rotate90DegreesRight();

            // Back row
            _viewPortPositions[0].Index = _position + 3*_direction + 2*_direction.Rotate90DegreesLeft();
            _viewPortPositions[1].Index = _position + 3*_direction + 1*_direction.Rotate90DegreesLeft();
            _viewPortPositions[2].Index = _position + 3 * _direction;
            _viewPortPositions[3].Index = _position + 3*_direction + 1*_direction.Rotate90DegreesRight();
            _viewPortPositions[4].Index = _position + 3*_direction + 2*_direction.Rotate90DegreesRight();

            // Back sides
            _viewPortPositions[5].Index = _position + 2*_direction + 2*_direction.Rotate90DegreesLeft();
            _viewPortPositions[6].Index = _position + 2*_direction + 2*_direction.Rotate90DegreesRight();
            _viewPortPositions[7].Index = _position + 2 * _direction + _direction.Rotate90DegreesLeft();
            _viewPortPositions[8].Index = _position + 2 * _direction + _direction.Rotate90DegreesRight();

            //Position F
            _viewPortPositions[9].Index = _position + 2*_direction + _direction.Rotate90DegreesLeft();
            //Position D
            _viewPortPositions[10].Index = _position + 2*_direction;
            //Position E
            _viewPortPositions[11].Index = _position + 2*_direction + _direction.Rotate90DegreesRight();
            //Position C
            _viewPortPositions[12].Index = _position + _direction + _direction.Rotate90DegreesLeft();
            //Position CF
            _viewPortPositions[13].Index = _viewPortPositions[12].Index;
            //Position B
            _viewPortPositions[14].Index = _position + _direction + _direction.Rotate90DegreesRight();
            //Position BF
            _viewPortPositions[15].Index = _viewPortPositions[14].Index;
            //Position A
            _viewPortPositions[16].Index = _position + _direction;
            //Position L
            _viewPortPositions[17].Index = _position + _direction.Rotate90DegreesLeft();
            //Position R
            _viewPortPositions[18].Index = _position + _direction.Rotate90DegreesRight();

            ScreenManager.Instance.Update(gameTime);
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend,
                SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);

            _spriteBatch.Draw(_dungeonTile, Vector2.Zero, _rectVoid, Color.White, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
            _spriteBatch.Draw(_dungeonTile, _positionVoid * Scale, _rectVoid, Color.White, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
            _spriteBatch.Draw(_dungeonTile, _positionVoid * 8 * Scale, _rectVoid, Color.White, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);

            foreach (var vp in _viewPortPositions)
            {
                if (!vp.Index.IsIndexable(MapWidth, MapHeight) || _map[vp.Index.Y, vp.Index.X] == (int) TileType.Nothing)
                    continue;

                int tile = _map[vp.Index.Y, vp.Index.X] - 1;
                var rect = new Rectangle(vp.SourceRect.X, vp.SourceRect.Y + tile*34, vp.SourceRect.Width,
                    vp.SourceRect.Height);
                _spriteBatch.Draw(_dungeonTile, vp.DrawPosition*Scale, rect, Color.White, 0f, Vector2.Zero, Scale,
                    SpriteEffects.None, 0f);
            }
            
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        class ViewPortPositions
        {
            public Vector2Int Index { get; set; }
            public Rectangle SourceRect { get; set; }
            public Vector2 DrawPosition { get; set; }
        }
    }
}
