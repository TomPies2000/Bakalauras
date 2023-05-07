using CO2BakalaurasAPI.Data;
using CO2BakalaurasAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CO2BakalaurasAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatistikaController : ControllerBase
    {
        private readonly CO2DatabaseContext _dbContext;
        public StatistikaController(CO2DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("CreateStatistic")]
        public IActionResult CreateStatistic([FromBody] StatistikaRequest request)
        {
            Statistika statistika = new()
            {
                VARTOTOJO_ID = request.VARTOTOJO_ID,
                LYGIS = request.LYGIS,
                LYGIO_PAVADINIMAS = request.LYGIO_PAVADINIMAS,
                TASKU_SUMA = request.TASKU_SUMA,
                LAIKOTARPIS = request.LAIKOTARPIS
            };
            try
            {
                _dbContext.STATISTIKA.Add(statistika);
                _dbContext.SaveChanges();
                return Ok();
            }
            catch
            {
                return StatusCode(500);
            }
        }


        [HttpGet("GetStatisticByUserId/{ID}")]
        public IActionResult GetStatisticByUserId([FromRoute] int ID)
        {
            try
            {
                var statistika = _dbContext.STATISTIKA.Where(x => x.VARTOTOJO_ID == ID);
                if (statistika == null)
                {
                    return StatusCode(404);
                }
                return Ok(statistika);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPut("UpdateStatistic")]
        public IActionResult UpdateStatistic([FromBody] StatistikaRequest request)
        {
            try
            {
                var statistika = _dbContext.STATISTIKA.FirstOrDefault(x => x.STATISTIKOS_ID == request.STATISTIKOS_ID);
                if (statistika == null) return StatusCode(404);

                statistika.VARTOTOJO_ID = request.VARTOTOJO_ID;
                //statistika.STATISTIKOS_ID = request.STATISTIKOS_ID;
                statistika.LYGIS = request.LYGIS;
                statistika.LYGIO_PAVADINIMAS = request.LYGIO_PAVADINIMAS;
                statistika.TASKU_SUMA = request.TASKU_SUMA;
                statistika.LAIKOTARPIS = request.LAIKOTARPIS;
                _dbContext.Entry(statistika).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

        }

        [HttpDelete("DeleteStatisticByUserID/{ID}")]
        public IActionResult DeleteStatisticByUserID([FromRoute] int ID)
        {
            try
            {
                var statistika = _dbContext.STATISTIKA.FirstOrDefault(x => x.VARTOTOJO_ID == ID);
                if (statistika == null) return StatusCode(404);
                _dbContext.Entry(statistika).State = EntityState.Deleted;
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
