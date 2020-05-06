using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Collections;

namespace XoGame
{
    class Board
    {
        int width;
        int height;
        int moves_left;

        //Hashtable moves;
        Dictionary<Move, string> moves;
            /*dictionary =
            new Dictionary<string, int>();*/

        public Board(int width, int height)
        {
            this.width = width;
            this.height = height;
            this.moves = new Dictionary<Move, string>(); //Hashtable();
            this.moves_left = width * height;

        }

        public bool make_move(Move move, IPlayer player)
        {
            bool can_do = can_move(move);

            if (can_do)
            {
                moves[move] = player.get_name();
                this.moves_left = this.moves_left - 1;
                // update hash
            }
/*            else
            {
            }*/
            return can_do;
        }

        public bool can_move(Move move)
        {
            return !moves.ContainsKey(move);
        }

        public bool no_more_moves()
        {
            return this.moves_left == 0;
        }

        public string winner()
        {
            var x0y0 = new Move{ x = 0, y = 0 };
            var x1y0 = new Move{ x = 1, y = 0 };
            var x2y0 = new Move{ x = 2, y = 0 };
            
            var x0y1 = new Move{ x = 0, y = 1 };
            var x1y1 = new Move{ x = 1, y = 1 };
            var x2y1 = new Move{ x = 2, y = 1 };

            var x0y2 = new Move{ x = 0, y = 2 };
            var x1y2 = new Move{ x = 1, y = 2 };
            var x2y2 = new Move{ x = 2, y = 2 };


            string winner = "";
            bool gotWinner = false;

            // клонки:

            winner = WinnerByRows(x0y0, x0y1, x0y2, out gotWinner);
            if (gotWinner) {
                return winner;
            }
            winner = WinnerByRows(x1y0, x1y1, x1y2, out gotWinner);
            if (gotWinner)
            {
                return winner;
            }
            winner = WinnerByRows(x2y0, x2y1, x2y2, out gotWinner);
            if (gotWinner)
            {
                return winner;
            }

            // ряды:

            winner = WinnerByRows(x0y0, x1y0, x2y0, out gotWinner);
            if (gotWinner)
            {
                return winner;
            }
            winner = WinnerByRows(x0y1, x1y1, x2y1, out gotWinner);
            if (gotWinner)
            {
                return winner;
            }
            winner = WinnerByRows(x0y2, x1y2, x2y2, out gotWinner);
            if (gotWinner)
            {
                return winner;
            }

            // диагонали

            winner = WinnerByRows(x0y0, x1y1, x2y2, out gotWinner);
            if (gotWinner)
            {
                return winner;
            }
            winner = WinnerByRows(x2y0, x1y1, x0y2, out gotWinner);
            if (gotWinner)
            {
                return winner;
            }

            return ""; // mo winner...
        }

        private string WinnerByRows(Move a, Move b, Move c, out bool gotWinner)
        {
            if (moves.ContainsKey(a) && moves.ContainsKey(b) && moves.ContainsKey(c))
            {
                var aa = moves[a];
                var bb = moves[b];
                var cc = moves[c];

                var aa_bb = aa.Equals(bb);
                var aa_cc = aa.Equals(cc);

                if (aa_bb && aa_cc)
                {
                    gotWinner = true;
                    return moves[a];
                }
            }
            gotWinner = false;
            return "";
        }

    }
}


/*
 * using System;
using System.Collections;

class Program
{
    static void Main()
    {
	Hashtable hashtable = new Hashtable();
	hashtable[1] = "One";
	hashtable[2] = "Two";
	hashtable[13] = "Thirteen";
*/