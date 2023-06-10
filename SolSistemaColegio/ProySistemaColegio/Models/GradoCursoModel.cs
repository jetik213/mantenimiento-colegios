using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ProySistemaColegio.Models
{
    public class GradoCursoModel
    {
        public int graCur_cod { get; set; }
        public int gra_cod { get; set; }
        [Display(Name = "Código de curso")]
        public int cur_cod { get; set; }
        public string graCur_eli { get; set; }
    }
}
