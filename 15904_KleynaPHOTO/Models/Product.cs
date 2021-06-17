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

 
        [DisplayName("Podkategoria")]
        public int ProductPodkategoriaID { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(30, ErrorMessage = "Nazwa jest za długa")]
        [DisplayName("Nazwa")]
        public string ProductNazwa { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(300, ErrorMessage = "Opis jest za długi")]
        [DisplayName("Opis")]
        public string ProductOpis { get; set; }

        
        [DisplayName("Cena Netto")]
        public decimal ProductCenaNetto { get; set; }

        [DefaultValue(23)]
        [DisplayName("Podatek")]
        public int ProductPodatek { get; set; }

        [DefaultValue(1)]
        [DisplayName("Ilość sztuk")]
        public int ProductIlosc_Sztuk_Magazyn { get; set; }
    }
}
