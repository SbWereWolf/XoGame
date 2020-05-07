using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace XoGame
{
    internal class ReferyFactory
    {


        [NotNull]
        public IRefery Make(Dictionary<Tuple<int, int>, int> map)
        {
            return new Refery(map);
        }
    }
}
