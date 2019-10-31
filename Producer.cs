using System;
using System.Threading;

namespace Task03
{
    public class Producer
    {
        public Producer(CancellationToken ct)
        {
            Console.WriteLine("  Producer created in thread {0}", Thread.CurrentThread.ManagedThreadId);
            int i = 0;
            while(true) 
            {
                if (ct.IsCancellationRequested)
                {
                    Console.WriteLine("Task {0} cancelled", Thread.CurrentThread.ManagedThreadId);
                    ct.ThrowIfCancellationRequested();
                }
                
                i++;
                Program.mutexIn.WaitOne();
                Program.semW.Wait();

                Produce(Thread.CurrentThread.ManagedThreadId * 1000 + i);

                Program.semW.Release();
                Program.mutexIn.ReleaseMutex();
                
                Thread.Sleep(1000 * Setup.sleepInSec);
            }
        }

        void Produce(int id)
        {
            Data.list.Add(id);
            Console.WriteLine("  Produced {0} by thread {1}", id, Thread.CurrentThread.ManagedThreadId);
        }
    }
}