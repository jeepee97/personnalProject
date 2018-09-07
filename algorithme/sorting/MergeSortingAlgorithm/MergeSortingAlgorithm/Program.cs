using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergeSortingAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            // creation d'une liste de n nombre
            int n = 100000;
            List<int> ensemble = creerEnsembleRandom(n);
            int count = ensemble.Count();
            Console.WriteLine(count);

            // afficher(ensemble);
            // Console.WriteLine("");

            ensemble = diviser(ensemble);

            // afficher(ensemble);

            verifier(ensemble);

            Console.Read();
        }

        static List<int> diviser(List<int> ensemble)
        {
            int n = ensemble.Count();
            if (n == 1)
            {
                return ensemble;
            }

            // un ensemble de taille n/2 et lautre de n - n/2
            // car si n = 5, n/2 = 2, et il manquerait un element.
            // dans ce cas, on a e1[2], et e2[5-2] = e2[3]
            List<int> e1 = new List<int>();
            List<int> e2 = new List<int>();

            for (int i = 0; i < n/2; i++)
            {
                e1.Add(ensemble[i]);
            }
            for (int i = n/2; i < n; i++)
            {
                e2.Add(ensemble[i]);
            }

            e1 = diviser(e1);
            e2 = diviser(e2);

            return pourMieuxRegner(e1, e2);
        }

        static List<int> pourMieuxRegner(List<int> e1, List<int> e2)
        {
            List<int> e3 = new List<int>();

            while (e1.Count != 0 && e2.Count != 0)
            {
                if (e1[0] < e2[0])
                {
                    e3.Add(e1[0]);
                    e1.RemoveAt(0);
                }
                else
                {
                    e3.Add(e2[0]);
                    e2.RemoveAt(0);
                }
            }
            while(e1.Count != 0)
            {
                e3.Add(e1[0]);
                e1.RemoveAt(0);
            }
            while(e2.Count != 0)
            {
                e3.Add(e2[0]);
                e2.RemoveAt(0);
            }
            return e3;
        }

        static void afficher(List<int> ensemble)
        {
            int count = ensemble.Count();
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < ensemble[i]; j++)
                {
                    Console.Write("1");
                }
                Console.WriteLine("");
            }
        }

        static List<int> creerEnsembleRandom(int n)
        {
            List<int> ensemble = new List<int>();
            Random rnd = new Random();
            for (int i = 0; i < n; i++)
            {
                ensemble.Add(rnd.Next(n));
            }
            return ensemble;
        }

        static void verifier(List<int> ensemble)
        {
            bool isSorted = true;
            for (int i = 0; i < ensemble.Count - 1; i++)
            {
                if (ensemble[i] > ensemble[i + 1])
                {
                    isSorted = false;
                }
            }
            if (isSorted)
            {
                Console.WriteLine("the list is sorted");
            }
            else
            {
                Console.Write("there was an error, the list is not sorted");
            }
        }
    }
}
