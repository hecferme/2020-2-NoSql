using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veterinaria.Model;

namespace Veterinaria.AccesoADatos
{
    public class Conexion
    {
        // comentario

        public IMongoDatabase ConectarConBaseDeDatos ()
        {
            var client = new MongoClient("mongodb+srv://rwuser:12345@myatlascluster-as0q0.gcp.mongodb.net/veterinaria?retryWrites=true&w=majority");
            var database = client.GetDatabase("veterinaria");
            return database;
        }

        public IAsyncCursor<BsonDocument> ListarTodasLasColeciones (IMongoDatabase laBaseDeDatos)
        {
            var elResultado = laBaseDeDatos.ListCollections();
            return elResultado;
        }

        public IList<Animalito> ListarTodosLosAnimalitos ()
        {
            var laBaseDeDatos = ConectarConBaseDeDatos();
            var collection = laBaseDeDatos.GetCollection<Animalito>("animalitos");
            var filter = new BsonDocument();
            var elResultado = collection.Find(filter).ToList();
            return elResultado;
        }

        public IList<Animalito> ListarAnimalitosPorNombre(string elNombreDelAnimalito)
        {
            var laBaseDeDatos = ConectarConBaseDeDatos();
            var collection = laBaseDeDatos.GetCollection<Animalito>("animalitos");
            var expresssionFilter = Builders<Animalito>.Filter.Regex(x => x.Nombre, elNombreDelAnimalito);
            var elResultado = collection.Find(expresssionFilter).ToList();
            return elResultado;
        }

    }
}
