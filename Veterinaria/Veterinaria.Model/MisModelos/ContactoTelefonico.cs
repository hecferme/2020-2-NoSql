using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veterinaria.Model
{
    [BsonIgnoreExtraElements]
    public class ContactoTelefonico
    {
        [BsonElement("proveedor")]
        public string Proveedor { get; set; }
        [BsonElement("numero")]
        public int NumeroTelefonico { get; set; }
        [BsonExtraElements]
        public BsonDocument Metadata { get; set; }
    }
}
