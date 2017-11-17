using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace DungeonMonoGame
{
        public class Enemy
        {
            public bool IsDead { get; set; }
            public int Health { get; set; }
            public int MaxHealth { get; set; }
            public int Attack { get; set; }
            public int HitRate { get; set; }
            public int GoldDrop { get; set; }

        public Enemy(Game game)
            {

                Health = 7;
                MaxHealth = Health;
                Attack = 1;
                HitRate = 60;
                GoldDrop = 10;




            }
        }
    }

