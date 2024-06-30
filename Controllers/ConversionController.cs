using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PruebaTecnica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    //Manejo de la conversion de numero a su respectiva pronunciación.
    public class ConversionController : ControllerBase
    {
        [Authorize]
        [HttpPost]
        public IActionResult ConvertNumber([FromBody] NumeroRequest request)
        {
            if (request.Number < 0 || request.Number > 999999999999)
            {
                return BadRequest(new { Message = "El número debe estar entre 0 y 999,999,999,999" });
            }

            try
            {
                string numberInWords = request.Number.ToWords(new System.Globalization.CultureInfo("es"));
                return Ok(new { NumberInWords = numberInWords });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al convertir el número", Details = ex.Message });
            }
        }
    }

    public class NumeroRequest
    {
        public long Number { get; set; }
    }
}
