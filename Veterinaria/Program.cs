using System;

namespace Veterinaria
{
    class Program
    {
        static void Main(string[] args)
        {
            DoJob();
        }

        static void DoJob ()
        {
            var worker = new Worker();
            worker.work01();
        }

    }
}
