using CO2BakalaurasAPI.Data;
using CO2BakalaurasAPI.Models;
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


        [HttpPost("CreateUser")]
        public IActionResult CreateUser([FromBody] VartotojasRequest request)
        {
            Vartotojas vartotojas = new()
            {
                PRISIJUNGIMO_VARDAS = request.PRISIJUNGIMO_VARDAS,
                VARTOTOJO_VARDAS = request.VARTOTOJO_VARDAS,
                VARTOTOJO_PAVARDE = request.VARTOTOJO_PAVARDE,
                VARTOTOJO_EMAIL = request.VARTOTOJO_EMAIL,
                VARTOTOJO_SLAPTAZODIS = request.VARTOTOJO_SLAPTAZODIS
            };
            try
            {
                _dbContext.VARTOTOJAS.Add(vartotojas);
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