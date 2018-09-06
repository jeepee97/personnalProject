using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp8
{
    class Program
    {
        public struct Player
        {
            public Tuple<int, int> position;
        }

        static void Main(string[] args)
        {
            //soit une map en 2D
            // 0 = rien, 1 = player, 2 = goal, 3 = unwalkable, 4 = path
            int mapSize = 5;
            int[ , ] imageMap = new int[ , ] { { 0, 0, 0, 0, 2 },
                                               { 0, 3, 3, 3, 0 },
                                               { 0, 0, 0, 3, 0 },
                                               { 0, 3, 0, 3, 0 },
                                               { 1, 3, 0, 0, 0 } };
            Noeud[,] map = new Noeud[5,5];
            for(int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    map[i, j] = new Noeud(imageMap[i, j], new Tuple<int, int>(i, j));
                }
            }

            //le debut transformer en Noeud
            Tuple<int, int> pPosition = getPosition(1, map, mapSize);
            Noeud start = map[pPosition.Item2, pPosition.Item1];
            //la fin transformer en Noeud
            Tuple<int, int> gPosition = getPosition(2, map, mapSize);
            Noeud end = map[gPosition.Item2, gPosition.Item1];
            Player player;
            player.position = pPosition;

            Console.WriteLine("player = (" + player.position.Item2 + ", " + player.position.Item1 + ")");
            Console.WriteLine("goal = (" + gPosition.Item2 + ", " + gPosition.Item1 + ")");

            //afficher la map avant de voir le resultat
            afficherMap(map, mapSize);

            pathFinding(start, end, map);

            //afficher le resultat avec le path
            Console.WriteLine("");
            afficherMap(map, mapSize);

            Console.Read();
        }

        //obtient la position d'un point dans une map donnees en supposant quon connait la taille
        public static Tuple<int, int> getPosition(int n, Noeud[,] map, int mapSize)
        {
            Tuple<int, int> position;
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    if (map[i, j].valeur == n)
                    {
                        position = map[i,j].position;
                        return position;
                    }
                }
            }
            position = new Tuple<int, int>(-1, -1);
            Console.WriteLine("error Noeud not found...");
            return position;
        }

        //obtient les neighbours d'un point dans une map
        public static List<Noeud> getNeighbours(Noeud point, Noeud[ , ] map)
        {
            //l'ensemble des neighbours (maximum 4)
            List<Noeud> neighbours = new List<Noeud>();
            for (int i = -1; i <= 1; i++)
            {
                if (i != 0)
                {
                    int checkX = point.position.Item2 + i;
                    //si le point est dans le cadre de la map
                    if (checkX >= 0 && checkX < 5)
                    {
                        neighbours.Add(map[point.position.Item1, checkX]);
                    }
                }
            }
            for (int j = -1; j <= 1; j++)
            {
                if (j != 0)
                {
                    int checkY = point.position.Item1 + j;
                    //si le point est dans le cadre de la map
                    if (checkY >= 0 && checkY < 5)
                    {
                        neighbours.Add(map[checkY, point.position.Item2]);
                    }
                }
            }
            return neighbours;
        }

        //obtient la distance entre un point et la fin (impossible de se deplacer en diagonale)
        public static int getDistance(Noeud currentPoint, Noeud end)
        {
            int x = Math.Abs(currentPoint.position.Item2 - end.position.Item2);
            int y = Math.Abs(currentPoint.position.Item1 - end.position.Item1);
            return x + y;
        }

        //une fois le path terminer, fait le trajet inverse pour avoir le chemin final
        public static List<Noeud> retracePath(Noeud start, Noeud end)
        {
            List<Noeud> path = new List<Noeud>();
            Noeud currentPoint = end;
            while (currentPoint != start)
            {
                //remonte le fil des parents de chaque noeud en changeant la valeur pour voir le resultat
                currentPoint.valeur = 4;
                path.Add(currentPoint);
                currentPoint = currentPoint.parent;
            }
            return path;
        }

        public static void pathFinding(Noeud start, Noeud end, Noeud[,] map)
        {
            bool pathSuccess = false;
            //list des point a verifier
            List<Noeud> openSet = new List<Noeud>();
            //list des point deja verifier
            List<Noeud> closedSet = new List<Noeud>();
            //chemin final
            List<Noeud> path = new List<Noeud>();

            start.hCost = getDistance(start, end);
            start.gCost = 0;

            openSet.Add(start);

            while(openSet.Count > 0)
            {
                Noeud currentPoint = openSet[0];

                //trouve le point avec le plus petit fCost
                for (int i = 1; i < openSet.Count; i++)
                {
                    if ((openSet[i].fCost < currentPoint.fCost) || (openSet[i].fCost == currentPoint.fCost && openSet[i].hCost < currentPoint.hCost))
                    {
                        currentPoint = openSet[i];
                    }
                }
                //enleve le point de openSet et lajoute a closedSet
                openSet.Remove(currentPoint);
                closedSet.Add(currentPoint);

                if (currentPoint.position == end.position)
                {
                    pathSuccess = true;
                    break;
                }

                foreach(Noeud Neighbour in getNeighbours(currentPoint, map))
                {
                     //si le point est valide et pas deja evaluer
                    if (Neighbour.valeur == 3 || closedSet.Contains(Neighbour))
                    {
                        continue;
                    }

                    int nouveauGCost = currentPoint.gCost + 1;
                    //si le point n'est pas deja dans openSet ou que le chemin reduit le gCost d'un point existant dans openSet
                    if (!openSet.Contains(Neighbour) || nouveauGCost < Neighbour.gCost)
                    {
                        Neighbour.gCost = nouveauGCost;
                        Neighbour.hCost = getDistance(Neighbour, end);
                        Neighbour.parent = currentPoint;
                        if (!openSet.Contains(Neighbour))
                        {
                            //ajout les neighbours aux point a verifier
                            openSet.Add(Neighbour);
                        }
                    }
                }
            }
            if (pathSuccess)
            {
                path = retracePath(start, end);
            }
        }

        public static void afficherMap(Noeud[,] map, int mapSize)
        {
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    Console.Write(map[i, j].valeur + " ");
                }
                Console.WriteLine("");
            }
        }

        public static void afficherNeighbours(List<Noeud> neighbours)
        {
            foreach (Noeud N in neighbours)
            {
                Console.WriteLine("(" + N.position.Item2 + " ," + N.position.Item1 + ")");
            }
        }
    }
}
