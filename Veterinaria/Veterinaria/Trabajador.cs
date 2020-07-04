using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Veterinaria.Model;

namespace Veterinaria
{
    public class Trabajador 
    {
        public void Trabajo01()
        {
            var laOpcion = string.Empty;
            while (laOpcion != "X")
            {
                DesplegarMenu();
                laOpcion = LeaLaOpcion();
                switch (laOpcion)
                {
                    case "1":
                        ListeLasColecciones();
                        break;
                    case "2":
                        ListeTodosLosAnimalitos();
                        break;
                    case "3":
                        ListeAnimalitosPorNombre();
                        break;
                    default:
                        break;
                }
            }
        }

        private void ListeTodosLosAnimalitos()
        {
            var client = new Veterinaria.AccesoADatos.Conexion();
            var laListaDeAnimalitos = client.ListarTodosLosAnimalitos();
            ImprimirListadoDeAnimalitos(laListaDeAnimalitos);
        }

        private void ImprimirListadoDeAnimalitos(IList<Animalito> laListaDeAnimalitos)
        {
            if (laListaDeAnimalitos.Count > 0)
            {
                Console.WriteLine("Lista de todos los animalitos:");
                foreach (var animalito in laListaDeAnimalitos)
                {
                    Console.WriteLine(string.Format("Id: {2}; Nombre: {0}; Tipo: {1}", animalito.Nombre, animalito.Tipo, animalito.AnimalitoId.ToString()));
                }
            }
            else
                Console.WriteLine("No se encontró ningún animalito.");
       }

        private void ListeAnimalitosPorNombre()
        {
            Console.Write("Digite el nombre del animalito: ");
            var elNombreDelAnimalito = Console.ReadLine();
            var client = new Veterinaria.AccesoADatos.Conexion();
            var laListaDeAnimalitos = client.ListarAnimalitosPorNombre(elNombreDelAnimalito);
            ImprimirListadoDeAnimalitos(laListaDeAnimalitos);
        }

        // comentarios
        private void ListeLasColecciones()
        {
            var client = new Veterinaria.AccesoADatos.Conexion();
            var laBaseDeDatos = client.ConectarConBaseDeDatos();
            var laListaDeColecciones = client.ListarTodasLasColeciones(laBaseDeDatos);

            Console.WriteLine("Lista de colecciones in the database:");
            foreach (var collection in laListaDeColecciones.ToList()) 
            { 
                Console.WriteLine(collection.ToString()); 
            }
        }

        private void DesplegarMenu()
        {
            Console.WriteLine("Menu Principal");
            Console.WriteLine("1. Listar las colecciones.");
            Console.WriteLine("2. Listar todos los animalitos.");
            Console.WriteLine("3. Listar los animalitos por nombre.");
            Console.WriteLine("X.  Salir");
        }

        private string LeaLaOpcion()
        {
            string elResultado = Console.ReadLine();
            return elResultado;
        }
    }
}
