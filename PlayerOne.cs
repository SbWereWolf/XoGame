namespace XoGame
{
    internal class PlayerOne
    {
        public readonly string Mark;
        public PlayerOne(string marker)
        {
            this.Mark = marker;
        }

        public string GetTitle()
        {
            return Mark;
        }

    }
}
