using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace _15904_KleynaPHOTO.Models
{
    public class CategoryList
    {
        public int id { get; set; }
        
        [DisplayName("Kategoria")]
        public string Category { get; set; }
    }
}
