using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ProTiendaColegio.Models
{
    public class CategoriaModel
    {
        [Display(Name = "Código")]
        public int cat_cod { get; set; }
        public string cat_desc { get; set; }
    }
}
