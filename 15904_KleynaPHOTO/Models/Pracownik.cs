using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _15904_KleynaPHOTO.Models
{
    public class Pracownik
    {
        public int PracownikID { get; set; }

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
        public string PracownikImie { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(20, ErrorMessage = "Nazwisko jest za długie")]
        public string PracownikNazwisko { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(30, ErrorMessage = "Nazwa stanowiska jest za długa")]
        [DisplayName("Stanowisko")]
        public string PracownikStanowisko { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(11, ErrorMessage = "Pesel ma 11 znaków")]
        [DisplayName("Pesel")]
        public string PracownikPesel{ get; set; }

        [Required(AllowEmptyStrings = false)]
        [DisplayName("Data zatrudnienia")]
        public string PracownikData { get; set; }


        [Required(AllowEmptyStrings = false)]
        [DisplayName("Pensja")]
        public double PracownikPensja { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DisplayName("Dodatek")]
        public double PracownikDodatek { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DisplayName("Podaj login")]
        [StringLength(30, ErrorMessage = "Login jest za długi")]
        public string PracownikLogin { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DisplayName("Podaj hasło")]
        [StringLength(30, ErrorMessage = "Hasło jest za długie")]
        public string PracownikHaslo { get; set; }

        public string PracownikRola { get; set; }
    }
}
