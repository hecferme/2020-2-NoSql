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
    public class Propietario
    {
        [BsonElement("nombre")]
        public string Nombre{ get; set; }

        [BsonElement("email")]
        public string DireccionElectronica { get; set; }

        [BsonExtraElements]
        public BsonDocument Metadata { get; set; }

        [BsonElement("contactosTelefonicos")]
        //public ContactoTelefonico[] LosContactos { get; set; }
        public IList<ContactoTelefonico> LosContactos { get; set; }

    }
}
