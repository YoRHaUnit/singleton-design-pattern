using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingletonDesignPattern
{
    public sealed class Singleton
    {
        private static Singleton instance = null;
        private static readonly object padlock = new object();
        public string name { get; set; }

        Singleton()
        {
        }

        public static Singleton Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new Singleton();
                    }
                    return instance;
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Task task1 = Task.Factory.StartNew(() => Singleton.Instance.name = "test1");
            Task task2 = Task.Factory.StartNew(() => Singleton.Instance.name = "test2");

            // Ici le risque est que les modifications du premier singleton instancié, correspondant ici a la task1, n'existent plus.
            // Dans la console, seul "test2" s'affiche et pas "test1"

            Task.WaitAll(task1, task2);
            Console.WriteLine("All threads complete");
            Console.WriteLine(Singleton.Instance.name);

            Console.ReadKey();
        }
    }


}
