using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace DungeonMonoGame
{
    public class Boss
    {
        public bool IsDead { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int Attack { get; set; }
        public int HitRate { get; set; }
        public int GoldDrop { get; set; }
        public int Exp { get; set; }

        public Boss(Game game)
        {

            Health = 1000;                  
            MaxHealth = Health;
            Attack = 30;
            HitRate = 100;
            GoldDrop = 10000;
            Exp = 1000;




        }
    }
}
