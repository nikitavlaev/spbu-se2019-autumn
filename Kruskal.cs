using System;

namespace Task02
{
    public class Kruskal
    {
// Find set of vertex i 
        static int FindSet(int[] parent, int i)
        {
            while (parent[i] != i)
                i = parent[i];
            return i;
        }

// Finds MST using Kruskal's algorithm 
        public static int RunSeqKruskal(int[,] graphMatrix)
        {
            int verticesNumber = graphMatrix.GetLength(0);
            int[] parent = new int[verticesNumber];

            int mincost = 0; // Cost of min MST. 

            // Initialize sets of disjoint sets. 
            for (int i = 0; i < verticesNumber; i++)
                parent[i] = i;

            // Include minimum weight edges one by one 
            int edge_count = 0;
            while (edge_count < verticesNumber - 1)
            {
                int min = Int32.MaxValue, a = -1, b = -1;
                for (int i = 0; i < verticesNumber; i++)
                {
                    for (int j = i + 1; j < verticesNumber; j++)
                    {
                        if ((FindSet(parent, i) != FindSet(parent, j)) && graphMatrix[i, j] < min)
                        {
                            min = graphMatrix[i, j];
                            a = i;
                            b = j;
                        }
                    }
                }

                int a1 = FindSet(parent, a);
                int b1 = FindSet(parent, b);
                parent[a1] = b1;

                /* Console.Write("Edge {0}:({1}, {2}) graphMatrix:{3} \n",
                     edge_count++, a, b, min);*/
                edge_count++;
                mincost += min;
            }

            Console.Write("\n Minimum cost= {0} \n", mincost);
            return mincost;
        }

     
        public static int RunParallelKruskal(int[,] graphMatrix)
        {
            int verticesNumber = graphMatrix.GetLength(0);
            int[] parent = new int[verticesNumber];

            int mincost = 0; // Cost of min MST. 

            // Initialize sets of disjoint sets. 
            for (int i = 0; i < verticesNumber; i++)
                parent[i] = i;

            // Include minimum weight edges one by one 
            int edge_count = 0;
            int chunkNum = Environment.ProcessorCount;
            int chunkSize = verticesNumber / chunkNum;
            
            while (edge_count < verticesNumber - 1)
            {
                
                int min = Int32.MaxValue, a = -1, b = -1;
                for (int t = 0; t < chunkNum; t++)
                {
                    //int min = Int32.MaxValue, a = -1, b = -1;
                    for (int i = 0; i < verticesNumber; i++)
                    {
                        for (int j = i + 1; j < verticesNumber; j++)
                        {
                            if ((FindSet(parent, i) != FindSet(parent, j)) && graphMatrix[i, j] < min)
                            {
                                min = graphMatrix[i, j];
                                a = i;
                                b = j;
                            }
                        }
                    }
                }

                int a1 = FindSet(parent, a);
                int b1 = FindSet(parent, b);
                parent[a1] = b1;

                /* Console.Write("Edge {0}:({1}, {2}) graphMatrix:{3} \n",
                     edge_count++, a, b, min);*/
                edge_count++;
                mincost += min;
            }

            Console.Write("\n Minimum cost= {0} \n", mincost);
            return mincost;
        }
    }
}