using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ProySistemaColegio.Models.Procedimientos_almacenados
{
    public class sp_listar_profesoresCCModel
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
        public string dis_desc { get; set; }

        [Display(Name = "Pre")]
        public string dirPre_desc { get; set; }

        [Display(Name = "Dirección")]
        public string pro_dir { get; set; }

        [Display(Name = "Teléfono")]
        public int pro_tel { get; set; }

        [Display(Name = "Género")]
        public string gen_desc { get; set; }

        [Display(Name = "Tipo Doc")]
        public string tipDoc_desc { get; set; }

        [Display(Name = "N° Documento")]
        public int pro_docN { get; set; }

        [Display(Name = "Sueldo")]
        public decimal  pro_suel { get; set; }
    }
}
