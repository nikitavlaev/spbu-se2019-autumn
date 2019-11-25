using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Task02
{
    class Program
    {
        static void PrintMSTs(int verticesNumber, int[] parent1, int[] parent2, int[,] graph)
        {
            Console.WriteLine("Edge \tWeight \tEdge \tWeight");
            for (int i = 1; i < verticesNumber; i++)
            {
                Console.Write(parent1[i] + " - " + i + "\t" + graph[parent1[i], i] + "\t");
                Console.WriteLine(parent2[i] + " - " + i + "\t" + graph[parent2[i], i]);
            }
        }

        static int CalculateMSTSum(int[] prev, int verticesNumber, int[,] graphMatrix)
        {
            int sum = 0;
            for (int i = 1; i < verticesNumber; i++)
            {
                sum += graphMatrix[prev[i], i];
            }
            return sum;
        }
        static void Main()
        {
            var rnd = new Random();

//            GenerateGraph(AppDomain.CurrentDomain.BaseDirectory + "/1.in", 5 + rnd.Next(2),
//                10 + rnd.Next(2), rnd.Next(100));
//            var graphMatrix = LoadGraph(AppDomain.CurrentDomain.BaseDirectory + "/1.in");
            int vertices = 6000;
            Graph.GenerateGraph(AppDomain.CurrentDomain.BaseDirectory + "/2.in",
                vertices /*+ rnd.Next(5000)*/,
                vertices * (vertices - 1) / 2 /*+ rnd.Next(1000000)*/,
                rnd.Next(100, 100));
            var graphMatrix = Graph.LoadGraph(AppDomain.CurrentDomain.BaseDirectory + "/2.in");
       //     Graph.SaveGraph(AppDomain.CurrentDomain.BaseDirectory + "/2matrix.in", graphMatrix);
            //   Graph.printMatrix(graphMatrix);
            Console.WriteLine();
            Stopwatch timer1 = Stopwatch.StartNew();
            // var a = Kruskal.RunSeqKruskal(graphMatrix);
            // var a = Floyd.RunSeqFloyd(graphMatrix);
            var a = Prim.RunSeqPrim(graphMatrix);
            timer1.Stop();
            Console.WriteLine("Прошло: {0} мс.",
                timer1.ElapsedMilliseconds);

            Stopwatch timer2 = Stopwatch.StartNew();
            // var b = Kruskal.RunParallelKruskal(graphMatrix);
            // var b = Floyd.RunSeqFloyd(graphMatrix);
            var b = Prim.RunParallelPrim(graphMatrix);
            timer2.Stop();
            Console.WriteLine("Прошло: {0} мс.",
                timer2.ElapsedMilliseconds);

            int sum1 = CalculateMSTSum(a, vertices, graphMatrix);
            int sum2 = CalculateMSTSum(b, vertices, graphMatrix);
      
             Console.WriteLine("{0} {1}", sum1, sum2);
             
           //  PrintMSTs(vertices, a, b, graphMatrix);
        }
    }
}