using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veterinaria.Model
{
    [BsonIgnoreExtraElements]
    public class Animalito
    {
        [BsonId] 
        public ObjectId AnimalitoId { get; set; }
        [BsonElement("nombre")]
        public string Nombre { get; set; }
        [BsonElement("tipo")]
        public string Tipo { get; set; }
        [BsonExtraElements] 
        public BsonDocument Metadata { get; set; }
    }
}
