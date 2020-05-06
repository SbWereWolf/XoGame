using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XoGame
{
    class User : IPlayer
    {

        Move chosenMove;

        // duplicated in User and RandomUI since I'm not a fan of superclasses and know no other way yet
        public string name;

        public User (string name)
        {
            this.name = name;
        }
        // duplicated in User and RandomUI since I'm not a fan of superclasses and know no other way yet

        public string get_name()
        {
            return name;
        }

        public void move_is(Move move)
        {
            chosenMove = move;
        }

        public Move do_move()
        {
            return chosenMove;
        }
    }
}
