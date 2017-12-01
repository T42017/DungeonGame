using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace DungeonMonoGame 
{
    class PlayerStatus : DrawableGameComponent
    {
        public int Health;
        public string Name;
        public int Money;
        public int AttackDamage;
        public int MaxHealth;  
        public int Experience;

        private SpriteBatch spriteBatch;
        private Texture2D playerTexture;
        private Rectangle TitleSafe;

        public float scale = 1f;

        public PlayerStatus(Game game) : base(game)
        {

        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            playerTexture = Game.Content.Load<Texture2D>("PlayerPic");
            TitleSafe = GetTitleSafeArea(.8f);
        }

        protected Rectangle GetTitleSafeArea(float percent)
        {
            var graphics = GraphicsDevice;

            Rectangle retval = new Rectangle(
                graphics.Viewport.X,
                graphics.Viewport.Y,
                graphics.Viewport.Width,
                graphics.Viewport.Height);

            return retval;
        }

        public override void Update(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // TODO: Add your game logic here.
            
            scale = scale % 2;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            
            spriteBatch.Begin();
            Vector2 pos = new Vector2(TitleSafe.Left, TitleSafe.Top);
            
            spriteBatch.Draw(playerTexture, pos, null, Color.White, 0f, new Vector2(-2400, 0), scale / 2, SpriteEffects.None, 0f);

            spriteBatch.End();
            base.Draw(gameTime);



        }
    }
}
