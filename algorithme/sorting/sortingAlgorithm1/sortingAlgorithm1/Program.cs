using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sortingAlgorithm1
{
    class Program
    {
        static void Main(string[] args)
        {
            //soit un ensemble de int
            List<int> ensemble = creerList();
            bool terminer = false;

            while (!terminer)
            {
                bool switching = false;
                for (int i = 0; i < ensemble.Count - 1; i++)
                {
                    if (ensemble[i] > ensemble[i + 1])
                    {
                        int tampon = ensemble[i];
                        ensemble[i] = ensemble[i + 1];
                        ensemble[i + 1] = tampon;
                        switching = true;
                    }
                }
                if (!switching)
                {
                    terminer = true;
                }
            }

            Console.Write("terminer");
            Console.Read();
        }

        static void afficherList(List<int> ensemble)
        {
            for (int i = 0; i < ensemble.Count; i++)
            {
                for (int j = 0; j < ensemble[i]; j++)
                {
                    Console.Write("x");
                }
                Console.WriteLine("");
            }
        }

        static List<int> creerList()
        {
            List<int> ensemble = new List<int>();
            for (int i = 0; i < 100000; i++)
            {
                ensemble.Add(100000 - i);
            }
            return ensemble;
        }
    }
}
