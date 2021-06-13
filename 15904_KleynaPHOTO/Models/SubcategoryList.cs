using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace _15904_KleynaPHOTO.Models
{
    public class SubcategoryList
    {
        public int id { get; set; }


        [DisplayName("Podkategoria")]
        public string Subcategory { get; set; }
    }
}
