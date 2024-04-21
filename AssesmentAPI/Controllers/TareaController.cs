using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AssesmentAPI.Data;
using AssesmentAPI.Models;

namespace AssesmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareaController : ControllerBase
    {
        private readonly TareaData _tareaData;
        public TareaController(TareaData tareaData)
        {
            _tareaData = tareaData;
        }
        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<Tarea> Lista = await _tareaData.Listar();
            return StatusCode(StatusCodes.Status200OK, Lista);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Obtener(int id)
        {
            Tarea tarea = await _tareaData.Obtener(id);
            return StatusCode(StatusCodes.Status200OK, tarea);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] Tarea tarea)
        {
            bool respuesta = await _tareaData.Crear(tarea);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
        }

        [HttpPut]
        public async Task<IActionResult> Actualizar([FromBody] Tarea tarea)
        {
            bool respuesta = await _tareaData.Actualizar(tarea);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            bool respuesta = await _tareaData.Eliminar(id);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
        }
    }
}
