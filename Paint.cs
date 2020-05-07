using System.Windows.Controls;
using JetBrains.Annotations;

namespace XoGame
{
    class Paint
    {
        private readonly int _tag;
        [NotNull] private readonly Button[] _canvas;
        [NotNull] private readonly string _mark;

        public Paint(Button[] cells, int tag, string mark)
        {
            _canvas = new Button[] { };
            if (cells != null)
            {
                _canvas = cells;
            }

            _mark = string.Empty;
            if (mark != null)
            {
                _mark = mark;
            }

            _tag = tag;
        }

        private Button Find()
        {
            Button result = null;
            foreach (var button in _canvas)
            {
                var key = 0;
                var isSuccess = false;
                if (button?.Tag != null)
                {
                    key = (int)button.Tag;
                    isSuccess = true;
                }
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
