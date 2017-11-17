using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonMonoGame
{
    public class Player
    {
        public bool IsDead { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int Attack { get; set; }
        public int HitRate { get; set; }
        public int Gold { get; set; }

        /* private Texture2D playerTexture;
         private Texture2D healthTexture; */

        public Player(Game game)
        {

            Health = 10;
            MaxHealth = Health;
            Attack = 1;
            HitRate = 50;
            Gold = 0;



        }
    }
}
