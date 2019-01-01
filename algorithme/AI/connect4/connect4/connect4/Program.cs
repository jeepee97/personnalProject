using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace connect4
{
    class Program
    {
        // 0 = none, 1 = red, 2 = black
        static void Main(string[] args)
        {
            bool playerTurn = true;
            bool gameOver = false;
            Board board = new Board(7);
            AI ai = new AI();

            while (!gameOver)
            {
                switch (playerTurn)
                {
                    case true:

                        int line = choisirLigne(board);
                        board.jouer(line, playerTurn);

                        gameOver = isGameOver(board);
                        playerTurn = false;
                        break;
                    case false:

                        int line2 = ai.choisirLigne(board);
                        board.jouer(line2, playerTurn);

                        gameOver = isGameOver(board);
                        playerTurn = true;
                        break;
                }
            }

            if (!playerTurn)
            {
                Console.WriteLine("Player wins!!!!");
            }
            else
            {
                Console.WriteLine("AI wins!!!!");
            }

            Console.Read();
        }

        static int choisirLigne(Board board)
        {
            int line;
            Console.WriteLine("Choisir une ligne (0, 1, 2, 3, 4, 5, 6)");
            try
            {
                line = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                return choisirLigne(board);
            }

            if (line >= 7 || line < 0 || board.getBoardPos(6, line) != 0)
            {
                Console.WriteLine("ligne invalide...");
                return choisirLigne(board);
            }
    
            Console.WriteLine(line);
            return line;
        }

        static bool isGameOver(Board board)
        {
            // null
            bool tampon = true;
            for (int i = 0; i < board.getSize(); i++)
            {
                tampon = tampon & (board.getBoardPos(6, i) != 0);
                if (tampon)
                {
                    return true; 
                }
            }

            for (int i = 0; i < board.getSize(); i++)
            {
                for (int j = 0; j < board.getSize(); j++)
                {
                    int valeur = board.getBoardPos(i, j);
                    if (valeur != 0)
                    {
                        //horizontal
                        if (j <= 3)
                        {
                            for (int k = 0; k < 4; k++)
                            {
                                if (valeur != board.getBoardPos(i, j + k))
                                {
                                    break;
                                }
                                if (k == 3)
                                {
                                    return true;
                                }
                            }
                        }
                        
                        //vertical
                        if (i <= 3)
                        {
                            for (int k = 0; k < 4; k++)
                            {
                                if (valeur != board.getBoardPos(i + k, j))
                                {
                                    break;
                                }
                                if (k == 3)
                                {
                                    return true;
                                }
                            }
                        }

                        // diagonale 1
                        if (j <= 3 && i <= 3)
                        {
                            for (int k = 0; k < 4; k++)
                            {
                                if (valeur != board.getBoardPos(i + k, j + k))
                                {
                                    break;
                                }
                                if (k == 3)
                                {
                                    return true;
                                }
                            }
                        }

                        // diagonale 2
                        if (j >= 3 && i <= 3)
                        {
                            for (int k = 0; k < 4; k++)
                            {
                                if (valeur != board.getBoardPos(i + k, j - k))
                                {
                                    break;
                                }
                                if (k == 3)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }
    }
}
