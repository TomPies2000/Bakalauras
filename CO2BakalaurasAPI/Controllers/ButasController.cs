using CO2BakalaurasAPI.Data;
using CO2BakalaurasAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CO2BakalaurasAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ButasController : ControllerBase
    {
        private readonly CO2DatabaseContext _dbContext;
        public ButasController(CO2DatabaseContext dbContext)
        {
            _dbContext = dbContext;   
        }

        [HttpPost("CreateHouse")]
        public IActionResult CreateHouse([FromBody] ButasRequest request)
        {
            Butas butas = new()
            {
                SANAUDU_ID = request.SANAUDU_ID,
                PLOTAS = request.PLOTAS,
                SILDYMO_TIPAS = request.SILDYMO_TIPAS,
                PIRMINES_ELEKTROS_SANAUDOS = request.PIRMINES_ELEKTROS_SANAUDOS,
                PIRMINES_VANDENS_SANAUDOS = request.PIRMINES_VANDENS_SANAUDOS,
                PIRMINES_DUJU_SANAUDOS = request.PIRMINES_DUJU_SANAUDOS
            };
            try
            {
                _dbContext.BUTAS.Add(butas);
                _dbContext.SaveChanges();
                return Ok();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet("GetHouseByUsageId/{ID}")]
        public IActionResult GetHouseByUsageId([FromRoute] int ID)
        {
            try
            {
                var butas = _dbContext.BUTAS.FirstOrDefault(x => x.SANAUDU_ID == ID);
                if (butas == null)
                {
                    return StatusCode(404);
                }
                return Ok(butas);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
