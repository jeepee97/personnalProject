using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace connect4
{
    class Noeud
    {
        private int winRate;
        private int move;
        private Board board;
        private Noeud parent;

        public Noeud()
        {
            winRate = -1;
            move = -1;
            board = null;
            parent = null;
        }

        public Noeud(Board board_)
        {
            winRate = -1;
            move = -1;
            board = new Board(board_);
            parent = null;
        }

        public Noeud(Noeud parent_)
        {
            parent = parent_;
            board = new Board(parent_.board);
        }

        public void setBoard(int move_, bool playerTurn)
        {
            move = move_;
            int i = 0;
            while (board.getBoardPos(i, move) != 0)
            {
                i++;
            }
            if (playerTurn)
            {
                board.set(i, move, 1);
            }
            else
            {
                board.set(i, move, 2);
            }
        }

        public Board getBoard()
        {
            return board;
        }

        public int getWinRate()
        {
            return winRate;
        }

        public void setVal(int winRate_, int move_)
        {
            winRate = winRate_;
            move = move_;
        }

        public int getMove()
        {
            return move;
        }
    }
}
