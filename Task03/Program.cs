using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading;
using System.Threading.Tasks;

namespace Task03
{
    public class Program
    {
        public static Mutex mutexIn = new Mutex(initiallyOwned: false);
        public static SemaphoreSlim semNotEmpty = new SemaphoreSlim(0, Int32.MaxValue);

        public static async Task Main(string[] args)
        {
            Console.WriteLine("Start");
            Data<int> data = new Data<int>();
            Task[] producers = new Task[Setup.producersNum];
            Task[] consumers = new Task[Setup.consumersNum];

            //necessary setup to cancel with a help of tokens
            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;

            //run producers constructors
            for (int i = 0; i < Setup.producersNum; i++)
            {
                producers[i] = Task.Run(() => new Producer(token), token);
            }

            //run consumers constructors
            for (int i = 0; i < Setup.consumersNum; i++)
            {
                consumers[i] = Task.Run(() => new Consumer(token), token);
            }

            Console.ReadKey();
            Console.WriteLine("Cancelling");
            tokenSource.Cancel();
            
            //release half of the semaphore amount to avoid SemaphoreFullException
            semNotEmpty.Release(Int32.MaxValue/2);
            
            try
            {
                await Task.WhenAll(Misc.ConcatTasks(producers, consumers));
            }
            //exception is thrown to break out of loops
            catch (OperationCanceledException)
            {
                Console.WriteLine($"\n{nameof(OperationCanceledException)} thrown, finishing\n");
            }
            finally
            {
                tokenSource.Dispose();
            }
        }
    }

    class Data<T>
    {
        public Data()
        {
            list = new List<T>();
        }

        public static List<T> list;
    }
}