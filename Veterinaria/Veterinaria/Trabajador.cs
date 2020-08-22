﻿using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Veterinaria.Model;
using Veterinaria.Model.MisModelos;

namespace Veterinaria
{
    public class Trabajador 
    {
        private string elresultado;

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
                    case "7":
                        SubirFotoDelAnimalito();
                        break;
                    case "8":
                        InsertarAnimalito();
                        break;
                    case "9":
                        CambieElNombreDelAnimalito();
                        break;
                    default:
                        break;
                }
            }
        }

        private void InsertarAnimalito()
        {
            Console.Write("Digite el nombre del animalito: ");
            var elNombre = Console.ReadLine();
            Console.Write("Digite el tipo del animalito: ");
            var elTipo = Console.ReadLine();
            Console.Write("Digite la fecha de nacimiento del animalito: ");
            var laFechaDeNacimiento = Console.ReadLine();
            Console.Write("Digite el nombre del propietario del animalito: ");
            var elPropietario = Console.ReadLine();
            Console.Write("Digite el email del propietario del animalito: ");
            var elEmailDelPropietario = Console.ReadLine();
            Console.Write("Digite el teléfono del propietario del animalito: ");
            var elTelefonoDelPropietario = Console.ReadLine();

            var elAnimalito = new Animalito();
            elAnimalito.Nombre = elNombre;
            elAnimalito.Tipo = elTipo;
            elAnimalito.FechaNacimiento = new DateTime();
            elAnimalito.ElPropietario = new Propietario();
            elAnimalito.ElPropietario.Nombre = elPropietario;
            elAnimalito.ElPropietario.DireccionElectronica = elEmailDelPropietario;
            elAnimalito.ElPropietario.LosContactos = new List<ContactoTelefonico>();
            var elObjetoTelefono = new ContactoTelefonico();
            elObjetoTelefono.Proveedor = "Claro";
            elObjetoTelefono.NumeroTelefonico = int.Parse(elTelefonoDelPropietario);
            elAnimalito.ElPropietario.LosContactos.Add(elObjetoTelefono);
            var client = new Veterinaria.AccesoADatos.Conexion();
            client.InsertarAnimalito(elAnimalito);
        }

        private void SubirFotoDelAnimalito()
        {
            Console.Write("Digite el nombre del archivo con la foto: ");
            var elNombreDeLaFoto = Console.ReadLine();
            Console.Write("Digite la descripción de la foto: ");
            var laDescripcicion = Console.ReadLine();
            Console.Write("Digite la fecha y hora de la foto: ");
            var laFechaYHora = Console.ReadLine();
            var laMetadata = new MetadataDeFotos();
            laMetadata.Descripcion = laDescripcicion;
            laMetadata.FechaYHora = laFechaYHora;
            var client = new Veterinaria.AccesoADatos.Fotos();
            var elIdDeLaFoto = client.CargarArchivoPorMedioDeStream(elNombreDeLaFoto, laMetadata);
            if (elIdDeLaFoto != null)
                Console.WriteLine(string.Format("El id de la foto es {0}",
                    elIdDeLaFoto.ToString()));
            else
                Console.WriteLine("Ocurrió un error al subir el archivo");
            //           ImprimirListadoDeAnimalitos(elIdDeLaFoto);
            //5f1b9a30f107ec1e64a72ef6
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
                var contador = 0;
                foreach (var animalito in laListaDeAnimalitos)
                {
                    Console.WriteLine(string.Format("Animalito número {3}: Id: {2}; Nombre: {0}; Tipo: {1}; Contactos del propietario: {4}", animalito.Nombre, animalito.Tipo, animalito.AnimalitoId.ToString(), contador++.ToString(), ObtenerListaContactos(animalito.ElPropietario)));
                }
            }
            else
                Console.WriteLine("No se encontró ningún animalito.");
       }

        private string ObtenerListaContactos(Propietario elPropietario)
        {
            var elResultado = string.Empty;
            if (elPropietario != null && elPropietario.LosContactos != null)
            {
                foreach (var contacto in elPropietario.LosContactos)
                {
                    elResultado += string.Concat(string.Format("Proveedor: {0}; Numero: {1}", contacto.Proveedor, contacto.NumeroTelefonico.ToString()));
                }
            }
            return elResultado;
        }

        private void CambieElNombreDelAnimalito()
        {
            Console.Write("Digite el nombre del animalito: ");
            var elNombreDelAnimalito = Console.ReadLine();
            var client = new Veterinaria.AccesoADatos.Conexion();
            var laListaDeAnimalitos = client.ListarAnimalitosPorNombre(elNombreDelAnimalito);
            ImprimirListadoDeAnimalitos(laListaDeAnimalitos);
            Console.Write("Seleccione el número animalito cuyo nombre desea cambiar: ");
            var elAnimalitoSeleccionado = Console.ReadLine();
            var elNumeroDeAnimalito = 0;
            if (int.TryParse(elAnimalitoSeleccionado, out elNumeroDeAnimalito))
            {
                if (elNumeroDeAnimalito >= 0 && elNumeroDeAnimalito < laListaDeAnimalitos.Count)
                {
                    var elRegistroDelAnimalito = laListaDeAnimalitos[elNumeroDeAnimalito];
                    Console.Write(string.Format("El nombre actual del animalito es [{0}]. Digite el nuevo nombre: ", elRegistroDelAnimalito.Nombre));
                    var elNuevoNombreDelAnimalito = Console.ReadLine();
                    client.ActualizarNombreDeAnimalito(elRegistroDelAnimalito.AnimalitoId, elNuevoNombreDelAnimalito);
                }
            }
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
            Console.WriteLine("7. Subir foto del animalito.");
            Console.WriteLine("8. Insertar un animalito.");
            Console.WriteLine("9. Cambiar nombre de un animalito.");
            Console.WriteLine("X.  Salir");
        }

        private string LeaLaOpcion()
        {
            string elResultado = Console.ReadLine();
            return elResultado;
        }
    }
}
