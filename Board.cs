using System;
using System.Collections.Generic;

namespace XoGame
{
    internal class Board
    {
        private readonly int _playeX;
        private readonly int _player0;
        private readonly ReferyFactory _factory;
        private readonly Dictionary<Tuple<int, int>, int> _mapX;
        private readonly Dictionary<Tuple<int, int>, int> _map0;

        public Board(ReferyFactory factory)
        {
            _playeX = 10; // X
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
            var isContains = cell != null && (_mapX != null && _mapX.ContainsKey(cell));
            var wasRemove = false;
            if (isContains)
            {
                _mapX.Remove(cell);
                wasRemove = true;
            }
            if (!wasRemove)
            {
                if (_map0 != null) if (cell != null) isContains = _map0.ContainsKey(cell);
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
            var checkX = cell != null && _mapX != null && _mapX.ContainsKey(cell);
            Refery refery = null;
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
                has = _map0 != null && _mapX != null && (_mapX.ContainsKey(key) || _map0.ContainsKey(key));
            }

            return !has;
        }
    }
}
