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

        public DateTime? Data { get; set; }
 
        public string? Status { get; set; }

        public string? Auditado { get; set; }

        public string? CopReverteu { get; set; }

        public string? Log { get; set; }

        public string? PDF { get; set; }

        public string? Foto { get; set; }

        public string? Contrato { get; set; }

        public string? WO { get; set; }

        [Key]
        [StringLength(20)]
        [Required]
        public string? OS { get; set; }

        public string? Assinante { get; set; }

        public string? Tecnico { get; set; }

        public string? Login { get; set; }

        public string? Matricula { get; set; }

        public string? COP { get; set; }

        public string? UltimoAlterar { get; set; }

        public string? Local { get; set; }

        public string? PontoCasaApt { get; set; }

        [StringLength(150)]
        [Required]
        public string? Cidade { get; set; }

        [StringLength(150)]
        [Required]
        public string? Base { get; set; }

        public DateTime? Horario { get; set; }

        public string? Segmento { get; set; }

        [StringLength(150)]
        [Required]
        public string? Servico { get; set; }

        public string? TipoServico { get; set; }

        public string? TipoOS { get; set; }

        [StringLength(150)]
        [Required]
        public string? Endereco { get; set; }
        
        [StringLength(10)]
        [Required]
        public string? Numero { get; set; }

        [StringLength(150)]
        public string? Complemento { get; set; }

        [StringLength(9)]
        [Required]
        public string? CEP { get; set; }

        public string? Node { get; set; }

        [StringLength(150)]
        [Required]
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
