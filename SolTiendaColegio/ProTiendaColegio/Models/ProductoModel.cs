using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ProTiendaColegio.Models
{
    public class ProductoModel
    {
        [Display(Name = "Código")]
        public int pro_cod { get; set; }
        [Display(Name = "Descripción")]
        public string pro_desc { get; set; }
        [Display(Name = "Categoría")]
        public int cat_cod { get; set; }
        [Display(Name = "Stock")]
        public int pro_stock { get; set; }
        [Display(Name = "Precio")]
        public decimal pro_precio { get; set; }
        public string pro_eli { get; set; }
    } 
}
