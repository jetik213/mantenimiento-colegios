using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProySistemaColegio.Models;
using ProySistemaColegio.Models.Procedimientos_almacenados;
using System.Data.SqlClient;

namespace ProySistemaColegio.Controllers
{
    public class MatriculaController : Controller
    {
        //DAO 

        private readonly string cad_conexion;
        public MatriculaController(IConfiguration configuration)
        {
            cad_conexion = configuration.GetConnectionString("cn1");
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

        public List<EstudianteCodModel> ListadoEstudiante()
        {
            List<EstudianteCodModel> lista = new List<EstudianteCodModel>();
            SqlDataReader dr = SqlHelper.ExecuteReader(cad_conexion, "sp_listar_estudianteCOD");
            EstudianteCodModel obj;
            while (dr.Read())
            {
                obj = new EstudianteCodModel()
                {
                    est_cod = dr.GetInt32(0),
                    est_data = dr.GetString(1)
                };
                lista.Add(obj);
            }
            dr.Close();
            return lista;
        }

        public List<sp_listar_matriculasCCModel> ListadoMatriculasCC()
        {
            List<sp_listar_matriculasCCModel> lista = new List<sp_listar_matriculasCCModel>();
            SqlDataReader dr = SqlHelper.ExecuteReader(cad_conexion, "sp_listar_matriculasCC");
            sp_listar_matriculasCCModel obj;
            while (dr.Read())
            {
                obj = new sp_listar_matriculasCCModel()
                {
                    mat_cod = dr.GetInt32(0),
                    est_nom = dr.GetString(1),
                    sal_cod = dr.GetInt32(2)
                };
                lista.Add(obj);
            }
            dr.Close();
            return lista;
        }

        public string GrabarMatricula(sp_insertar_matriculaModel obj)
        {
            try
            {
                SqlHelper.ExecuteNonQuery(cad_conexion, "sp_insertar_matricula",
                  obj.gra_cod, obj.est_cod);
                //
                return $"La matrícula ha sido creado satisfactoriamente.";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string EliminarMatricula(int mat_cod)
        {
            try
            {
                SqlHelper.ExecuteNonQuery(cad_conexion, "sp_eliminar_matricula", mat_cod);
                return $"La matrícula fue eliminada correctamente";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        // GET: ProfesorController
        public ActionResult ListarMatriculas()
        {
            var listado = ListadoMatriculasCC();
            return View(listado);
        }

        // GET: MatriculaController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MatriculaController/Create
        public ActionResult InsertarMatricula()
        {
            sp_insertar_matriculaModel nuevo = new sp_insertar_matriculaModel();

            //
            ViewBag.GRADOS =
              new SelectList(ListadoGrados(), "gra_cod", "gra_desc");
            //
            ViewBag.ESTUDIANTES =
              new SelectList(ListadoEstudiante(), "est_cod", "est_data");
            //
            return View(nuevo);
        }

        // POST: AlumnoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InsertarMatricula(sp_insertar_matriculaModel obj)
        {
            try
            {
                ViewBag.MENSAJE = GrabarMatricula(obj);
                return RedirectToAction(nameof(ListarMatriculas));
            }
            catch (Exception ex)
            {
                ViewBag.MENSAJE = ex.Message;
            }
            //
            ViewBag.GRADOS =
              new SelectList(ListadoGrados(), "gra_cod", "gra_desc");
            //
            ViewBag.ESTUDIANTES =
              new SelectList(ListadoEstudiante(), "est_cod", "est_data");
            //
            return View(obj);
        }

        // GET: MatriculaController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MatriculaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MatriculaController/Delete/5
        public ActionResult AnularMatricula(int id)
        {
            sp_listar_matriculasCCModel obj = ListadoMatriculasCC().Find(v => v.sal_cod.Equals(id));

            return View(obj);
        }

        // POST: AlumnoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AnularMatricula(int id, IFormCollection collection)
        {
            try
            {
                ViewBag.MENSAJE = EliminarMatricula(id);
                return RedirectToAction(nameof(ListarMatriculas));
            }
            catch (Exception ex)
            {
                ViewBag.MENSAJE = ex.Message;
            }
            return View();
        }
    }
}
