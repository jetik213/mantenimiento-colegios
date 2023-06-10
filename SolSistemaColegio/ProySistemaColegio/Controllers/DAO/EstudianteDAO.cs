using ProySistemaColegio.Models.Procedimientos_almacenados;
using ProySistemaColegio.Models;
using System.Data.SqlClient;

namespace ProySistemaColegio.Controllers.DAO
{
    public class EstudianteDAO
    {
        private readonly string cad_conexion;

        public EstudianteDAO(IConfiguration configuration)
        {
            cad_conexion = configuration.GetConnectionString("cn1");
        }

        public List<EstudianteModel> ListadoEstudiante()
        {
            List<EstudianteModel> lista = new List<EstudianteModel>();
            SqlDataReader dr = SqlHelper.ExecuteReader(cad_conexion, "sp_listar_estudiantes");
            EstudianteModel obj;
            while (dr.Read())
            {
                obj = new EstudianteModel()
                {
                    est_cod = dr.GetInt32(0),
                    est_apePat = dr.GetString(1),
                    est_apeMat = dr.GetString(2),
                    est_fechNac = dr.GetDateTime(3),
                    dis_cod = dr.GetInt32(4),
                    dirPre_cod = dr.GetInt32(5),
                    est_dir = dr.GetString(6),
                    est_tel = dr.GetInt32(7),
                    gen_cod = dr.GetInt32(8),
                    tipDoc_cod = dr.GetInt32(9),
                    est_docN = dr.GetInt32(10),
                    suscribed = dr.GetInt32(11),
                    est_eli = dr.GetString(12)
                };
                lista.Add(obj);
            }
            dr.Close();
            return lista;
        }

        public List<sp_listar_estudiantesModel> ListadoEstudianteSP()
        {
            List<sp_listar_estudiantesModel> lista = new List<sp_listar_estudiantesModel>();
            SqlDataReader dr = SqlHelper.ExecuteReader(cad_conexion, "sp_listar_estudiantesCC");
            sp_listar_estudiantesModel obj;
            while (dr.Read())
            {
                obj = new sp_listar_estudiantesModel()
                {
                    est_cod = dr.GetInt32(0),
                    est_nom = dr.GetString(1),
                    est_apePat = dr.GetString(2),
                    est_apeMat = dr.GetString(3),
                    est_fechNac = dr.GetDateTime(4),
                    dis_desc = dr.GetString(5),
                    dirPre_desc = dr.GetString(6),
                    est_dir = dr.GetString(7),
                    est_tel = dr.GetInt32(8),
                    gen_desc = dr.GetString(9),
                    tipDoc_desc = dr.GetString(10),
                    est_docN = dr.GetInt32(11),
                    suscribed = dr.GetInt32(12)
                };
                lista.Add(obj);
            }
            dr.Close();
            return lista;
        }

    }
}
