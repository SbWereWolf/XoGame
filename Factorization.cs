using System;

namespace XoGame
{
    class Factorization : IFactorization
    {
        private readonly int _positionBase;

        public Factorization(int positionBase)
        {
            _positionBase = positionBase;
        }
        public int Encode(Tuple<int, int> x)
        {
            if (x == null)
            {
                throw new Exception("x is null, it is invalid value.");
            }

            return x.Item1 + x.Item2 * _positionBase;
        }

        public Tuple<int, int> Decode(int value)
        {
            var x = value % _positionBase;
            var y = value / _positionBase;

            return new Tuple<int, int>(x, y);
        }
    }
}
