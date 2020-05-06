using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XoGame
{
    class Game
    {
        private IPlayer playerX, playerO, nextMoves;
        private Board board;

        public Game(IPlayer x, IPlayer o, Board given_board)
        {
            board = given_board;
            playerX = x;
            playerO = o;
            nextMoves = x;
        }

        public Move step()
        {
            IPlayer nowMoves;

            if (nextMoves == playerX) {
                nowMoves = playerX;
                //nextMoves = playerO;
            } else {
                nowMoves = playerO;
                //nextMoves = playerX;
            }

            var move = nowMoves.do_move();
            if (!board.make_move(move, nowMoves))
            {
                move = Game.impossible_move();
            }
            else
            {
                if (nextMoves == playerX)
                {
                    nextMoves = playerO;
                }
                else
                {
                    nextMoves = playerX;
                }
            }

            return move;
            // TODO
        }

        public static Move impossible_move()
        {
            return new Move { x = -111, y = -111 };
        }

        public bool finished()
        {
            if (hasWinner())
            {
                return true;
            }
            return this.board.no_more_moves();
        }

        public string winner()
        {
            return board.winner();
        }

        public bool hasWinner()
        {
            return !board.winner().Equals("");
        }

    }
}
