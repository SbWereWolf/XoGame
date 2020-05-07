using System;
using System.Globalization;
using System.Windows.Controls;
using JetBrains.Annotations;

namespace XoGame
{
    class Paint
    {
        private readonly int _tag = -1;
        [NotNull] private readonly Button[] _canvas;
        [NotNull] private readonly string _mark;

        public Paint(Button[] cells, Tuple<int, int> cell, string mark)
        {
            _canvas = new Button[]{};
            if (cells != null)
            {
                _canvas = cells;
            }

            _mark = string.Empty;
            if (mark != null)
            {
                _mark = mark;
            }

            if (cell != null)
            {
                _tag = cell.Item1 + cell.Item2 * 10;
            }
        }

        private Button Find()
        {
            Button result = null;
            foreach (var button in _canvas)
            {
                var key = -1;
                var isSuccess = button?.Tag != null && (int.TryParse(button.Tag.ToString(), NumberStyles.Integer, CultureInfo.InvariantCulture, out key));
                if (isSuccess && key == _tag)
                {
                    result = button;
                    break;
                }
            }

            return result;
        }

        public bool Redo()
        {
            var result = false;
            var button = Find();
            if (button != null)
            {
                button.Content = _mark;
                result = true;
            }

            return result;
        }

        public bool Undo()
        {
            var result = false;
            var button = Find();
            if (button != null)
            {
                button.Content = string.Empty;
                result = true;
            }

            return result;
        }
    }
}
