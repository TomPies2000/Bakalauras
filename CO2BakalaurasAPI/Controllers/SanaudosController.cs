using CO2BakalaurasAPI.Data;
using CO2BakalaurasAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CO2BakalaurasAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SanaudosController: ControllerBase
    {

        private readonly CO2DatabaseContext _dbContext;

        public SanaudosController(CO2DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("GetUsageByUserID/{userID}")]
        public IActionResult GetUsageByUserID([FromRoute] int userID)
        {
            try
            {
                var sanaudos = _dbContext.SANAUDOS.Where(x => x.VARTOTOJO_ID == userID);
                if (sanaudos.IsNullOrEmpty())
                {
                    return StatusCode(404);
                }
                return Ok(sanaudos);
            }
            catch
            {
                return StatusCode(500);
            }
        }
        [HttpPost("CreateUsage")]
        public IActionResult CreateUsage([FromBody] SanaudosRequest request)
        {
            Sanaudos sanaudos = new()
            {
                VARTOTOJO_ID = request.VARTOTOJO_ID,
                AUTOMOBILIO_RIDA = request.AUTOMOBILIO_RIDA,
                ELEKTROS_SANAUDOS = request.ELEKTROS_SANAUDOS,
                VANDENS_SANAUDOS = request.VANDENS_SANAUDOS,
                DUJU_SANAUDOS = request.DUJU_SANAUDOS,
                DATA = request.DATA
            };
            try
            {
                _dbContext.SANAUDOS.Add(sanaudos);
                _dbContext.SaveChanges();
                return Ok();
            }
            catch
            {
                return StatusCode(500);
            }
        }

    }
}
