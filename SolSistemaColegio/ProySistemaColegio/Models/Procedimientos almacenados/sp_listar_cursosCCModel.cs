using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ProySistemaColegio.Models.Procedimientos_almacenados
{
    public class sp_listar_cursosCCModel
    {
        public int cur_cod { get; set; }

        [Display(Name = "Grado")]
        public string gra_desc { get; set; }

        [Display(Name = "Curso")]
        public string cur_desc { get; set; }
    }
}
