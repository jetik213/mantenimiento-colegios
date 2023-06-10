using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ProySistemaColegio.Models.Procedimientos_almacenados
{
    public class sp_listar_estudiantesModel
    {
        [Display(Name = "Código")]
        public int est_cod { get; set; }

        [Display(Name = "Nombre")]
        [StringLength(50)]
        public string est_nom { get; set; }
        
        [Display(Name = "A. Paterno")]
        [StringLength(50)]
        public string est_apePat { get; set; }
        
        [Display(Name = "A. Materno")]
        [StringLength(50)]
        public string est_apeMat { get; set; }
        
        [Display(Name = "Fecha de nacimiento")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime est_fechNac { get; set; }
        
        [Display(Name = "Distrito")]
        public string dis_desc { get; set; }
        
        [Display(Name = "Pre")]
        public string dirPre_desc { get; set; }
        
        [Display(Name = "Dirección")]
        public string est_dir { get; set; }

        [Display(Name = "Teléfono")]
        public int est_tel { get; set; }

        [Display(Name = "Género")]
        public string gen_desc { get; set; }
        
        [Display(Name = "Doc")]
        public string tipDoc_desc { get; set; }
        
        [Display(Name = "N° Doc")]
        public int est_docN { get; set; }
        
        [Display(Name = "Matriculado")]
        public int suscribed { get; set; }
        
    }
}
