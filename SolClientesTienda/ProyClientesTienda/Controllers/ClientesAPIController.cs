using Microsoft.AspNetCore.Mvc;
using ProyClientesTienda.Entidades;
using ProyClientesTienda.DAO;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProyClientesTienda.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesAPIController : ControllerBase
    {
        private readonly BDColegioDAO dao;

        public ClientesAPIController(BDColegioDAO _dao)
        {
            dao = _dao;
        }

        // GET: api/<ClientesAPIController>
        [HttpGet(Name ="GetClientes")]
        public async Task<ActionResult<List<Clientes>>> Get()
        {
            var listado = await Task.Run(() => dao.ListarClientes());
            return Ok(listado);
        }

        // GET api/<ClientesAPIController>/5
        [HttpGet("{id}", Name ="GetClientesPorCodigo")]
        public string Get(string id)
        {
            return "value";
        }

        // POST api/<ClientesAPIController>
        [HttpPost("GrabarCliente")]
        public async Task<ActionResult<string>> Post([FromBody] Clientes obj)
        {
            string mensaje = await Task.Run(() => dao.GrabarCliente(obj));
            return Ok(mensaje);
        }

        // PUT api/<ClientesAPIController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ClientesAPIController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
