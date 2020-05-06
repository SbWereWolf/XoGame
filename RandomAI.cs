using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XoGame
{
    class RandomAI : IPlayer
    {
        // duplicated in User and RandomUI since I'm not a fan of superclasses and know no other way yet
        public string name;

        public RandomAI(string name)
        {
            this.name = name;
        }
        // duplicated in User and RandomUI since I'm not a fan of superclasses and know no other way yet

        public Move do_move()
        {
            var rand = new Random();
            return new Move { x = rand.Next(0,2 +1), y = rand.Next(0,2 +1) };
        }

        public string get_name()
        {
            return name;
        }
    }
}
