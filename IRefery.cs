using System;
using System.Collections.Generic;

namespace XoGame
{
    internal interface IRefery
    {
        bool IsWin(Tuple<int, int> cell);
        bool IsDraw(Dictionary<Tuple<int, int>, int> map);
    }
}
