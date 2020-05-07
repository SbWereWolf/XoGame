using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace XoGame
{
    internal class Board
    {
        private readonly int _playeX;
        private readonly int _player0;
        private readonly ReferyFactory _factory;
        [NotNull] private readonly Dictionary<Tuple<int, int>, int> _mapX;
        [NotNull] private readonly Dictionary<Tuple<int, int>, int> _map0;

        public Board(ReferyFactory factory)
        {
            _playeX = 10; // roman ten is "X"
            _player0 = 0;
            _factory = factory;

            _mapX = new Dictionary<Tuple<int, int>, int>();
            _map0 = new Dictionary<Tuple<int, int>, int>();
        }

        public bool MarkX(Tuple<int, int> cell)
        {
            var may = IsEmpty(cell);
            var result = false;
            if (may)
            {
                Mount(cell, _mapX, _playeX);
                result = true;
            }

            return result;
        }
        public bool Mark0(Tuple<int, int> cell)
        {
            var may = IsEmpty(cell);
            var result = false;
            if (may)
            {
                Mount(cell, _map0, _player0);
                result = true;
            }

            return result;
        }

        public bool Purge(Tuple<int, int> cell)
        {
            var isContains = cell != null && (_mapX.ContainsKey(cell));
            var wasRemove = false;
            if (isContains)
            {
                _mapX.Remove(cell);
                wasRemove = true;
            }
            if (!wasRemove)
            {
                if (cell != null) isContains = _map0.ContainsKey(cell);
            }
            if (!wasRemove && isContains)
            {
                _map0.Remove(cell);
                wasRemove = true;
            }

            return wasRemove;
        }

        public bool IsFatality(Tuple<int, int> cell)
        {
            var checkX = cell != null && _mapX.ContainsKey(cell);
            IRefery refery = null;
            if (checkX && _factory != null)
            {
                refery = _factory.Make(_mapX);
            }
            if (!checkX && _factory != null && cell != null)
            {
                refery = _factory.Make(_map0);
            }

            var result = refery != null && refery.IsWin(cell);

            return result;
        }

        public bool IsDraw()
        {
            var result = false;
            if (_factory != null)
            {
                var refery = _factory.Make(_mapX);
                result = refery.IsDraw(_map0);
            }

            return result;
        }

        private static void Mount(Tuple<int, int> key, IDictionary<Tuple<int, int>, int> map, int mark)
        {
            if (key != null)
            {
                map?.Add(key, mark);
            }

        }

        private bool IsEmpty(Tuple<int, int> key)
        {
            var has = false;
            if (key != null)
            {
                has = (_mapX.ContainsKey(key) || _map0.ContainsKey(key));
            }

            return !has;
        }
    }
}
