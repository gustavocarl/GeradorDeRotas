using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Table("TB_CIDADE")]
    public class Cidade
    {
        [Key]
        [StringLength(20)]
        public string Id { get; set; }
        
        [StringLength(150)]
        [Required]
        public string Nome { get; set; }
    }
}
