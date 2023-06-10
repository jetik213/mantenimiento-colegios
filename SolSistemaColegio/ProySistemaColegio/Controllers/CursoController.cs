using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProySistemaColegio.Models.Procedimientos_almacenados;
using ProySistemaColegio.Models;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProySistemaColegio.Controllers
{
    public class CursoController : Controller
    {
        //DAO 

        private readonly string cad_conexion;

        public CursoController(IConfiguration configuration)
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

        public List<sp_listar_cursosCCModel> ListadoCursosCC()
        {
            List<sp_listar_cursosCCModel> lista = new List<sp_listar_cursosCCModel>();
            SqlDataReader dr = SqlHelper.ExecuteReader(cad_conexion, "sp_listar_cursosCC");
            sp_listar_cursosCCModel obj;
            while (dr.Read())
            {
                obj = new sp_listar_cursosCCModel()
                {
                    cur_cod = dr.GetInt32(0),
                    gra_desc = dr.GetString(1),
                    cur_desc = dr.GetString(2)
                };
                lista.Add(obj);
            }
            dr.Close();
            return lista;
        }

        public sp_editar_gradoCursoModel ObtenerCurso(int cur_cod)
        {

            SqlDataReader dr = SqlHelper.ExecuteReader(cad_conexion, "sp_obtener_gradoCurso", cur_cod);
            var obj = new sp_editar_gradoCursoModel();
            while (dr.Read())
            {
                obj = new sp_editar_gradoCursoModel()
                {
                    cur_cod = dr.GetInt32(0),
                    cur_desc = dr.GetString(1)
                };
            }
            dr.Close();
            return obj;
        }

        public string GrabarCurso(sp_insertar_gradoCursoModel obj)
        {
            int primero = 0, segundo = 0, tercero = 0, cuarto = 0, quinto = 0, sexto = 0;

            if (obj.primero == true)
            {
                primero = 1;
            }
            if (obj.segundo == true)
            {
                segundo = 1;
            }
            if (obj.tercero == true)
            {
                tercero = 1;
            }
            if (obj.cuarto == true)
            {
                cuarto = 1;
            }
            if (obj.quinto == true)
            {
                quinto = 1;
            }
            if (obj.sexto == true)  
            {
                sexto = 1;
            }
            try
            {
                SqlHelper.ExecuteNonQuery(cad_conexion, "sp_insertar_gradoCurso",
                  primero, segundo, tercero, cuarto, quinto, sexto, obj.cur_desc);
                //
                return $"El curso fue registrado correctamente";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string EditarCurso(sp_editar_gradoCursoModel obj)
        {
            int primero = 0, segundo = 0, tercero = 0, cuarto = 0, quinto = 0, sexto = 0;

            if (obj.primero == true)
            {
                primero = 1;
            }
            if (obj.segundo == true)
            {
                segundo = 1;
            }
            if (obj.tercero == true)
            {
                tercero = 1;
            }
            if (obj.cuarto == true)
            {
                cuarto = 1;
            }
            if (obj.quinto == true)
            {
                quinto = 1;
            }
            if (obj.sexto == true)
            {
                sexto = 1;
            }
            try
            {
                SqlHelper.ExecuteNonQuery(cad_conexion, "sp_editar_gradoCurso",
                  primero, segundo, tercero, cuarto, quinto, sexto, obj.cur_desc, obj.cur_cod);
                //
                return $"El curso {obj.cur_desc} ha sido actualizado";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string EliminarCurso(int cur_cod)
        {
            try
            {
                SqlHelper.ExecuteNonQuery(cad_conexion, "sp_eliminar_gradoCurso", cur_cod);
                return $"El curso fue eliminado correctamente";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        //CONTROLLER 

        // GET: ProfesorController
        public ActionResult ListarCursos()
        {
            var listado = ListadoCursosCC();
            return View(listado);
        }


        // GET: CursoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult InsertarCurso()
        {
            sp_insertar_gradoCursoModel nuevo = new sp_insertar_gradoCursoModel();

            return View(nuevo);
        }

        // POST: AlumnoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InsertarCurso(sp_insertar_gradoCursoModel obj)
        {
            try
            {
                ViewBag.MENSAJE = GrabarCurso(obj);
                return RedirectToAction(nameof(ListarCursos));
            }
            catch (Exception ex)
            {
                ViewBag.MENSAJE = ex.Message;
            }
            return View(obj);
        }

        // GET: CursoController/Edit/5
        public ActionResult ActualizarCurso(int id)
        {
            var item = ObtenerCurso(id);
            return View(item);
        }

        // POST: AlumnoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ActualizarCurso(int id, sp_editar_gradoCursoModel obj)
        {
            try
            {
                ViewBag.MENSAJE = EditarCurso(obj);
                return RedirectToAction(nameof(ListarCursos));
            }
            catch (Exception ex)
            {
                ViewBag.MENSAJE = ex.Message;
            }
            return View();
        }
        // GET: ProfesorController/Delete/5
        public ActionResult AnularCurso(int id)
        {
            CursoModel obj = ListadoCursos().Find(v => v.cur_cod.Equals(id));

            return View(obj);
        }

        // POST: AlumnoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AnularCurso(int id, IFormCollection collection)
        {
            try
            {
                ViewBag.MENSAJE = EliminarCurso(id);
                return RedirectToAction(nameof(ListarCursos));
            }
            catch (Exception ex)
            {
                ViewBag.MENSAJE = ex.Message;
            }
            return View();
        }
    }
}
