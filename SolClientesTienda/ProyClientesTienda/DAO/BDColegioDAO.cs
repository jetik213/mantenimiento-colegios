using ProyClientesTienda.Entidades;
using System.Data.SqlClient;

namespace ProyClientesTienda.DAO
{
    public class BDColegioDAO
    {
        private readonly string cad_conexion;

        public BDColegioDAO(IConfiguration config)
        {
            cad_conexion = config.GetConnectionString("cn1");
        }

        public List<Clientes> ListarClientes()
        {
            var lista = new List<Clientes>();
            SqlDataReader dr = SqlHelper.ExecuteReader(cad_conexion, "sp_listar_clientes");
            while (dr.Read())
            {
                lista.Add(new Clientes()
                {
                    cli_cod = dr.GetString(0),
                    cli_nom = dr.GetString(1),
                    cli_tel = dr.GetInt32(2)
                });
            }
            dr.Close();
            return lista;
        }

        public string GrabarCliente(Clientes obj)
        {
            string mensaje = "";

            try
            {
                SqlHelper.ExecuteNonQuery(cad_conexion, "sp_insertar_cliente", obj.cli_cod, obj.cli_nom, obj.cli_tel);
                mensaje = $"Se registró el cliente: {obj.cli_nom}";
            }
            catch (Exception e)
            {
                mensaje = e.Message;
            }
            return mensaje;
        }

    }
}
