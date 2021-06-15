using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _15904_KleynaPHOTO.Models
{
    public class Klient
    {
        public int KlientID { get; set; }

        public int AdresID { get; set; }

        //start tabela adres
        [DisplayName("Ulica")]
        [Required(AllowEmptyStrings = false)]
        public string AdresUlica { get; set; }

        [DisplayName("Numer")]
        [Required(AllowEmptyStrings = false)]
        public string AdresNumer { get; set; }

        [DisplayName("Kod")]
        [Required(AllowEmptyStrings = false)]
        public string AdresKod { get; set; }

        [DisplayName("Miasto")]
        [Required(AllowEmptyStrings = false)]
        public string AdresMiasto { get; set; }
        //koniec tabeli adres


        [Required(AllowEmptyStrings = false)]
        [DisplayName("Imię")]
        public string KlientImie { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DisplayName("Nazwisko")]
        public string KlientNazwisko { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DisplayName("Pesel")]
        public string KlientPesel { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DisplayName("Telefon")]
        public string KlientTelefon { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DisplayName("Email")]
        public string KlientEmail { get; set; }


        [DefaultValue(0)]
        [DisplayName("Usuniety")]
        public string KlientUsuniety { get; set; }



    }
}
