using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ProySistemaColegio.Models
{
    public class ProfesorModel
    {
        [Display(Name = "Cód. profesor")]
        public int pro_cod { get; set; }
        [Display(Name = "Nombre")]
        public string pro_nom { get; set; }
        [Display(Name = "A. Paterno")]
        public string pro_apePat { get; set; }
        [Display(Name = "A. Materno")]
        public string pro_apeMat { get; set; }
        [Display(Name = "Fecha de nacimiento")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime pro_fechNac { get; set; }
        [Display(Name = "Distrito")]
        public int dis_cod { get; set; }
        [Display(Name = "Pre")]
        public int dirPre_cod { get; set; }
        [Display(Name = "Dirección")]
        public string pro_dir { get; set; }
        [Display(Name = "Teléfono")]
        public int pro_tel { get; set; }
        [Display(Name = "Género")]
        public int gen_cod { get; set; }
        [Display(Name = "Tipo Doc")]
        public int tipDoc_cod { get; set; }
        [Display(Name = "N° Documento")]
        public int pro_docN { get; set; }
        [Display(Name = "Sueldo")]
        public decimal pro_suel { get; set; }
        public string pro_eli { get; set; }
    }
}
