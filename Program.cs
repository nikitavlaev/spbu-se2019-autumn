using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading;
using System.Threading.Tasks;

namespace Task03
{
    class Program
    {
        public static int readCount = 0;

        public static Mutex mutexIn = new Mutex(initiallyOwned: false);
        public static Mutex mutexRead = new Mutex(initiallyOwned: false);
        public static SemaphoreSlim semW = new SemaphoreSlim(1, 1);

        static async Task Main(string[] args)
        {
            Console.WriteLine("Start");
            Data data = new Data();
            Task[] producers = new Task[Setup.producersNum];
            Task[] consumers = new Task[Setup.consumersNum];

            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;

            for (int i = 0; i < Setup.producersNum; i++)
            {
                producers[i] = Task.Run(() => new Producer(token), token);
            }

            for (int i = 0; i < Setup.consumersNum; i++)
            {
                consumers[i] = Task.Run(() => new Consumer(token), token);
            }

            Console.ReadKey();
            Console.WriteLine("Cancelling");
            tokenSource.Cancel();
            try
            {
                await Task.WhenAll(Misc.ConcatTasks(producers, consumers));
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine($"\n{nameof(OperationCanceledException)} thrown\n");
            }
            finally
            {
                tokenSource.Dispose();
            }

            // Task.WaitAll(Misc.ConcatTasks(producers, consumers));
        }
    }

    class Data
    {
        public Data()
        {
            list = new List<int>();
        }

        public static List<int> list;
    }
}