namespace XoGame
{
    internal class Game
    {
        private readonly IPlayer _playerX;
        private readonly IPlayer _playerO;
        private IPlayer _nextMoves;
        private readonly Board _board;

        public Game(IPlayer x, IPlayer o, Board givenBoard)
        {
            _board = givenBoard;
            _playerX = x;
            _playerO = o;
            _nextMoves = x;
        }

        public Move Step()
        {
            var move = impossible_move();
            var nowMoves = _nextMoves == _playerX ? _playerX : _playerO;

            if (nowMoves != null)
            {
                move = nowMoves.Do_move();
            }
            if (_board != null && !_board.Make_move(move, nowMoves))
            {
                move = impossible_move();
            }
            else
            {
                _nextMoves = _nextMoves == _playerX ? _playerO : _playerX;
            }

            return move;
        }

        public static Move impossible_move()
        {
            return new Move { X = -111, Y = -111 };
        }

        public bool Finished()
        {
            if (HasWinner())
            {
                return true;
            }
            var board = this._board;
            return board != null && board.no_more_moves();
        }

        public string Winner()
        {
            var result = "";
            if (_board!=null)
            {
                result = _board.Winner();
            }
            return result;
        }

        public bool HasWinner()
        {
            var winner = "";
            if (_board != null)
            {
                winner = _board.Winner();
            }
            return winner != null && !winner.Equals("");
        }

    }
}
