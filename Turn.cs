using System;

namespace XoGame
{
    internal class Turn
    {
        private readonly Tuple<int, int> _cell;
        private readonly Board _board;

        public Turn(Board board, Tuple<int, int> cell)
        {
            _board = board;
            _cell = cell;
        }
        public bool RedoX()
        {
            return _board != null && _board.MarkX(_cell);
        }
        public bool Redo0()
        {
            return _board != null && _board.Mark0(_cell);
        }

        public bool Undo()
        {
            return _board != null && _board.Purge(_cell);
        }

        public bool IsWin()
        {
            return _board != null && _board.IsFatality(_cell);

        }

        public bool IsDraw()
        {
            return _board != null && _board.IsDraw();
        }
    }
}
