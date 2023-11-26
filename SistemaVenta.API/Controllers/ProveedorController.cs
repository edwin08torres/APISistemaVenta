using Microsoft.AspNetCore.Mvc;
using SistemaVenta.API.Utilidad;
using SistemaVenta.BLL.Servicios.Contrato;
using SistemaVenta.DTO;

namespace SistemaVenta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorController : ControllerBase
    {
        private readonly IProveedor _proveedorServicio;

        public ProveedorController(IProveedor proveedorServicio)
        {
            _proveedorServicio = proveedorServicio;
        }

        [HttpGet]
        [Route("Listar")]
        public async Task<IActionResult> Listar()
        {
            var rsp = new Response<List<ProveedorDTO>>();

            try
            {
                rsp.status = true;
                rsp.value = await _proveedorServicio.listar();

            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;

            }
            return Ok(rsp);
        }
    }
}
