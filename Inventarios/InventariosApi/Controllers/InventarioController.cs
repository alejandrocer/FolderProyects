using System.Threading.Tasks;
using InventariosApi.Data;
using InventariosApi.Entity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace InventariosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventarioController : ControllerBase
    {
        private readonly IInventarioService _Inventarios;
        public InventarioController(IInventarioService Inventarios)
        {
            _Inventarios = Inventarios;
        }

        [HttpPost("AddInventory")]
        public async Task<ActionResult> SetInventory([FromBody]JObject jObject)
        {
            var Invent = JsonConvert.DeserializeObject<Inventarios>(jObject.ToString());

            var result = await _Inventarios.AddInventario(Invent);

            return Ok(result);
        }

        [HttpGet("GetInventory/{id}")]
        public async Task<ActionResult> GetInventory(int ID)
        {
            var result = await _Inventarios.BuscarInventario(ID);

            return Ok(result);
        }

        [HttpPut("UpdateInventory/{id}")]
        public async Task<ActionResult> UpdateInventory([FromBody]JObject jObject)
        {
            var Invent = JsonConvert.DeserializeObject<Inventarios>(jObject.ToString());
            var result = await _Inventarios.ActualizarInventario(Invent);

            return Ok(result);
        }

        [HttpDelete("DeleteInventory/{id}")]
        public async Task<ActionResult> DeleteInventory([FromBody]JObject jObject)
        {
            var Invent = JsonConvert.DeserializeObject<Inventarios>(jObject.ToString());
            await _Inventarios.DeleteInventario(Invent);

            return Ok();
        }
    }
}