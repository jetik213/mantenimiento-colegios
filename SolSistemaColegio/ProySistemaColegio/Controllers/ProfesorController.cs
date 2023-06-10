using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProySistemaColegio.Models;
using ProySistemaColegio.Models.Procedimientos_almacenados;
using System.Data.SqlClient;

namespace ProySistemaColegio.Controllers
{
    public class ProfesorController : Controller
    {
        //DAO 

        private readonly string cad_conexion;

        public ProfesorController(IConfiguration configuration)
        {
            cad_conexion = configuration.GetConnectionString("cn1");
        }

        /*private readonly EstudianteDAO dao;
        public EstudianteController(EstudianteDAO _dao)
        {
            dao = _dao;
        }*/

        public List<ProfesorModel> ListadoProfesores()
        {
            List<ProfesorModel> lista = new List<ProfesorModel>();
            SqlDataReader dr = SqlHelper.ExecuteReader(cad_conexion, "sp_listar_profesores");
            ProfesorModel obj;
            while (dr.Read())
            {
                obj = new ProfesorModel()
                {
                    pro_cod = dr.GetInt32(0),
                    pro_nom = dr.GetString(1),
                    pro_apePat = dr.GetString(2),
                    pro_apeMat = dr.GetString(3),
                    pro_fechNac = dr.GetDateTime(4),
                    dis_cod = dr.GetInt32(5),
                    dirPre_cod = dr.GetInt32(6),
                    pro_dir = dr.GetString(7),
                    pro_tel = dr.GetInt32(8),
                    gen_cod = dr.GetInt32(9),
                    tipDoc_cod = dr.GetInt32(10),
                    pro_docN = dr.GetInt32(11),
                    pro_suel = dr.GetDecimal(12),
                    pro_eli = dr.GetString(13)
                };
                lista.Add(obj);
            }
            dr.Close();
            return lista;
        }

        public List<sp_listar_profesoresCCModel> ListadoProfesoresSP()
        {
            List<sp_listar_profesoresCCModel> lista = new List<sp_listar_profesoresCCModel>();
            SqlDataReader dr = SqlHelper.ExecuteReader(cad_conexion, "sp_listar_profesoresCC");
            sp_listar_profesoresCCModel obj;
            while (dr.Read())
            {
                obj = new sp_listar_profesoresCCModel()
                {
                    pro_cod = dr.GetInt32(0),
                    pro_nom = dr.GetString(1),
                    pro_apePat = dr.GetString(2),
                    pro_apeMat = dr.GetString(3),
                    pro_fechNac = dr.GetDateTime(4),
                    dis_desc = dr.GetString(5),
                    dirPre_desc = dr.GetString(6),
                    pro_dir = dr.GetString(7),
                    pro_tel = dr.GetInt32(8),
                    gen_desc = dr.GetString(9),
                    tipDoc_desc = dr.GetString(10),
                    pro_docN = dr.GetInt32(11),
                    pro_suel = dr.GetDecimal(12)
                };
                lista.Add(obj);
            }
            dr.Close();
            return lista;
        }

        public List<DistritoModel> ListadoDistritos()
        {
            List<DistritoModel> lista = new List<DistritoModel>();
            SqlDataReader dr = SqlHelper.ExecuteReader(cad_conexion, "sp_listar_distritos");
            DistritoModel obj;
            while (dr.Read())
            {
                obj = new DistritoModel()
                {
                    dis_cod = dr.GetInt32(0),
                    dis_desc = dr.GetString(1)
                };
                lista.Add(obj);
            }
            dr.Close();
            return lista;
        }

        public List<DirPreModel> ListadoDirPres()
        {
            List<DirPreModel> lista = new List<DirPreModel>();
            SqlDataReader dr = SqlHelper.ExecuteReader(cad_conexion, "sp_listar_dirPres");
            DirPreModel obj;
            while (dr.Read())
            {
                obj = new DirPreModel()
                {
                    dirPre_cod = dr.GetInt32(0),
                    dirPre_desc = dr.GetString(1)
                };
                lista.Add(obj);
            }
            dr.Close();
            return lista;
        }

        public List<GeneroModel> ListadoGeneros()
        {
            List<GeneroModel> lista = new List<GeneroModel>();
            SqlDataReader dr = SqlHelper.ExecuteReader(cad_conexion, "sp_listar_generos");
            GeneroModel obj;
            while (dr.Read())
            {
                obj = new GeneroModel()
                {
                    gen_cod = dr.GetInt32(0),
                    gen_desc = dr.GetString(1)
                };
                lista.Add(obj);
            }
            dr.Close();
            return lista;
        }

        public List<TipoDocModel> ListadoTipoDoc()
        {
            List<TipoDocModel> lista = new List<TipoDocModel>();
            SqlDataReader dr = SqlHelper.ExecuteReader(cad_conexion, "sp_listar_tipoDocs");
            TipoDocModel obj;
            while (dr.Read())
            {
                obj = new TipoDocModel()
                {
                    tipDoc_cod = dr.GetInt32(0),
                    tipDoc_desc = dr.GetString(1)
                };
                lista.Add(obj);
            }
            dr.Close();
            return lista;
        }

        public ProfesorModel ObtenerProfesor(int pro_cod)
        {

            SqlDataReader dr = SqlHelper.ExecuteReader(cad_conexion, "sp_obtener_profesor", pro_cod);
            var obj = new ProfesorModel();
            while (dr.Read())
            {
                obj = new ProfesorModel()
                {
                    pro_cod = dr.GetInt32(0),
                    pro_nom = dr.GetString(1),
                    pro_apePat = dr.GetString(2),
                    pro_apeMat = dr.GetString(3),
                    pro_fechNac = dr.GetDateTime(4),
                    dis_cod = dr.GetInt32(5),
                    dirPre_cod = dr.GetInt32(6),
                    pro_dir = dr.GetString(7),
                    pro_tel = dr.GetInt32(8),
                    gen_cod = dr.GetInt32(9),
                    tipDoc_cod = dr.GetInt32(10),
                    pro_docN = dr.GetInt32(11),
                    pro_suel = dr.GetDecimal(12)
                };
            }
            dr.Close();
            return obj;
        }

        public string GrabarProfesor(ProfesorModel obj)
        {
            try
            {
                SqlHelper.ExecuteNonQuery(cad_conexion, "sp_insertar_profesor",
                  obj.pro_nom, obj.pro_apePat, obj.pro_apeMat, obj.pro_fechNac, obj.dis_cod,
                obj.dirPre_cod, obj.pro_dir, obj.pro_tel, obj.gen_cod, obj.tipDoc_cod, obj.pro_docN
                , obj.pro_suel);
                //
                return $"El profesor con doc. N°{obj.pro_docN} fue registrado correctamente";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string EditarProfesor(ProfesorModel obj)
        {
            try
            {
                SqlHelper.ExecuteNonQuery(cad_conexion, "sp_editar_profesor",
                   obj.pro_cod, obj.pro_nom, obj.pro_apePat, obj.pro_apeMat, obj.pro_fechNac, obj.dis_cod,
                obj.dirPre_cod, obj.pro_dir, obj.pro_tel, obj.gen_cod, obj.tipDoc_cod, obj.pro_docN, obj.pro_suel);
                //
                return $"El Profesor con Doc. N°{obj.pro_docN} ha sido actualizado";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string EliminarProfesor(int pro_cod)
        {
            try
            {
                SqlHelper.ExecuteNonQuery(cad_conexion, "sp_eliminar_profesor", pro_cod);
                return $"El profesor con doc N°{pro_cod} fue eliminado correctamente";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        // GET: ProfesorController
        public ActionResult ListarListadoProfesores()
        {
            var listado = ListadoProfesoresSP();
            return View(listado);
        }

        // GET: ProfesorController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult InsertarProfesor()
        {
            ProfesorModel nuevo = new ProfesorModel();

            //
            ViewBag.DISTRITOS =
              new SelectList(ListadoDistritos(), "dis_cod", "dis_desc");
            //
            ViewBag.DIRPREFIJOS =
              new SelectList(ListadoDirPres(), "dirPre_cod", "dirPre_desc");
            //
            ViewBag.GENEROS =
              new SelectList(ListadoGeneros(), "gen_cod", "gen_desc");

            ViewBag.TIPODOCS =
              new SelectList(ListadoTipoDoc(), "tipDoc_cod", "tipDoc_desc");
            //
            return View(nuevo);
        }

        // POST: AlumnoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InsertarProfesor(ProfesorModel obj)
        {
            try
            {
                ViewBag.MENSAJE = GrabarProfesor(obj);
                return RedirectToAction(nameof(ListarListadoProfesores));
            }
            catch (Exception ex)
            {
                ViewBag.MENSAJE = ex.Message;
            }
            //
            ViewBag.DISTRITOS =
              new SelectList(ListadoDistritos(), "dis_cod", "dis_desc");
            //
            ViewBag.DIRPREFIJOS =
              new SelectList(ListadoDirPres(), "dirPre_cod", "dirPre_desc");
            //
            ViewBag.GENEROS =
              new SelectList(ListadoGeneros(), "gen_cod", "gen_desc");

            ViewBag.TIPODOCS =
              new SelectList(ListadoTipoDoc(), "tipDoc_cod", "tipDoc_desc");
            //
            return View(obj);
        }

        // GET: ProfesorController/Edit/5
        public ActionResult ActualizarProfesor(int id)
        {
            var profesor = ObtenerProfesor(id);
            ViewBag.DISTRITOS =
              new SelectList(ListadoDistritos(), "dis_cod", "dis_desc");
            //
            ViewBag.DIRPREFIJOS =
              new SelectList(ListadoDirPres(), "dirPre_cod", "dirPre_desc");
            //
            ViewBag.GENEROS =
              new SelectList(ListadoGeneros(), "gen_cod", "gen_desc");

            ViewBag.TIPODOCS =
              new SelectList(ListadoTipoDoc(), "tipDoc_cod", "tipDoc_desc");
            //
            return View(profesor);
        }

        // POST: AlumnoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ActualizarProfesor(ProfesorModel obj)
        {
            try
            {
                ViewBag.MENSAJE = EditarProfesor(obj);
                return RedirectToAction(nameof(ListarListadoProfesores));
            }
            catch (Exception ex)
            {
                ViewBag.MENSAJE = ex.Message;
            }
            //
            ViewBag.DISTRITOS =
              new SelectList(ListadoDistritos(), "dis_cod", "dis_desc");
            //
            ViewBag.DIRPREFIJOS =
              new SelectList(ListadoDirPres(), "dirPre_cod", "dirPre_desc");
            //
            ViewBag.GENEROS =
              new SelectList(ListadoGeneros(), "gen_cod", "gen_desc");

            ViewBag.TIPODOCS =
              new SelectList(ListadoTipoDoc(), "tipDoc_cod", "tipDoc_desc");
            //
            return View();
        }

        // GET: ProfesorController/Delete/5
        public ActionResult AnularProfesor(int id)
        {
            ProfesorModel obj = ListadoProfesores().Find(v => v.pro_cod.Equals(id));

            return View(obj);
        }

        // POST: AlumnoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AnularProfesor(int id, IFormCollection collection)
        {
            try
            {
                ViewBag.MENSAJE = EliminarProfesor(id);
                return RedirectToAction(nameof(ListarListadoProfesores));
            }
            catch (Exception ex)
            {
                ViewBag.MENSAJE = ex.Message;
            }
            return View();
        }
    }
}
