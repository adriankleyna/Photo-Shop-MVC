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
        [StringLength(30, ErrorMessage = "Nazwa jest za długa")]
        public string AdresUlica { get; set; }

        [DisplayName("Numer")]
        [Required(AllowEmptyStrings = false)]
        [StringLength(5, ErrorMessage = "Numer adresu jest za długi")]
        public string AdresNumer { get; set; }

        [DisplayName("Kod")]
        [Required(AllowEmptyStrings = false)]
        [StringLength(10, ErrorMessage = "Kod adresu jest za długi")]
        public string AdresKod { get; set; }

        [DisplayName("Miasto")]
        [Required(AllowEmptyStrings = false)]
        [StringLength(50, ErrorMessage = "Nazwa jest za długa")]
        public string AdresMiasto { get; set; }
        //koniec tabeli adres


        [Required(AllowEmptyStrings = false)]
        [StringLength(20, ErrorMessage = "Imie jest za długie")]
        [DisplayName("Imię")]
        public string KlientImie { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(20, ErrorMessage = "Nazwisko jest za długie")]
        [DisplayName("Nazwisko")]
        public string KlientNazwisko { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(11, ErrorMessage = "Pesel ma 11 znaków")]
        [DisplayName("Pesel")]
        public string KlientPesel { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(14, ErrorMessage = "Telefon jest za długi")]
        [DisplayName("Telefon")]
        public string KlientTelefon { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(30, ErrorMessage = "Email jest za długi")]
        [EmailAddress]
        [DisplayName("Email")]
        public string KlientEmail { get; set; }


        [DefaultValue(0)]
        [DisplayName("Usuniety")]
        [StringLength(1, ErrorMessage = "Wpisz 0 lub 1")]
        public string KlientUsuniety { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DisplayName("Podaj login")]
        [StringLength(30, ErrorMessage = "Login jest za długi")]
        public string KlientLogin { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DisplayName("Podaj hasło")]
        [StringLength(30, ErrorMessage = "Hasło jest za długie")]
        public string KlientHaslo { get; set; }


        public string KlientRola { get; set; }

    }
}
