using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace _15904_KleynaPHOTO.Models
{
    public class Zamowienie
    {
        [DisplayName("Podaj pracownika")]
        public List<PracownikList> Pracownicy { get; set; }

        [DisplayName("Podaj ID pracownika")]
        public int PracownikID { get; set; }

        [DisplayName("Podaj ID Klienta")]
        public int KlientID { get; set; }

        [DisplayName("Podaj ID Zamówienia")]
        public int ZamowienieID { get; set; }

        [DisplayName("Data zamówienia")]
        public string ZamowienieData { get; set; }

        [DisplayName("Cena dostawy netto")]
        public double ZamowienieCenaNettoDostawy { get; set; }

        [DisplayName("Podatek Dostawa")]
        [DefaultValue(23)]
        public int ZamowieniePodatekDostawa { get; set; }

        [DisplayName("Podaj Wartość Koszyka Netto")]
        public double KoszykCennaNetto { get; set; }

        [DisplayName("Podaj ID Produktu")]
        public int ProductID { get; set; }




    }
}
