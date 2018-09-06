using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp8
{
    class Noeud
    {
        public int valeur;
        public Tuple<int, int> position;
        public int gCost;
        public int hCost;
        public Noeud parent;

        public Noeud(int valeur_, Tuple<int, int> position_)
        {
            valeur = valeur_;
            position = position_;
        }

        public int fCost
        {
            get
            {
                return hCost + gCost;
            }
        }

    }
}
