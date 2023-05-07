using CO2BakalaurasAPI.Data;
using CO2BakalaurasAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CO2BakalaurasAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UzduotisController : ControllerBase
    {
        private readonly CO2DatabaseContext _dbContext;

        public UzduotisController(CO2DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        [HttpPost("CreateTask")]
        public IActionResult CreateTask([FromBody] Uzduotis request)
        {
            Uzduotis uzduotis = new()
            {
                ADMINISTRATORIAUS_ID = request.ADMINISTRATORIAUS_ID,
                PAVADINIMAS = request.PAVADINIMAS,
                APRASYMAS = request.APRASYMAS,
                TASKU_SKAICIUS = request.TASKU_SKAICIUS,
                KATEGORIJA = request.KATEGORIJA,
                PRIMINIMO_LAIKAS = request.PRIMINIMO_LAIKAS
            };
            try
            {
                _dbContext.UZDUOTIS.Add(uzduotis);
                _dbContext.SaveChanges();
                return Ok();
            }
            catch
            {
                return StatusCode(500);
            }
        }
        
        [HttpGet("GetTask")]
        public IActionResult GetTask()
        {
            try
            {
                var uzduotis = _dbContext.UZDUOTIS;
                if (uzduotis.IsNullOrEmpty())
                {
                    return StatusCode(404);
                }
                return Ok(uzduotis);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet("GetTaskByID/{ID}")]
        public IActionResult GetTaskByID([FromRoute] int ID)
        {
            try
            {
                var uzduotis = _dbContext.UZDUOTIS.FirstOrDefault(x => x.UZDUOTIES_ID == ID);
                if (uzduotis == null)
                {
                    return StatusCode(404);
                }
                return Ok(uzduotis);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPut("UpdateTask")]
        public IActionResult UpdateTask([FromBody] UzduotisRequest request)
        {
            try
            {
                var uzduotis = _dbContext.UZDUOTIS.FirstOrDefault(x => x.UZDUOTIES_ID == request.UZDUOTIES_ID);
                if (uzduotis == null) return StatusCode(404);

                uzduotis.PAVADINIMAS = request.PAVADINIMAS;
                uzduotis.PRIMINIMO_LAIKAS = request.PRIMINIMO_LAIKAS;
                uzduotis.TASKU_SKAICIUS = request.TASKU_SKAICIUS;
                uzduotis.APRASYMAS = request.APRASYMAS;
                uzduotis.KATEGORIJA = request.KATEGORIJA;
                uzduotis.ADMINISTRATORIAUS_ID = request.ADMINISTRATORIAUS_ID;
                _dbContext.Entry(uzduotis).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("DeleteTaskByID/{ID}")]
        public IActionResult DeleteTaskByID([FromRoute] int ID)
        {
            try
            {
                var uzduotis = _dbContext.UZDUOTIS.FirstOrDefault(x => x.UZDUOTIES_ID == ID);
                if (uzduotis == null) return StatusCode(404);
                _dbContext.Entry(uzduotis).State = EntityState.Deleted;
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
