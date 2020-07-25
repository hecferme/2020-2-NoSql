using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veterinaria.Model.MisModelos;

namespace Veterinaria.AccesoADatos
{
    public class Fotos
    {
        private IMongoDatabase ConectarConBaseDeDatos()
        {
            var laConexion = new Conexion();
            var laBaseDeDatos = laConexion.ConectarConBaseDeDatos();
            return laBaseDeDatos;
        }

        public ObjectId CargarArchivoPorMedioDeStream(string elArchivoParaCargar, 
            MetadataDeFotos laMetadata)
        {
            var database = ConectarConBaseDeDatos();
            IGridFSBucket bucket = new GridFSBucket(database); 
            Stream stream = File.Open(elArchivoParaCargar, FileMode.Open); 
            var options = new GridFSUploadOptions() 
            { 
                Metadata = new BsonDocument() 
                { 
                    { "descripcion", laMetadata.Descripcion }, 
                    { "fechaYHora", laMetadata.FechaYHora.ToString() } 
                } 
            };
            var id = bucket.UploadFromStream(elArchivoParaCargar, stream, options);
            return id;
        }
    }
}
