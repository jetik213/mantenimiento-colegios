using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ProTiendaColegio.Models
{
    public class ClienteModel
    {
        [Display(Name = "Código")]
        public string cli_cod { get; set; }
        [Display(Name = "Nombres")]
        public string cli_nom { get; set; }
        [Display(Name = "Teléfono")]
        public string cli_tel { get; set; }
    }
}
