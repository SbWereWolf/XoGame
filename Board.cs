using System.Collections.Generic;

namespace XoGame
{
    internal class Board
    {
        private int _movesLeft;
        
        private readonly Dictionary<Move, string> _moves;

        public Board(int width, int height)
        {
            this._moves = new Dictionary<Move, string>();
            this._movesLeft = width * height;

        }

        public bool Make_move(Move move, IPlayer player)
        {
            var canDo = can_move(move);

            if (canDo && _moves != null && player != null)
            {
                _moves[move] = player.Get_name();
                this._movesLeft = this._movesLeft - 1;
            }
            return canDo;
        }

        private bool can_move(Move move)
        {
            return _moves != null && !_moves.ContainsKey(move);
        }

        public bool no_more_moves()
        {
            return this._movesLeft == 0;
        }

        public string Winner()
        {
            var x0Y0 = new Move{ X = 0, Y = 0 };
            var x1Y0 = new Move{ X = 1, Y = 0 };
            var x2Y0 = new Move{ X = 2, Y = 0 };
            
            var x0Y1 = new Move{ X = 0, Y = 1 };
            var x1Y1 = new Move{ X = 1, Y = 1 };
            var x2Y1 = new Move{ X = 2, Y = 1 };

            var x0Y2 = new Move{ X = 0, Y = 2 };
            var x1Y2 = new Move{ X = 1, Y = 2 };
            var x2Y2 = new Move{ X = 2, Y = 2 };


            var winner = WinnerByRows(x0Y0, x0Y1, x0Y2, out var gotWinner);
            if (gotWinner) {
                return winner;
            }
            winner = WinnerByRows(x1Y0, x1Y1, x1Y2, out gotWinner);
            if (gotWinner)
            {
                return winner;
            }
            winner = WinnerByRows(x2Y0, x2Y1, x2Y2, out gotWinner);
            if (gotWinner)
            {
                return winner;
            }
            

            winner = WinnerByRows(x0Y0, x1Y0, x2Y0, out gotWinner);
            if (gotWinner)
            {
                return winner;
            }
            winner = WinnerByRows(x0Y1, x1Y1, x2Y1, out gotWinner);
            if (gotWinner)
            {
                return winner;
            }
            winner = WinnerByRows(x0Y2, x1Y2, x2Y2, out gotWinner);
            if (gotWinner)
            {
                return winner;
            }
            

            winner = WinnerByRows(x0Y0, x1Y1, x2Y2, out gotWinner);
            if (gotWinner)
            {
                return winner;
            }
            winner = WinnerByRows(x2Y0, x1Y1, x0Y2, out gotWinner);
            if (gotWinner)
            {
                return winner;
            }

            return "";
        }

        private string WinnerByRows(Move a, Move b, Move c, out bool gotWinner)
        {
            if (_moves != null 
                && (_moves.ContainsKey(a) 
                && _moves.ContainsKey(b) 
                && _moves.ContainsKey(c)))
            {
                var aa = _moves[a];
                var bb = _moves[b];
                var cc = _moves[c];

                var aaBb = aa != null && aa.Equals(bb);
                var aaCc = aa != null && aa.Equals(cc);

                if (aaBb && aaCc)
                {
                    gotWinner = true;
                    return _moves[a];
                }
            }
            gotWinner = false;
            return "";
        }

    }
}
