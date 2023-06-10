using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ProySistemaColegio.Models.Procedimientos_almacenados
{
    public class sp_editar_gradoCursoModel
    {
        [Display(Name = "Código")]
        public int cur_cod { get; set; }
        public bool primero { get; set; }
        public bool segundo { get; set; }
        public bool tercero { get; set; }
        public bool cuarto { get; set; }
        public bool quinto { get; set; }
        public bool sexto { get; set; }
        [Display(Name = "Curso")]
        public string cur_desc { get; set; }
    }
}
