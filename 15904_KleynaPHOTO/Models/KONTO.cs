using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _15904_KleynaPHOTO.Models
{
    public class KONTO
    {
        public int id { get; set; }

        [DisplayName("Login")]
        [Required(AllowEmptyStrings = false)]
        public string userLogin { get; set; }

        [DisplayName("Hasło")]
        [Required(AllowEmptyStrings = false)]
        public string userPassword { get; set; }

        public string rola { get; set; }
    }
}
