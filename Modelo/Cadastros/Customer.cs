using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Modelo.Cadastros
{
    public class Customer
    {
        [DisplayName("Id")]
        public long CustomerId { get; set; }
        [StringLength(10, MinimumLength = 10)]
        [RegularExpression("([0-9]{10})")]
        [Required]
        public string Cnpj { get; set; }
        [Required]
        public string Name { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
