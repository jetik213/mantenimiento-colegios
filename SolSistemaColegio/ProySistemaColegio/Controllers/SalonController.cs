using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProySistemaColegio.Models.Procedimientos_almacenados;
using ProySistemaColegio.Models;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProySistemaColegio.Controllers
{
    public class SalonController : Controller
    {
        //DAO 

        private readonly string cad_conexion;
        public SalonController(IConfiguration configuration)
        {
            cad_conexion = configuration.GetConnectionString("cn1");
        }

        public List<CursoModel> ListadoCursos()
        {
            List<CursoModel> lista = new List<CursoModel>();
            SqlDataReader dr = SqlHelper.ExecuteReader(cad_conexion, "sp_listar_cursos");
            CursoModel obj;
            while (dr.Read())
            {
                obj = new CursoModel()
                {
                    cur_cod = dr.GetInt32(0),
                    cur_desc = dr.GetString(1)
                };
                lista.Add(obj);
            }
            dr.Close();
            return lista;
        }

        public List<GradoModel> ListadoGrados()
        {
            List<GradoModel> lista = new List<GradoModel>();
            SqlDataReader dr = SqlHelper.ExecuteReader(cad_conexion, "sp_listar_grados");
            GradoModel obj;
            while (dr.Read())
            {
                obj = new GradoModel()
                {
                    gra_cod = dr.GetInt32(0),
                    gra_desc = dr.GetString(1)
                };
                lista.Add(obj);
            }
            dr.Close();
            return lista;
        }

        public List<sp_listar_gradoCursoModel> ListadoGradoCursosCC()
        {
            List<sp_listar_gradoCursoModel> lista = new List<sp_listar_gradoCursoModel>();
            SqlDataReader dr = SqlHelper.ExecuteReader(cad_conexion, "sp_listar_gradoCurso");
            sp_listar_gradoCursoModel obj;
            while (dr.Read())
            {
                obj = new sp_listar_gradoCursoModel()
                {
                    graCur_cod = dr.GetInt32(0),
                    graCur_desc = dr.GetString(1)
                };
                lista.Add(obj);
            }
            dr.Close();
            return lista;
        }
        public SalonModel ObtenerSalon(int sal_cod)
        {

            SqlDataReader dr = SqlHelper.ExecuteReader(cad_conexion, "sp_obtener_salon", sal_cod);
            var obj = new SalonModel();
            while (dr.Read())
            {
                obj = new SalonModel()
                {
                    sal_cod = dr.GetInt32(0),
                    pro_cod = dr.GetInt32(1),
                    graCur_cod = dr.GetInt32(2)
                };
            }
            dr.Close();
            return obj;
        }

        public List<ProfesoresCodModel> ListadoProfesores()
        {
            List<ProfesoresCodModel> lista = new List<ProfesoresCodModel>();
            SqlDataReader dr = SqlHelper.ExecuteReader(cad_conexion, "sp_listar_profesoresCOD");
            ProfesoresCodModel obj;
            while (dr.Read())
            {
                obj = new ProfesoresCodModel()
                {
                    pro_cod = dr.GetInt32(0),
                    pro_desc = dr.GetString(1)
                };
                lista.Add(obj);
            }
            dr.Close();
            return lista;
        }

        public List<sp_listar_salonCCModel> ListadoSalonesCC()
        {
            List<sp_listar_salonCCModel> lista = new List<sp_listar_salonCCModel>();
            SqlDataReader dr = SqlHelper.ExecuteReader(cad_conexion, "sp_listar_salonCC");
            sp_listar_salonCCModel obj;
            while (dr.Read())
            {
                obj = new sp_listar_salonCCModel()
                {
                    sal_cod = dr.GetInt32(0),
                    pro_nom = dr.GetString(1),
                    gra_desc = dr.GetString(2),
                    cur_desc = dr.GetString(3)
                };
                lista.Add(obj);
            }
            dr.Close();
            return lista;
        }

        public string GrabarSalon(SalonModel obj)
        {
            try
            {
                SqlHelper.ExecuteNonQuery(cad_conexion, "sp_insertar_salon",
                  obj.pro_cod, obj.graCur_cod);
                //
                return $"El salón ha sido creado satisfactoriamente.";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string EditarSalon(SalonModel obj)
        {
            try
            {
                SqlHelper.ExecuteNonQuery(cad_conexion, "sp_editar_salon",
                  obj.pro_cod, obj.graCur_cod, obj.sal_cod);
                //
                return $"El salón N° {obj.sal_cod} ha sido actualizado";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string EliminarSalon(int sal_cod)
        {
            try
            {
                SqlHelper.ExecuteNonQuery(cad_conexion, "sp_eliminar_salon", sal_cod);
                return $"El salón fue eliminado correctamente";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        // GET: ProfesorController
        public ActionResult ListarSalones()
        {
            var listado = ListadoSalonesCC();
            return View(listado);
        }

        // GET: SalonController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult InsertarSalon()
        {
            SalonModel nuevo = new SalonModel();

            //
            ViewBag.PROFESORES =
              new SelectList(ListadoProfesores(), "pro_cod", "pro_desc");
            //
            ViewBag.GRADOCURSOS =
              new SelectList(ListadoGradoCursosCC(), "graCur_cod", "graCur_desc");
            //
            return View(nuevo);
        }

        // POST: AlumnoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InsertarSalon(SalonModel obj)
        {
            try
            {
                ViewBag.MENSAJE = GrabarSalon(obj);
                return RedirectToAction(nameof(ListarSalones));
            }
            catch (Exception ex)
            {
                ViewBag.MENSAJE = ex.Message;
            }
            //
            ViewBag.PROFESORES =
              new SelectList(ListadoProfesores(), "pro_cod", "pro_desc");
            //
            ViewBag.GRADOCURSOS =
              new SelectList(ListadoGradoCursosCC(), "graCur_cod", "graCur_desc");
            //
            return View(obj);
        }

        // GET: SalonController/Edit/5
        public ActionResult ActualizarSalon(int id)
        {
            var item = ObtenerSalon(id);
            //
            ViewBag.PROFESORES =
              new SelectList(ListadoProfesores(), "pro_cod", "pro_desc");
            //
            ViewBag.GRADOCURSOS =
              new SelectList(ListadoGradoCursosCC(), "graCur_cod", "graCur_desc");
            //
            return View(item);
        }

        // POST: AlumnoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ActualizarSalon(int id, SalonModel obj)
        {
            try
            {
                ViewBag.MENSAJE = EditarSalon(obj);
                return RedirectToAction(nameof(ListarSalones));
            }
            catch (Exception ex)
            {
                ViewBag.MENSAJE = ex.Message;
            }
            //
            ViewBag.PROFESORES =
              new SelectList(ListadoProfesores(), "pro_cod", "pro_desc");
            //
            ViewBag.GRADOCURSOS =
              new SelectList(ListadoGradoCursosCC(), "graCur_cod", "graCur_desc");
            //
            return View();
        }

        // GET: ProfesorController/Delete/5
        public ActionResult AnularSalon(int id)
        {
            sp_listar_salonCCModel obj = ListadoSalonesCC().Find(v => v.sal_cod.Equals(id));

            return View(obj);
        }

        // POST: AlumnoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AnularSalon(int id, IFormCollection collection)
        {
            try
            {
                ViewBag.MENSAJE = EliminarSalon(id);
                return RedirectToAction(nameof(ListarSalones));
            }
            catch (Exception ex)
            {
                ViewBag.MENSAJE = ex.Message;
            }
            return View();
        }
    }
}
