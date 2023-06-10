using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProySistemaColegio.Models;
using ProySistemaColegio.Models.Procedimientos_almacenados;
using System.Data.SqlClient;

namespace ProySistemaColegio.Controllers
{
    public class ProductoController : Controller
    {
        private readonly string cad_conexion;

        public ProductoController(IConfiguration configuration)
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

        public ProductoModel ObtenerProducto(int pro_cod)
        {

            SqlDataReader dr = SqlHelper.ExecuteReader(cad_conexion, "sp_obtener_producto", pro_cod);
            var obj = new ProductoModel();
            while (dr.Read())
            {
                obj = new ProductoModel()
                {
                    pro_cod = dr.GetInt32(0),
                    pro_desc = dr.GetString(1),
                    cat_cod = dr.GetInt32(2),
                    pro_stock = dr.GetInt32(3),
                    pro_precio = dr.GetDecimal(4)
                };
            }
            dr.Close();
            return obj;
        }

        public string GrabarProducto(ProductoModel obj)
        {
            try
            {
                SqlHelper.ExecuteNonQuery(cad_conexion, "sp_insertar_productos",
                  obj.pro_desc, obj.cat_cod, obj.pro_stock, obj.pro_precio);
                //
                return $"El producto '{obj.pro_desc}' fue registrado correctamente";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string EditarProducto(ProductoModel obj)
        {
            try
            {
                SqlHelper.ExecuteNonQuery(cad_conexion, "sp_editar_productos",
                   obj.pro_desc, obj.cat_cod, obj.pro_stock, obj.pro_precio, obj.pro_cod);
                //
                return $"El producto {obj.pro_desc} ha sido actualizado";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string EliminarProducto(int pro_cod)
        {
            try
            {
                SqlHelper.ExecuteNonQuery(cad_conexion, "sp_eliminar_productos", pro_cod);
                return $"El producto con código '{pro_cod}' fue eliminado correctamente";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        //CONTROLLER

        // GET: AlumnoController
        public IActionResult ListarProductos()
        {
            //var listado = dao.ListadoEstudianteSP();
            var listado = ListadoProductoCC();
            return View(listado);
        }

        // GET: ProductoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductoController/Create
        public ActionResult InsertarProducto()
        {
            ProductoModel nuevo = new ProductoModel();

            //
            ViewBag.CATEGORIAS =
              new SelectList(ListadoCategorias(), "cat_cod", "cat_desc");
            //
            return View(nuevo);
        }

        // POST: AlumnoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InsertarProducto(ProductoModel obj)
        {
            try
            {
                ViewBag.MENSAJE = GrabarProducto(obj);
                return RedirectToAction(nameof(ListarProductos));
            }
            catch (Exception ex)
            {
                ViewBag.MENSAJE = ex.Message;
            }
            //
            ViewBag.CATEGORIAS =
              new SelectList(ListadoCategorias(), "cat_cod", "cat_desc");
            //
            return View(obj);
        }


        // GET: ProductoController/Edit/5
        public ActionResult ActualizarProducto(int id)
        {
            var item = ObtenerProducto(id);
            //
            ViewBag.CATEGORIAS =
              new SelectList(ListadoCategorias(), "cat_cod", "cat_desc");
            //
            return View(item);
        }

        // POST: AlumnoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ActualizarProducto(int id, ProductoModel obj)
        {
            try
            {
                ViewBag.MENSAJE = EditarProducto(obj);
                return RedirectToAction(nameof(ListarProductos));
            }
            catch (Exception ex)
            {
                ViewBag.MENSAJE = ex.Message;
            }
            //
            ViewBag.CATEGORIAS =
              new SelectList(ListadoCategorias(), "cat_cod", "cat_desc");
            //
            return View();
        }

        // GET: ProductoController/Delete/5
        public ActionResult AnularProducto(int id)
        {
            sp_listar_productosModel obj = ListadoProductoCC().Find(v => v.pro_cod.Equals(id));

            return View(obj);
        }

        // POST: AlumnoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AnularProducto(int id, IFormCollection collection)
        {
            try
            {
                ViewBag.MENSAJE = EliminarProducto(id);
                return RedirectToAction(nameof(ListarProductos));
            }
            catch (Exception ex)
            {
                ViewBag.MENSAJE = ex.Message;
            }
            return View();
        }
    }
}
