using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class Pessoa
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Display(Name = "Nome Completo")]
        public string NomeCompleto { get; set; }

        public string NomeEquipe { get; set; } = string.Empty;
    }
}
