using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ProySistemaColegio.Models.Procedimientos_almacenados
{
    public class sp_listar_gradoCursoModel
    {
        [Display(Name = "Código de curso")]
        public int graCur_cod { get; set; }
        [Display(Name = "Descripción")]
        public string graCur_desc { get; set; }

    }
}
