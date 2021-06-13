using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace _15904_KleynaPHOTO.Models
{
    public class Product
    {
        public int ProductID { get; set; }


        [DisplayName("Producent")]
        public int ProductProducent { get; set; }

        [DisplayName("Lista Producentów")]
        public List<ProducentList> Producenci { get; set; }


        [DisplayName("Lista Kategori")]
        public List<CategoryList> Kategorie { get; set; }

        [DisplayName("Lista Podkategori")]
        public List<SubcategoryList> Podkategorie { get; set; }


        [DisplayName("Podkategoria")]
        public int ProductPodkategoriaID { get; set; }

        [DisplayName("Nazwa")]
        public string ProductNazwa { get; set; }

        [DisplayName("Opis")]
        public string ProductOpis { get; set; }

        [DisplayName("Cena Netto")]
        public decimal ProductCenaNetto { get; set; }

        [DisplayName("Podatek")]
        public int ProductPodatek { get; set; }

        [DisplayName("Ilość sztuk")]
        public int ProductIlosc_Sztuk_Magazyn { get; set; }
    }
}
