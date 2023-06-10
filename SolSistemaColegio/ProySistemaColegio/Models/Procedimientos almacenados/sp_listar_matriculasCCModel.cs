using System.ComponentModel.DataAnnotations;

namespace ProySistemaColegio.Models.Procedimientos_almacenados
{
    public class sp_listar_matriculasCCModel
    {
        [Display(Name = "Cód. Matrícula")]
        public int mat_cod { get; set; }

        [Display(Name = "Alumno")]
        public string est_nom { get; set; }
        [Display(Name = "Cód. salón")]
        public int sal_cod { get; set; }
    }
}
