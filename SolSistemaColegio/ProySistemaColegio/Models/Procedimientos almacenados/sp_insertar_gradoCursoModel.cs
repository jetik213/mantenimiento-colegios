using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ProySistemaColegio.Models.Procedimientos_almacenados
{
    public class sp_insertar_gradoCursoModel
    {
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
