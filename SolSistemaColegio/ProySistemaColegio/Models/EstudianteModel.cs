using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ProySistemaColegio.Models
{
    public class EstudianteModel
    {
        [Display(Name = "Código")]
        public int est_cod { get; set; }
        [Display(Name = "Nombre")]
        public string est_nom { get; set; }
        [Display(Name = "A. Paterno")]
        public string est_apePat { get; set; }
        [Display(Name = "A. Materno")]
        public string est_apeMat { get; set; }
        [Display(Name = "Fecha de nacimiento")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime est_fechNac { get; set; }
        [Display(Name = "Distrito")]
        public int dis_cod { get; set; }
        [Display(Name = "Pre")]
        public int dirPre_cod { get; set; }
        [Display(Name = "Dirección")]
        public string est_dir { get; set; }
        [Display(Name = "Teléfono")]
        public int est_tel { get; set; }
        [Display(Name = "Género")]
        public int gen_cod { get; set; }
        [Display(Name = "Doc")]
        public int tipDoc_cod { get; set; }
        [Display(Name = "N° Doc")]
        public int est_docN { get; set; }
        [Display(Name = "Matriculado")]
        public int suscribed { get; set; }
        [Display(Name = "Eliminado")]
        public string est_eli { get; set; }
    }
}
