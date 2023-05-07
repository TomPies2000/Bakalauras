using CO2BakalaurasAPI.Data;
using Microsoft.AspNetCore.Mvc;

namespace CO2BakalaurasAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdministratoriusController : ControllerBase
    {
        private readonly CO2DatabaseContext _dbContext;

        public AdministratoriusController(CO2DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("GetAdminByUserID/{ID}")]
        public IActionResult GetAdminByUserID([FromRoute] int ID)
        {
            try
            {
                var admin = _dbContext.ADMINISTRATORIUS.FirstOrDefault(x => x.VARTOTOJO_ID == ID);
                if (admin == null)
                {
                    return StatusCode(404);
                }
                return Ok(admin);
            }
            catch
            {
                return StatusCode(500);
            }
        }

    }
}
