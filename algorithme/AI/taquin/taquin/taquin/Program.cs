using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taquin
{
    class Noeud
    {
        int[,] taquinN;
        Noeud parent;
        // hCost est le cout de l'heuristique calculer,
        public int hCost = 0;
        // alors que gCost est le cout des deplacement jusqu'a maintenant
        public int gCost = 0;
        public int fCost;

        public Noeud(int[,] taquin, Noeud parent_, int gCost_)
        {
            taquinN = taquin;
            parent = parent_;
            hCost = 0;
            gCost = gCost_;
            SetFCost();
        }

        public void SetHCost(int hcost_)
        {
            hCost = hcost_;
            SetFCost();
        }

        void SetFCost()
        {
            fCost = hCost + gCost;
        }

        public int GetFCost()
        {
            return fCost;
        }

        public int[,] GetTaquin()
        {
            return taquinN;
        }

        public int GetGCost()
        {
            return gCost;
        }

        public int GetHCost()
        {
            return hCost;
        }

        public Noeud GetParent()
        {
            return parent;
        }
    }

    class Program
    {
        static int[,] taquin;
        static void Main(string[] args)
        {
            //on creer le taquin aleatoire par lequel nous allons commencer :
            int size = 3;
            taquin = new int[size, size];
            SetTaquin(size);
            int[,] test = { { 6, 0, 3 }, { 7, 1, 4 }, { 5, 8, 2 } };
            Noeud start = new Noeud(test, null, 0);
            AfficherTaquin(test);
            CalculerHCost3(start);
            Console.Read();
            

            //creation de letat final :
            int[,] final = { { 0, 1, 2 },{3, 4, 5},{6, 7, 8} };
            Noeud end = new Noeud(final, null, 0);
            AfficherTaquin(final);

            Console.Read();

            /*
            List<Noeud> test = GetNeighbours(start);
            foreach(Noeud n in test)
            {
                AfficherTaquin(n.GetTaquin());
            }
            */

            //int test = TrouverSolutionPartielle(start, end);

            //trouver la solution
            Noeud noeudSolution = TrouverSolution(start, end);

            //faire une liste qui retrace les chemin
            List<Noeud> solution = new List<Noeud>();
            while(noeudSolution != null)
            {
                solution.Add(noeudSolution);
                noeudSolution = noeudSolution.GetParent();
            }

            //afficher le chemin
            foreach(Noeud n in solution)
            {
                AfficherTaquin(n.GetTaquin());
            }
            
            Console.Read();
        }

        static Noeud TrouverSolution(Noeud start, Noeud end)
        {
            List<Noeud> openSet = new List<Noeud>();
            List<Noeud> closedSet = new List<Noeud>();
            openSet.Add(start);

            while (openSet.Count() > 0)
            {
                // trouver le Noeud avec le plus petit fCost
                Noeud next = openSet[0];
                foreach(Noeud n in openSet)
                {
                    if (n.GetFCost() < next.GetFCost() || (n.GetFCost() == next.GetFCost() && n.GetHCost() < next.GetHCost()))
                    {
                        next = n;
                    }
                }
                Console.WriteLine("GCost : " + next.GetGCost());
                Console.WriteLine("HCost : " + next.GetHCost());
                Console.WriteLine("FCost : " + next.GetFCost());
                AfficherTaquin(next.GetTaquin());
                // trouver ses etats voisins et l'ajouter au openset
                List<Noeud> neighbours = GetNeighbours(next);
                foreach(Noeud n in neighbours)
                {
                    if (VerifierTaquin(n.GetTaquin(), end.GetTaquin()))
                    {
                        Console.WriteLine("solution finale trouvee!");
                        return n;
                    }

                    bool contains = false;
                    foreach(Noeud no in openSet)
                    {
                        if (VerifierTaquin(n.GetTaquin(), no.GetTaquin()))
                        {
                            contains = true;
                        }
                    }
                    bool CSContains = false;
                    foreach (Noeud nc in closedSet)
                    {
                        if (VerifierTaquin(n.GetTaquin(), nc.GetTaquin()))
                        {
                            CSContains = true;
                        }
                    }

                    if (!contains && !CSContains)
                    {
                        openSet.Add(n);
                    }
                }

                // sortir le noeud de openSet et l'ajouter a closedSet
                openSet.Remove(next);
                closedSet.Add(next);
            }
            Console.WriteLine("aucune solution trouvee...");
            return null;
        }

        static bool VerifierTaquin(int[,] t1, int[,] t2)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (t1[i,j] != t2[i, j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        static bool SolutionPartielle(Noeud n, Noeud end)
        {
            bool confirmation = true;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (!(n.GetTaquin()[i,j] == end.GetTaquin()[i,j] || end.GetTaquin()[i,j] == 9))
                    {
                        confirmation = false;
                    }
                }
            }
            return confirmation;
        }

        static Noeud TrouverSolutionPartielle(Noeud start, Noeud end, int x, int y)
        {
            List<Noeud> openSet = new List<Noeud>();
            List<Noeud> closedSet = new List<Noeud>();
            openSet.Add(start);
            while (openSet.Count > 0)
            {
                Noeud next = openSet[0];
                foreach (Noeud n in openSet)
                {
                    if (n.GetFCost() < next.GetFCost() || (n.GetFCost() == next.GetFCost() && n.GetHCost() < next.GetHCost()))
                    {
                        next = n;
                    }
                }
                // trouver ses etats voisins et l'ajouter au openset
                List<Noeud> neighbours = GetNeighboursPartiel(next,x,y);
                foreach (Noeud n in neighbours)
                {
                    if (SolutionPartielle(n, end))
                    {
                        Console.WriteLine("solution trouvee!");
                        openSet = null;
                        closedSet = null;
                        return n;
                    }

                    bool contains = false;
                    foreach (Noeud no in openSet)
                    {
                        if (VerifierTaquin(n.GetTaquin(), no.GetTaquin()))
                        {
                            contains = true;
                        }
                    }
                    bool CSContains = false;
                    foreach (Noeud nc in closedSet)
                    {
                        if (VerifierTaquin(n.GetTaquin(), nc.GetTaquin()))
                        {
                            CSContains = true;
                        }
                    }

                    if (!contains && !CSContains)
                    {
                        openSet.Add(n);
                    }
                }
                openSet.Remove(next);
                closedSet.Add(next);
            }
            Console.WriteLine("Aucune solution trouvee...");
            return null;
        }

        static List<Noeud> GetNeighboursPartiel(Noeud n,int x,int y)
        {
            List<Noeud> neighbours = new List<Noeud>();
            // trouver position du 0
            int posX = 0;
            int posY = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (n.GetTaquin()[i, j] == 0)
                    {
                        posX = j;
                        posY = i;
                    }
                }
            }
            //trouver les etats voisin
            for (int i = -1; i <= 1; i += 2)
            {
                if (posY + i >= 0 && posY + i < 3)
                {
                    int[,] tamponT = new int[3, 3];
                    for (int k = 0; k < 3; k++)
                    {
                        for (int l = 0; l < 3; l++)
                        {
                            tamponT[k, l] = n.GetTaquin()[k, l];
                        }
                    }
                    tamponT[posY, posX] = tamponT[posY + i, posX];
                    tamponT[posY + i, posX] = 0;
                    Noeud nouveau = new Noeud(tamponT, n, n.GetGCost() + 1);
                    CalculerHCost2(nouveau, x, y);
                    neighbours.Add(nouveau);
                }
            }
            for (int j = -1; j <= 1; j += 2)
            {
                if ((posX + j) >= 0 && (posX + j) < 3)
                {
                    int[,] tamponT = new int[3, 3];
                    for (int k = 0; k < 3; k++)
                    {
                        for (int l = 0; l < 3; l++)
                        {
                            tamponT[k, l] = n.GetTaquin()[k, l];
                        }
                    }
                    tamponT[posY, posX] = tamponT[posY, posX + j];
                    tamponT[posY, posX + j] = 0;
                    Noeud nouveau = new Noeud(tamponT, n, n.GetGCost() + 1);
                    CalculerHCost2(nouveau, x, y);
                    neighbours.Add(nouveau);
                }
            }
            return neighbours;
        }

        static void SetTaquin(int size)
        {
            Random rdm = new Random();
            List<int> nombre = new List<int>();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    int tampon = rdm.Next(0, 8);
                    while (nombre.Contains(tampon))
                    {
                        tampon = rdm.Next(0, 9);
                    }
                    nombre.Add(tampon);
                    taquin[i, j] = tampon;
                }
            }
        }

        static void AfficherTaquin(int[,] taquin_)
        {
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine(taquin_[i, 0] + ", " + taquin_[i, 1] + ", " + taquin_[i, 2]);
            }
            Console.WriteLine("");
        }

        static List<Noeud> GetNeighbours(Noeud n)
        {
            List<Noeud> neighbours = new List<Noeud>();
            // trouver position du 0
            int posX = 0;
            int posY = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (n.GetTaquin()[i,j] == 0)
                    {
                        posX = j;
                        posY = i;
                    }
                }
            }
            //trouver les etats voisin
            for (int i = -1; i <= 1; i += 2)
            {
                if (posY + i >= 0 && posY + i < 3)
                {
                    int[,] tamponT = new int[3, 3];
                    for (int k = 0; k < 3; k++)
                    {
                        for (int l = 0; l < 3; l++)
                        {
                            tamponT[k, l] = n.GetTaquin()[k, l];
                        }
                    }
                    tamponT[posY, posX] = tamponT[posY + i, posX];
                    tamponT[posY + i, posX] = 0;
                    Noeud nouveau = new Noeud(tamponT, n, n.GetGCost() + 1);
                    CalculerHCost3(nouveau);
                    neighbours.Add(nouveau);
                }
            }
            for (int j = -1; j <= 1; j += 2)
            {
                if ((posX + j) >= 0 && (posX + j) < 3)
                {
                    int[,] tamponT = new int[3, 3];
                    for (int k = 0; k < 3; k++)
                    {
                        for (int l = 0; l < 3; l++)
                        {
                            tamponT[k, l] = n.GetTaquin()[k, l];
                        }
                    }
                    tamponT[posY, posX] = tamponT[posY, posX + j];
                    tamponT[posY, posX + j] = 0;
                    Noeud nouveau = new Noeud(tamponT, n, n.GetGCost() + 1);
                    CalculerHCost3(nouveau);
                    neighbours.Add(nouveau);
                }
            }
            return neighbours;
        }

        // devrait etre correct
        static void CalculerHCost(Noeud n)
        {
            // prenons comme premiere heuristique le nombre de cases mal placer
            int numero = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (n.GetTaquin()[i, j] != numero)
                    {
                        n.hCost++;
                    }
                    numero++;
                }
            }
            Console.WriteLine("HCost = " + n.hCost);
        }

        // devrait etre correct
        static void CalculerHCost1(Noeud n)
        {
            // trouve la position de chacun des chiffres
            int compteur = 0;
            int numero = 0;
            for (numero = 0; numero < 9; numero++)
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        //une fois la position trouver
                        if (n.GetTaquin()[i, j] == numero)
                        {
                            // compare sa position avec celle du but
                            int numero2 = 0;
                            for (int k = 0; k < 3; k++)
                            {
                                for (int l = 0; l < 3; l++)
                                {
                                    // une fois les 2 position trouvee
                                    if (numero2 == numero)
                                    {
                                        // evalue la distance qui reste a parcourir
                                        int y = i - k;
                                        if (y < 0)
                                        {
                                            y = -y;
                                        }
                                        int x = j - l;
                                        if (x < 0)
                                        {
                                            x = -x;
                                        }
                                        compteur += x + y;
                                    }
                                    numero2++;
                                }
                            }
                        }
                    }
                }
            }
            n.SetHCost(compteur);
        }

        // devrait etre correct
        static void CalculerHCost2(Noeud n, int min, int max)
        {
            // trouve la position de chacun des chiffres
            int compteur = 0;
            int numero = 0;
            for (numero = min; numero <= max; numero++)
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        //une fois la position trouver
                        if (n.GetTaquin()[i, j] == numero)
                        {
                            // compare sa position avec celle du but
                            int numero2 = 0;
                            for (int k = 0; k < 3; k++)
                            {
                                for (int l = 0; l < 3; l++)
                                {
                                    // une fois les 2 position trouvee
                                    if (numero2 == numero)
                                    {
                                        // evalue la distance qui reste a parcourir
                                        int y = i - k;
                                        if (y < 0)
                                        {
                                            y = -y;
                                        }
                                        int x = j - l;
                                        if (x < 0)
                                        {
                                            x = -x;
                                        }
                                       compteur += (x + y);
                                    }
                                    numero2++;
                                }
                            }
                        }
                    }
                }
            }
            n.SetHCost(compteur);
        }

        // devrait etre correct
        static void CalculerHCost3(Noeud n)
        {
            int[,] solutionPartielle = { { 0, 1, 2 }, { 3, 4, 9 }, { 9, 9, 9 } };
            int[,] solutionPartielle2 = { { 9, 9, 9 }, { 9, 9, 5 }, { 6, 7, 8 } };
            Noeud finPartielle = new Noeud(solutionPartielle, null, 0);
            Noeud finPartielle2 = new Noeud(solutionPartielle2, null, 0);
            Noeud tampon = TrouverSolutionPartielle(n, finPartielle, 0, 4);
            int compteur = CalculerHeuristique(tampon, 0, 4);
            tampon = TrouverSolutionPartielle(n, finPartielle2, 5, 8);
            compteur += CalculerHeuristique(tampon, 5, 9);
            n.SetHCost(compteur);
        }

        // devrait etre correct
        static int CalculerHeuristique(Noeud n, int min, int max)
        {
            int compteur = 0;
            while(n.GetParent() != null)
            {
                // trouver position 0
                int posX = 0;
                int posY = 0;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (n.GetTaquin()[i,j] == 0)
                        {
                            posX = j;
                            posY = i;
                        }
                    }
                }
                int posTampon = n.GetParent().GetTaquin()[posY, posX];
                for (int i = min; i <= max; i++)
                {
                    if (posTampon == i)
                    {
                        compteur++;
                    }
                }
                n = n.GetParent();
            }
            return compteur;
        }
    }
}
