using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ProySistemaColegio.Models.Procedimientos_almacenados
{
    public class sp_listar_salonCCModel
    {
        [Display(Name = "Cód. Salón")]
        public int sal_cod { get; set; }

        [Display(Name = "Nombre y Apellido")]
        public string pro_nom { get; set; }
        [Display(Name = "Grado")]
        public string gra_desc { get; set; }
        [Display(Name = "Curso")]
        public string cur_desc { get; set; }
    }
}
