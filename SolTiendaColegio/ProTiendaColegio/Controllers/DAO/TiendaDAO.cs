using ProTiendaColegio.Models;
using System.Data.SqlClient;

namespace ProTiendaColegio.Controllers.DAO
{
    public class TiendaDAO
    {
        private readonly string cad_conexion;

        public TiendaDAO(IConfiguration configuration)
        {
            cad_conexion = configuration.GetConnectionString("cn1");
        }

        public List<sp_listar_productosModel> ListadoProductoCC()
        {
            List<sp_listar_productosModel> lista = new List<sp_listar_productosModel>();
            SqlDataReader dr = SqlHelper.ExecuteReader(cad_conexion, "sp_listar_productos");
            sp_listar_productosModel obj;
            while (dr.Read())
            {
                obj = new sp_listar_productosModel()
                {
                    pro_cod = dr.GetInt32(0),
                    pro_desc = dr.GetString(1),
                    cat_desc = dr.GetString(2),
                    pro_stock = dr.GetInt32(3),
                    pro_precio = dr.GetDecimal(4)
                };
                lista.Add(obj);
            }
            dr.Close();
            return lista;
        }

        public List<CategoriaModel> ListadoCategorias()
        {
            List<CategoriaModel> lista = new List<CategoriaModel>();
            SqlDataReader dr = SqlHelper.ExecuteReader(cad_conexion, "sp_listar_categorias");
            CategoriaModel obj;
            while (dr.Read())
            {
                obj = new CategoriaModel()
                {
                    cat_cod = dr.GetInt32(0),
                    cat_desc = dr.GetString(1)
                };
                lista.Add(obj);
            }
            dr.Close();
            return lista;
        }

        public List<ClienteModel> ListadoClientes()
        {
            List<ClienteModel> lista = new List<ClienteModel>();
            SqlDataReader dr = SqlHelper.ExecuteReader(cad_conexion, "sp_listar_clientes");
            ClienteModel obj;
            while (dr.Read())
            {
                obj = new ClienteModel()
                {
                    cli_cod = dr.GetString(0),
                    cli_nom = dr.GetString(1)
                };
                lista.Add(obj);
            }
            dr.Close();
            return lista;
        }


        public sp_listar_productosModel BuscarProducto(int pro_cod)
        {
            sp_listar_productosModel buscado =
              ListadoProductoCC().Find(a => a.pro_cod.Equals(pro_cod));
            if (buscado == null)
                buscado = new sp_listar_productosModel();
            return buscado;
        }

        public string GrabarVentaWeb(string cli_id, decimal total, List<CarritoModel> listacar)
        {
            string? numero = SqlHelper.ExecuteScalar(cad_conexion,
                "sp_grabar_web_ventas_cab",cli_id, total).ToString();
            foreach (var item in listacar)
            {
                SqlHelper.ExecuteNonQuery(cad_conexion,
                  "sp_grabar_web_ventas_det",
                  numero, item.Codigo, item.Cantidad, item.Precio);
            }
            return numero;
        }
    }
}
