﻿using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ProySistemaColegio.Models.Procedimientos_almacenados
{
    public class sp_listar_productosModel
    {
        [Display(Name = "Código")]
        public int pro_cod { get; set; }
        [Display(Name = "Descripción")]
        public string pro_desc { get; set; }
        [Display(Name = "Categoría")]
        public string cat_desc { get; set; }
        [Display(Name = "Stocl")]
        public int pro_stock { get; set; }
        [Display(Name = "Precio")]
        public decimal pro_precio { get; set; }
    }
}
