namespace XoGame
{
    internal class User : IPlayer
    {
        private Move _chosenMove;

        private readonly string _name;

        public User (string name)
        {
            this._name = name;
        }

        public string Get_name()
        {
            return _name;
        }

        public void move_is(Move move)
        {
            _chosenMove = move;
        }

        public Move Do_move()
        {
            return _chosenMove;
        }
    }
}
