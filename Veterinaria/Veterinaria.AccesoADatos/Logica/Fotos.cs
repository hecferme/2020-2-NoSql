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
            var elNombreCompletoDelArchivo = elArchivoParaCargar.ToLower();
            char[] elVectorDeSeparadores = new char[1];
            elVectorDeSeparadores[0] = '\\';
            var elNombreDescompuestoDelArchivo = elNombreCompletoDelArchivo.Split(elVectorDeSeparadores);
            var elNombreSolitoDelArchivo = elNombreDescompuestoDelArchivo[elNombreDescompuestoDelArchivo.Length - 1];
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
            var id = bucket.UploadFromStream(elNombreSolitoDelArchivo, stream, options);
            return id;
        }
    }
}
