using System;

namespace Task02
{
    public class Graph
    {
        public static void SaveGraph(string path, int[,] graphMatrix)
        {
            int verticesNumber = graphMatrix.GetLength(0);
            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(path))
            {
                for (int i = 0; i < verticesNumber; i++)
                {
                    for (int j = 0; j < verticesNumber; j++)
                    {
                        if (graphMatrix[i, j] >= 0)
                        {
                            file.Write("{0} ", graphMatrix[i, j]);
                        }
                        else
                        {
                            file.Write("{0} ", Int32.MaxValue);
                        }
                    }

                    file.WriteLine();
                }
            }
        }

        public static void GenerateGraph(string path, int verticesNumber,
            int edgesNumber, int valueRange)
        {
            var rand = new Random();
            int[,] graphMatrix = new int[verticesNumber, verticesNumber];
            for (int i = 0; i < verticesNumber; i++)
            {
                for (int j = 0; j < verticesNumber; j++)
                {
                    graphMatrix[i, j] = -1;
                }
            }

            int edgesCount = 0;
            //I am interpreting value as non-negative number, because it does not violate 
            //task statement

            while (edgesCount < edgesNumber)
            {
                int i, j;
                do
                {
                    i = rand.Next(verticesNumber - 1);
                    j = rand.Next(i + 1, verticesNumber);
                    // Console.WriteLine("search");
                } while (graphMatrix[i, j] != -1);

                //       Console.WriteLine("found {0} {1}", i,j);
                int value = rand.Next(1, valueRange);
                graphMatrix[i, j] = value;
                graphMatrix[j, i] = value;
                edgesCount++;
            }

            for (int i = 0; i < verticesNumber; i++)
            {
                graphMatrix[i, i] = Int32.MaxValue;
            }

            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(path))
            {
                file.WriteLine(verticesNumber.ToString());
                for (int i = 0; i < verticesNumber; i++)
                {
                    for (int j = 0; j < verticesNumber; j++)
                    {
                        if (graphMatrix[i, j] >= 0)
                        {
                            file.WriteLine(i.ToString() + ' ' + j.ToString() + ' ' + graphMatrix[i, j].ToString());
                        }
                    }
                }
            }
        }

        public static int[,] LoadGraph(string path)
        {
            using (System.IO.StreamReader file =
                new System.IO.StreamReader(path))
            {
                int verticesNumber = int.Parse(file.ReadLine());
                int[,] graphMatrix = new int[verticesNumber, verticesNumber];
                for (int i = 0; i < verticesNumber; i++)
                {
                    for (int j = 0; j < verticesNumber; j++)
                    {
                        graphMatrix[i, j] = -1;
                    }
                }

                string dataLine;

                while ((dataLine = file.ReadLine()) != null)
                {
                    string[] inputArray = dataLine.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);
                    int i = int.Parse(inputArray[0]);
                    int j = int.Parse(inputArray[1]);
                    int value = int.Parse(inputArray[2]);
                    graphMatrix[i, j] = value;
                }

                return graphMatrix;
            }
        }

        public static void printMatrix(int[,] matrix)
        {
            int r = matrix.GetLength(0);
            int c = matrix.GetLength(1);
            for (int i = 0; i < r; i++)
            {
                for (int j = 0; j < c; j++)
                {
                    Console.Write($"{matrix[i, j].ToString() + ' ',12}");
                }

                Console.WriteLine();
            }
        }
    }
}