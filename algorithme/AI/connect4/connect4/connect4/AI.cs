using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace connect4
{
    class AI
    {


        public AI()
        {

        }

        public int choisirLigne(Board board)
        {
            Noeud start = new Noeud(board);
            return alpha(start, 0).getMove();
        }

        public static Noeud alpha(Noeud noeud, int it)
        {
            int move = 0;
            int meilleur = 0;
            int somme = 0;
            Board board = noeud.getBoard();
            Noeud retour = new Noeud();
            // pour chaque coup possible
            for (int j = 0; j < board.getSize(); j++)
            {
                // verifie sil est valide
                if (board.getBoardPos(6, j) == 0)
                {
                    // trouve la hauteur
                    int i = 0;
                    while (board.getBoardPos(i, j) != 0)
                    {
                        i++;
                    }

                    Noeud next = new Noeud(noeud);
                    next.setBoard(j, false);

                    // si on gagne sur ce tour, le meilleur est forcement 100.
                    if (isGameOver(next.getBoard()))
                    {
                        meilleur = 100;
                        somme += 100;
                        move = j;
                        retour.setVal(meilleur, move);
                        break;
                    }

                    // si on gagne pas, alors le meilleur est le meilleur des pires. sauf dans le cas dune defaite
                    else
                    {
                        if (it < 3)
                        {
                            int valeur = beta(next, it).getWinRate();
                            if (valeur > meilleur)
                            {
                                meilleur = valeur;
                                move = j;
                                retour.setVal(meilleur, move);
                            }
                        }
                        else
                        {
                            int valeur = boardValue2(next.getBoard());
                            if (valeur > meilleur)
                            {
                                meilleur = valeur;
                                move = j;
                                retour.setVal(meilleur, move);
                            }
                        }
                    }
                }
            }
            return retour;
        }

        public static Noeud beta(Noeud noeud, int it)
        {
            it++;
            int somme = 0;
            Board board = noeud.getBoard();
            int pire = 100;
            int move = 0;
            Noeud retour = new Noeud();
            // pour chaque coup possible
            for (int j = 0; j < board.getSize(); j++)
            {
                // verifie sil est valide
                if (board.getBoardPos(6, j) == 0)
                {
                    // trouve la hauteur
                    int i = 0;
                    while (board.getBoardPos(i, j) != 0)
                    {
                        i++;
                    }

                    Noeud next = new Noeud(noeud);
                    next.setBoard(j, true);

                    // si on gagne sur ce tour, le meilleur est forcement 100.
                    if (isGameOver(next.getBoard()))
                    {
                        pire = 0;
                        move = j;
                        retour.setVal(pire, move);
                        break;
                    }

                    // si on gagne pas, alors le meilleur est le meilleur des pires.
                    else
                    {
                        if (it < 3)
                        {
                            int valeur = alpha(next, it).getWinRate();
                            if (valeur < pire)
                            {
                                pire = valeur;
                                move = j;
                                retour.setVal(pire, move);
                            }
                        }
                        else
                        {
                            int valeur = boardValue2(next.getBoard());
                            if (valeur < pire)
                            {
                                pire = valeur;
                                move = j;
                                retour.setVal(pire, move);
                            }
                        }
                    }
                }
            }
            return retour;
        }

        static bool isGameOver(Board board)
        {
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

        static int boardValue(Board board)
        {
            int retour = 50;
            for (int i = 0; i < board.getSize(); i++)
            {
                for (int j = 0; j < board.getSize(); j++)
                {
                    int count = 0;
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
                                    if (k > 1)
                                    {
                                        if (valeur == 1)
                                        {
                                            retour -= 1;
                                        }
                                        else
                                        {
                                            retour += 1;
                                        }
                                    }
                                    break;
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
                                    if (k > 1)
                                    {
                                        if (valeur == 1)
                                        {
                                            retour -= 1;
                                        }
                                        else
                                        {
                                            retour += 1;
                                        }
                                    }
                                    break;
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
                                    if (k > 1)
                                    {
                                        if (valeur == 1)
                                        {
                                            retour -= 1;
                                        }
                                        else
                                        {
                                            retour += 1;
                                        }
                                    }
                                    break;
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
                                    if (k > 1)
                                    {
                                        if (valeur == 1)
                                        {
                                            retour -= 1;
                                        }
                                        else
                                        {
                                            retour += 1;
                                        }
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            return retour;
        }

        static int boardValue2(Board board)
        {
            int retour = 50;
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
                            int count = 0;
                            for (int k = 0; k < 4; k++)
                            {
                                if (valeur == board.getBoardPos(i, j + k))
                                {
                                    count++;
                                }
                            }
                            if (valeur == 1)
                            {
                                retour -= count * count;
                            }
                            else
                            {
                                retour += count * count;
                            }
                        }

                        //vertical
                        if (i <= 3)
                        {
                            int count = 0;
                            for (int k = 0; k < 4; k++)
                            {
                                if (valeur == board.getBoardPos(i + k, j))
                                {
                                    count++;
                                }
                            }
                            if (valeur == 1)
                            {
                                retour -= count * count;
                            }
                            else
                            {
                                retour += count * count;
                            }
                        }

                        // diagonale 1
                        if (j <= 3 && i <= 3)
                        {
                            int count = 0;
                            for (int k = 0; k < 4; k++)
                            {
                                if (valeur != board.getBoardPos(i + k, j + k))
                                {
                                    count++;
                                }
                            }
                            if (valeur == 1)
                            {
                                retour -= count * count;
                            }
                            else
                            {
                                retour += count * count;
                            }
                        }

                        // diagonale 2
                        if (j >= 3 && i <= 3)
                        {
                            int count = 0;
                            for (int k = 0; k < 4; k++)
                            {
                                if (valeur != board.getBoardPos(i + k, j - k))
                                {
                                    count++;
                                }
                            }
                            if (valeur == 1)
                            {
                                retour -= count * count;
                            }
                            else
                            {
                                retour += count * count;
                            }
                        }
                    }
                }
            }
            return retour;
        }
    }
}
