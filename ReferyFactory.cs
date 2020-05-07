using System;
using System.Collections.Generic;

namespace XoGame
{
    internal class ReferyFactory
    {

        
        public Refery Make(Dictionary<Tuple<int, int>, int> map)
        {
            return new Refery(map);
        } 
    }
}
