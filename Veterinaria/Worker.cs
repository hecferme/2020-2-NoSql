using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veterinaria
{
    public class Worker
    {
        public void work01()
        {
            var option = string.Empty;
            while (option != "X")
            {
                showMenu();
                option = readOption();
            }
        }

        private void showMenu()
        {
            Console.WriteLine("Main Menu");
            Console.WriteLine("1. Keep Alive");
            Console.WriteLine("X.  Kill");
        }

        private string readOption()
        {
            string elResultado = Console.ReadLine();
            return elResultado;
        }
    }
}
