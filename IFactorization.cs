using System;

namespace XoGame
{
    internal interface IFactorization
    {
        int Encode(Tuple<int, int> x);
        Tuple<int, int> Decode(int value);
    }
}
