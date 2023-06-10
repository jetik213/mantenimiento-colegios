namespace ProySistemaColegio.Models
{
    public class CarritoModel
    {
        public int Codigo { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Importe {
            get
            {
                return Precio * Cantidad;
            }
        }
    }
}
