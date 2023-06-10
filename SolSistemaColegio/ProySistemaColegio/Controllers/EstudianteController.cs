using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Packaging;
using ProySistemaColegio.Controllers.DAO;
using ProySistemaColegio.Models.Procedimientos_almacenados;
using ProySistemaColegio.Models;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProySistemaColegio.Controllers
{
    public class EstudianteController : Controller
    {
        //DAO 

        private readonly string cad_conexion;

        public EstudianteController(IConfiguration configuration)
        {
            cad_conexion = configuration.GetConnectionString("cn1");
        }

        /*private readonly EstudianteDAO dao;
        public EstudianteController(EstudianteDAO _dao)
        {
            dao = _dao;
        }*/

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
                    est_nom = dr.GetString(1),
                    est_apePat = dr.GetString(2),
                    est_apeMat = dr.GetString(3),
                    est_fechNac = dr.GetDateTime(4),
                    dis_cod = dr.GetInt32(5),
                    dirPre_cod = dr.GetInt32(6),
                    est_dir = dr.GetString(7),
                    est_tel = dr.GetInt32(8),
                    gen_cod = dr.GetInt32(9),
                    tipDoc_cod = dr.GetInt32(10),
                    est_docN = dr.GetInt32(11),
                    suscribed = dr.GetInt32(12),
                    est_eli = dr.GetString(13)
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

        public EstudianteModel ObtenerEstudiante(int est_cod)
        {
            
            SqlDataReader dr = SqlHelper.ExecuteReader(cad_conexion, "sp_obtener_estudiante",est_cod);
            var obj = new EstudianteModel();
            while (dr.Read())
            {
                obj = new EstudianteModel()
                {
                    est_cod = dr.GetInt32(0),
                    est_nom = dr.GetString(1),
                    est_apePat = dr.GetString(2),
                    est_apeMat = dr.GetString(3),
                    est_fechNac = dr.GetDateTime(4),
                    dis_cod = dr.GetInt32(5),
                    dirPre_cod = dr.GetInt32(6),
                    est_dir = dr.GetString(7),
                    est_tel = dr.GetInt32(8),
                    gen_cod = dr.GetInt32(9),
                    tipDoc_cod = dr.GetInt32(10),
                    est_docN = dr.GetInt32(11),
                    suscribed = dr.GetInt32(12)
                };
            }
            dr.Close();
            return obj;
        }

        public string GrabarEstudiante(EstudianteModel obj)
        {
            try
            {
                SqlHelper.ExecuteNonQuery(cad_conexion, "sp_insertar_estudiante",
                  obj.est_nom, obj.est_apePat, obj.est_apeMat, obj.est_fechNac, obj.dis_cod,
                obj.dirPre_cod, obj.est_dir, obj.est_tel, obj.gen_cod, obj.tipDoc_cod, obj.est_docN);
                //
                return $"El Estudiante con Doc. N°{obj.est_docN} fue registrado correctamente";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string EditarEstudiante(EstudianteModel obj)
        {
            try
            {
                SqlHelper.ExecuteNonQuery(cad_conexion, "sp_editar_estudiante",
                  obj.est_cod, obj.est_nom, obj.est_apePat, obj.est_apeMat, obj.est_fechNac, obj.dis_cod,
                obj.dirPre_cod, obj.est_dir, obj.est_tel, obj.gen_cod, obj.tipDoc_cod, obj.est_docN);
                //
                return $"El Estudiante con Doc. N°{obj.est_docN} ha sido actualizado";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }



        public string EliminarEstudiante(int est_cod)
        {
            try
            {
                SqlHelper.ExecuteNonQuery(cad_conexion, "sp_eliminar_estudiante", est_cod);
                return $"El estudiante con Doc N°{est_cod} fue eliminado correctamente";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        //CONTROLLER

        // GET: AlumnoController
        public IActionResult ConsultarListadoAlumnos()
        {
            //var listado = dao.ListadoEstudianteSP();
            var listado = ListadoEstudianteSP();
            return View(listado);
        }

        // GET: AlumnoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AlumnoController/Create
        public ActionResult InsertarEstudiante()
        {
            EstudianteModel nuevo = new EstudianteModel();

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
        public ActionResult InsertarEstudiante(EstudianteModel obj)
        {
            try
            {
                ViewBag.MENSAJE = GrabarEstudiante(obj);
                return RedirectToAction(nameof(ConsultarListadoAlumnos));
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

        // GET: AlumnoController/Edit/5
        public ActionResult ActualizarEstudiante(int id)
        {
            var estudiante = ObtenerEstudiante(id);
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
            return View(estudiante);
        }

        // POST: AlumnoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ActualizarEstudiante(int id, EstudianteModel obj)
        {
            try
            {
                ViewBag.MENSAJE = EditarEstudiante(obj);
                return RedirectToAction(nameof(ConsultarListadoAlumnos));
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

        // GET: AlumnoController/Delete/5
        public ActionResult AnularEstudiante(int id)
        {
            sp_listar_estudiantesModel obj = ListadoEstudianteSP().Find(v => v.est_cod.Equals(id));

            return View(obj);
        }

        // POST: AlumnoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AnularEstudiante(int id, IFormCollection collection)
        {
            try
            {
                ViewBag.MENSAJE = EliminarEstudiante(id);
                return RedirectToAction(nameof(ConsultarListadoAlumnos));
            }
            catch (Exception ex)
            {
                ViewBag.MENSAJE = ex.Message;
            }
            return View();
        }
    }
}
