using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace WorkWithPool
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Начало работы программы");
            ShowThreadInfo();
            Console.WriteLine("Запускаем Task1 в потоке из пула потоков");
            ThreadPool.QueueUserWorkItem(new WaitCallback(Task1));
            ShowThreadInfo();
            Console.WriteLine("Запускаем Task2 в потоке из пула потоков");
            Thread.Sleep(1000);
            ThreadPool.QueueUserWorkItem(Task2);
            ShowThreadInfo();
            Console.WriteLine("Главный поток.");
            Thread.Sleep(1000);
            Console.WriteLine("Главный поток завершен.\n");
            Console.WriteLine("Task1 и Task2 закончили работу");
            ShowThreadInfo();

            Console.WriteLine("Press any key to quit... ");
            Console.ReadLine();
        }

        static void ShowThreadInfo()
        {
            int workerThreads, completionPortThreads;
            ThreadPool.GetAvailableThreads(out workerThreads, out completionPortThreads);
            Console.WriteLine("Available> Worker: {0}, I/O: {1}", workerThreads, completionPortThreads);
            ThreadPool.GetMaxThreads(out workerThreads, out completionPortThreads);
            Console.WriteLine("      Max> Worker: {0}, I/O: {1}", workerThreads, completionPortThreads);
        }

        static void Task1(Object state)
        {
            Thread.CurrentThread.Name = "1";
            Console.WriteLine("Thread {0}:{1}", Thread.CurrentThread.Name, Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine();
            Thread.Sleep(200);
        }

        static void Task2(Object state)
        {
            Thread.CurrentThread.Name = "2";
            Console.WriteLine("Thread {0}:{1}", Thread.CurrentThread.Name, Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(200);
        }

    }
}
