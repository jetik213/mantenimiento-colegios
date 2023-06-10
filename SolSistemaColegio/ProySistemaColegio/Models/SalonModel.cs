using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ProySistemaColegio.Models
{
    public class SalonModel
    {
        public int sal_cod { get; set; }
        [Display(Name = "Profesor")]
        public int pro_cod { get; set; }
        [Display(Name = "Grado y curso")]
        public int graCur_cod { get; set; }
        public string sal_eli { get; set; }
    }
}
