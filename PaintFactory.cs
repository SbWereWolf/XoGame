using System.Windows.Controls;

namespace XoGame
{
    class PaintFactory
    {
        public Paint Make(Button[] cells, int tag, string mark)
        {
            return new Paint(cells, tag, mark);
        }
    }
}
