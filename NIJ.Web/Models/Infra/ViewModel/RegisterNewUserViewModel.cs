using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace NIJ.Web.Models.Infra.ViewModel
{
    public class RegisterNewUserViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        
        [Required]
        [StringLength(100, ErrorMessage = "A {0} precisa ter ao menos {2} e no maximo {1} caracteres de comprimento", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Senha")]
        [Compare("Password", ErrorMessage = "Os valores informados para senha e confirmação não são iguais.")]
        public string ConfirmPassword { get; set; }

    }
}
