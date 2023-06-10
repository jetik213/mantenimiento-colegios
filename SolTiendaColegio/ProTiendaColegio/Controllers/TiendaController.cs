using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProTiendaColegio.Models;
using System.Data.SqlClient;
using Newtonsoft.Json;
using ProTiendaColegio.Controllers.DAO;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Transactions;

namespace ProySistemaColegio.Controllers
{
    public class TiendaController : Controller
    {
        //DAO
        private readonly TiendaDAO dao;
        public TiendaController(TiendaDAO _dao)
        {
            dao = _dao;
        }

        List<CarritoModel> TraerCarrito()
        {
            List<CarritoModel>? lista =
                JsonConvert.DeserializeObject<List<CarritoModel>>(
                  HttpContext.Session.GetString("Carrito")
                  );
            return lista;
        }

        //CONTROLLER

        // GET: TiendaController
        public IActionResult ListarProductosTienda()
        {
            if (HttpContext.Session.GetString("Carrito") == null)
                HttpContext.Session.SetString("Carrito", JsonConvert.SerializeObject(new List<CarritoModel>()));
            return View(dao.ListadoProductoCC());
        }

        public IActionResult AgregarCarrito(int id = 0)
        {
            return View(dao.BuscarProducto(id));
        }

        [HttpPost]
        public IActionResult AgregarCarrito(int id = 0, int cantidad = 0)
        {
            sp_listar_productosModel obj = dao.BuscarProducto(id);

            if (obj.pro_stock < cantidad)
            {
                ViewBag.MENSAJE = "Stock insuficiente!";
                return View(obj);
            }

            CarritoModel cm = new CarritoModel()
            {
                Codigo = obj.pro_cod,
                Descripcion = obj.pro_desc,
                Cantidad = cantidad,
                Precio = obj.pro_precio
            };

            var lista_carrito = JsonConvert.DeserializeObject<List<CarritoModel>>(HttpContext.Session.GetString("Carrito"));

            var encontrado = lista_carrito.Find(c => c.Codigo.Equals(cm.Codigo));
            if(encontrado != null)
            {
                encontrado.Cantidad = encontrado.Cantidad + cm.Cantidad;
                ViewBag.MENSAJE = "El artículo fue actualizado en el carrito";
            }
            else
            {
                lista_carrito.Add(cm);
                ViewBag.MENSAJE = "Nuevo artículo agregado al carrito";
            }
            
            HttpContext.Session.SetString("Carrito", JsonConvert.SerializeObject(lista_carrito));

            return View(obj);
        }

        public IActionResult VerCarrito()
        {
            List<CarritoModel> lista_carrito = null;
            if (HttpContext.Session.GetString("Carrito") != null)
            {
                lista_carrito =
                   JsonConvert.DeserializeObject<List<CarritoModel>>(
                    HttpContext.Session.GetString("Carrito")
                     );
                if (lista_carrito == null || lista_carrito.Count == 0)
                    return RedirectToAction("ListarProductosTienda");
            }

            ViewBag.TOTAL = lista_carrito.Sum(c => c.Importe);
            return View(lista_carrito);
        }

        public IActionResult Eliminar(int id = 0)
        {
            List<CarritoModel>? lista_carrito =

        JsonConvert.DeserializeObject<List<CarritoModel>>(

          HttpContext.Session.GetString("Carrito"));
          CarritoModel? buscar = lista_carrito.Find(a => a.Codigo.Equals(id));
          lista_carrito.Remove(buscar);
          HttpContext.Session.SetString("Carrito",
          JsonConvert.SerializeObject(lista_carrito));
            return RedirectToAction("VerCarrito");
        }

        public IActionResult PagarCarrito()
        {
            List<CarritoModel>? lista_carrito = JsonConvert.DeserializeObject<List<CarritoModel>>(HttpContext.Session.GetString("Carrito"));
            ViewBag.CLIENTES =
              new SelectList(dao.ListadoClientes(), "cli_cod", "cli_nom");
            return View(lista_carrito);
        }

        [HttpPost]
        public IActionResult PagarCarrito(string cli_id = "")
        {
            List<CarritoModel> lista_carrito = TraerCarrito();
            using(TransactionScope trx = new TransactionScope())
            {
                try
                {
                   decimal total = lista_carrito.Sum(c => c.Importe);
                    string numero = dao.GrabarVentaWeb(cli_id, total, lista_carrito);
                    ViewBag.MENSAJE = $"La Venta: {numero} fué realizada con Éxito";
                    trx.Complete();
                    HttpContext.Session.Clear();
                    return RedirectToAction(nameof(ListarProductosTienda));
                }
                catch (Exception ex)
                {
                    ViewBag.MENSAJE = ex.Message;
                }
            }
            ViewBag.CLIENTES =
              new SelectList(dao.ListadoClientes(), "cli_cod", "cli_nom");
            return View(lista_carrito);
        }
    }
}
