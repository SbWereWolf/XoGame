using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XoGame
{
    interface IPlayer
    {
        Move do_move();

        string get_name();
    }
}
