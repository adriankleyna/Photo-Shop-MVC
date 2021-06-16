using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _15904_KleynaPHOTO.Models
{
    public class Product
    {
        public int ProductID { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DisplayName("Producent")]
        public int ProductProducent { get; set; }


        [DisplayName("Lista Producentów")]
        public List<ProducentList> Producenci { get; set; }


        [DisplayName("Lista Kategori")]
        public List<CategoryList> Kategorie { get; set; }

        [DisplayName("Lista Podkategori")]
        public List<SubcategoryList> Podkategorie { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DisplayName("Podkategoria")]
        public int ProductPodkategoriaID { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DisplayName("Nazwa")]
        public string ProductNazwa { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DisplayName("Opis")]
        public string ProductOpis { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DisplayName("Cena Netto")]
        public decimal ProductCenaNetto { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DisplayName("Podatek")]
        public int ProductPodatek { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DisplayName("Ilość sztuk")]
        public int ProductIlosc_Sztuk_Magazyn { get; set; }
    }
}
