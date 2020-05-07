using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace XoGame
{
    internal class Refery
    {
        [NotNull] private readonly Dictionary<Tuple<int, int>, int> _map;
        [NotNull] private readonly Tuple<int, int>[] _vectors;

        public Refery(Dictionary<Tuple<int, int>, int> map)
        {
            _map = new Dictionary<Tuple<int, int>, int>();
            if (map != null)
            {
                _map = map;
            }

            _vectors = new[]
            {
                new Tuple<int, int>(0, 1),
                new Tuple<int, int>(1, 1),
                new Tuple<int, int>(1, 0),
                new Tuple<int, int>(1, -1),
            };
        }

        public bool IsWin(Tuple<int, int> cell)
        {
            var result = false;
            foreach (var vector in _vectors)
            {
                var forward = Forward(cell, vector);
                var isContainsForward = _map.ContainsKey(forward);

                var isContainsForwardX2 = false;
                if (isContainsForward)
                {
                    var forwardX2 = Forward(forward, vector);
                    isContainsForwardX2 = _map.ContainsKey(forwardX2);
                }
                if (isContainsForwardX2)
                {
                    result = true;
                    break;
                }

                var backward = Backward(cell, vector);
                var isContainsBackward = _map.ContainsKey(backward);
                if (isContainsForward && isContainsBackward)
                {
                    result = true;
                    break;
                }
                var isContainsBackwardX2 = false;
                if (isContainsBackward)
                {
                    var backwardX2 = Backward(backward, vector);
                    isContainsBackwardX2 = _map.ContainsKey(backwardX2);
                }
                if (isContainsBackwardX2)
                {
                    result = true;
                    break;
                }
            }


            return result;
        }

        [NotNull]
        private static Tuple<int, int> Forward(Tuple<int, int> cell, Tuple<int, int> vector)
        {
            var result = new Tuple<int, int>(int.MinValue, int.MinValue);
            if (cell != null && vector != null)
            {
                var x = cell.Item1 + vector.Item1;
                var y = cell.Item2 + vector.Item2;
                result = new Tuple<int, int>(x, y);
            }

            return result;
        }

        [NotNull]
        private static Tuple<int, int> Backward(Tuple<int, int> cell, Tuple<int, int> vector)
        {
            var result = new Tuple<int, int>(int.MinValue, int.MinValue);
            if (cell != null && vector != null)
            {
                var x = cell.Item1 - vector.Item1;
                var y = cell.Item2 - vector.Item2;
                result = new Tuple<int, int>(x, y);
            }

            return result;
        }

        public bool IsDraw(Dictionary<Tuple<int, int>, int> map)
        {
            var result = false;
            if (map != null)
            {
                var occupied = map.Count + _map.Count;
                result = occupied == 9;

            }
            return result;
        }
    }
}
