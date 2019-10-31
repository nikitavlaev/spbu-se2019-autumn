using System;
using System.Linq;
using System.Threading;

namespace Task03
{
    public class Consumer
    {
        public Consumer(CancellationToken ct)
        {
            Console.WriteLine("  Consumer created in thread {0}", Thread.CurrentThread.ManagedThreadId);
            while (true) 
            {
                if (ct.IsCancellationRequested)
                {
                    Console.WriteLine("Task {0} cancelled", Thread.CurrentThread.ManagedThreadId);
                    ct.ThrowIfCancellationRequested();
                }

                Program.mutexIn.WaitOne();
                Program.mutexRead.WaitOne();
                
                if ((++Program.readCount) == 1)
                {
                    Program.semW.Wait();
                }

                Program.mutexRead.ReleaseMutex();
                Program.mutexIn.ReleaseMutex();

                Consume();

                Program.mutexRead.WaitOne();

                if ((--Program.readCount) == 0)
                {
                    Program.semW.Release();
                }

                Program.mutexRead.ReleaseMutex();
                Thread.Sleep(1000 * Setup.sleepInSec);
            }
        }

        void Consume()
        {
            int id;
            if (Data.list.Any())
            {
                id = Data.list[0];
                Data.list.RemoveAt(0);
            }
            else
            {
                id = -1;
            }

            Console.WriteLine("  Consumed {0} by thread {1}", id, Thread.CurrentThread.ManagedThreadId);
        }
    }
}