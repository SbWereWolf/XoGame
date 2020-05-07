namespace XoGame
{
    internal class PlayerOne
    {
        private readonly string _mark;
        public PlayerOne(string marker)
        {
            this._mark = marker;
        }

        public string GetTitle()
        {
            return _mark;
        }
    }
}
