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
        public string PracownikImie { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DisplayName("Nazwisko")]
        public string PracownikNazwisko { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DisplayName("Stanowisko")]
        public string PracownikStanowisko { get; set; }


        [DisplayName("Pesel")]
        public string PracownikPesel{ get; set; }

        [DisplayName("Data zatrudnienia")]
        public string PracownikData { get; set; }

        [DisplayName("Pensja")]
        public double PracownikPensja { get; set; }

        [DisplayName("Dodatek")]
        public double PracownikDodatek { get; set; }

        


    }
}
