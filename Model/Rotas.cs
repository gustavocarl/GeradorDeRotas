using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Rotas
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Display(Name = "Nome do Arquivo")]
        public string NomeDoArquivo { get; set; }

        public string CaminhoCompleto { get; set; }

        [Display(Name = "Serviço")]
        public string Servico { get; set; }

        [Display(Name = "Cidade")]
        public string Cidade { get; set; }

        [Display(Name = "Data da Rota")]
        public string Data { get; set; }

        public List<string> Cabecalho { get; set; }

        [Display(Name = "Equipes")]
        public List<string> Equipes { get; set; }

    }
}
