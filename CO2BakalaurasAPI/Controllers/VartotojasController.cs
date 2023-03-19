using CO2BakalaurasAPI.Data;
using Microsoft.AspNetCore.Mvc;

namespace CO2BakalaurasAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VartotojasController : ControllerBase
    {
        private readonly CO2DatabaseContext _dbContext;

        
        public VartotojasController(CO2DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("GetUserByID/{ID}")]
        public IActionResult GetUserByID([FromRoute] int ID)
        {
            
            try 
            {
                var vartotojas = _dbContext.VARTOTOJAS.FirstOrDefault(x => x.VARTOTOJO_ID == ID);
                if (vartotojas == null)
                {
                    return StatusCode(404);
                }
                return Ok(vartotojas);
            }
            catch 
            {
                return StatusCode(500);
            }
        }

        [HttpGet("GetUserByLogin/{login}/{psw}")]
        public IActionResult GetUserByLogin([FromRoute] string login, [FromRoute] string psw)
        {
            try
            {
                var vartotojas = _dbContext.VARTOTOJAS.FirstOrDefault(x => x.PRISIJUNGIMO_VARDAS == login && x.VARTOTOJO_SLAPTAZODIS == psw);
                if (vartotojas == null)
                {
                    return StatusCode(404);
                }
                return Ok(vartotojas);
            }
            catch
            {
                return StatusCode(500);
            }
        }

    }
}