using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace connect4
{
    class Board
    {
        private int size;
        private int[,] board;

        public Board(int size_)
        {
            size = size_;
            board = new int[size, size];
        }

        public Board(Board board_)
        {
            size = board_.size;
            board = new int[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    board[i, j] = board_.board[i, j];
                }
            }
        }

        public int getSize()
        {
            return size;
        }

        public int getBoardPos(int i, int j)
        {
            return board[i, j];
        }

        public void jouer(int line, bool playerTurn)
        {
            int i = 0;
            while (board[i, line] != 0)
            {
                i++;
            }
            if (playerTurn)
            {
                board[i, line] = 1;
                Console.WriteLine("Player : [" + i + "," + line + "]");
            }
            else
            {
                board[i, line] = 2;
                Console.WriteLine("AI : [" + i + "," + line + "]");
            }
            afficherBoard();
        }

        public void set(int i, int j, int val)
        {
            board[i, j] = val;
        }

        public void afficherBoard()
        {
            for (int i = size - 1; i >= 0; i--)
            {
                Console.Write("[ ");
                for (int j = 0; j < size; j++)
                {
                    if (board[i,j] == 0)
                    {
                        Console.Write("-");
                    }
                    else
                    {
                        Console.Write(board[i, j]);
                    }
     
                    if (j < size - 1)
                    {
                        Console.Write(", ");
                    }
                }
                Console.WriteLine(" ]");
            }
        }
    }
}
