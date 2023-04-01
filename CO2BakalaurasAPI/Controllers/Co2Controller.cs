using CO2BakalaurasAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CO2BakalaurasAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class Co2Controller : ControllerBase
    {
        private readonly CO2DatabaseContext _dbContext;
        public Co2Controller(CO2DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("GetCo2")]
        public IActionResult GetCo2()
        {
            try
            {
                var co2 = _dbContext.CO2.ToList();
                if (co2 == null)
                {
                    return StatusCode(404);
                }
                return Ok(co2);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
