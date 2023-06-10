using System.ComponentModel.DataAnnotations;

namespace ProySistemaColegio.Models.Procedimientos_almacenados
{
    public class sp_insertar_matriculaModel
    {
        [Display(Name = "Grado de estudio")]
        public int gra_cod { get; set; }
        [Display(Name = "Estudiante (Ape. Paterno / N° Doc)")]
        public int est_cod { get; set; }
    }
}
