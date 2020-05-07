namespace XoGame
{
    internal class Painter
    {
        public void Paint(Paint command)
        {
            command?.Redo();
        }

        public void Purge(Paint command)
        {
            command?.Undo();
        }
    }
}
