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
    [Table("TB_ROTAS")]
    public class Rotas
    {
        #region Inserção Banco de Dados

        public readonly static string INSERT = "INSERT INTO TB_ROTAS " +
            " ( OS, CIDADE, BASE, SERVICO, " +
            "ENDERECO, NUMERO, COMPLEMENTO, BAIRRO, CEP " +
            " ) VALUES (" +
            " @OS, @CIDADE, @BASE, @SERVICO, @ENDERECO, @NUMERO, @COMPLEMENTO, @BAIRRO, @CEP " +
            " ) ";

        public readonly static string GETALL = "SELECT " +
            " OS, CIDADE, BASE, SERVICO, " +
            " ENDERECO, NUMERO, COMPLEMENTO, BAIRRO, CEP " +
            "FROM TB_ROTAS ";

        #endregion

        #region Propriedades

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public DateTime? Data { get; set; }
 
        public string? Status { get; set; }

        public string? Auditado { get; set; }

        public string? CopReverteu { get; set; }

        public string? Log { get; set; }

        public string? PDF { get; set; }

        public string? Foto { get; set; }

        public string? Contrato { get; set; }

        public string? WO { get; set; }

        public string? OS { get; set; }

        public string? Assinante { get; set; }

        public string? Tecnico { get; set; }

        public string? Login { get; set; }

        public string? Matricula { get; set; }

        public string? COP { get; set; }

        public string? UltimoAlterar { get; set; }

        public string? Local { get; set; }

        public string? PontoCasaApt { get; set; }

        public string? Cidade { get; set; }

        public string? Base { get; set; }

        public DateTime? Horario { get; set; }

        public string? Segmento { get; set; }

        public string? Servico { get; set; }

        public string? TipoServico { get; set; }

        public string? TipoOS { get; set; }

        public string? Endereco { get; set; }
        
        public string? Numero { get; set; }

        public string? Complemento { get; set; }
        
        public string? CEP { get; set; }

        public string? Node { get; set; }

        public string? Bairro { get; set; }
    
        public string? Pacote { get; set; }

        public string? COD { get; set; }

        public string? Telefone1 { get; set; }

        public string? Telefone2 { get; set; }

        public string? OBS { get; set; }

        public string? OBSTecnico { get; set; }

        public string? Equipamento { get; set; }

        #endregion

    }
}
