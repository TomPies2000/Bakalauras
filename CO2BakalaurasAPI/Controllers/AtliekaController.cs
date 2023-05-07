using CO2BakalaurasAPI.Data;
using CO2BakalaurasAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CO2BakalaurasAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AtliekaController : ControllerBase
    {
        private readonly CO2DatabaseContext _dbContext;

        public AtliekaController(CO2DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("ExecuteTask")]
        public IActionResult ExecuteTask([FromBody] Atlieka request)
        {
            Atlieka atlieka = new()
            {
                VARTOTOJO_ID = request.VARTOTOJO_ID,
                UZDUOTIES_ID = request.UZDUOTIES_ID
            };
            try
            {
                _dbContext.ATLIEKA.Add(atlieka);
                _dbContext.SaveChanges();
                return Ok();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet("GetExecutedTasksByUserID/{ID}")]
        public IActionResult GetExecutedTasksByUserID([FromRoute] int ID)
        {
            try
            {
                var atlieka = _dbContext.ATLIEKA.Where(x => x.VARTOTOJO_ID == ID);
                if (atlieka == null)
                {
                    return StatusCode(404);
                }
                return Ok(atlieka);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("DeleteExecutedTasksByUserID/{ID}")]
        public IActionResult DeleteExecutedTasksByUserID([FromRoute] int ID)
        {
            try
            {
                var atlieka = _dbContext.ATLIEKA.FirstOrDefault(x => x.VARTOTOJO_ID == ID );
                if (atlieka == null) return StatusCode(404);
                _dbContext.Entry(atlieka).State = EntityState.Deleted;
                _dbContext.SaveChanges();
                return Ok();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("DeleteExecutedTasksByTaskID/{ID}")]
        public IActionResult DeleteExecutedTasksByTaskID([FromRoute] int ID)
        {
            try
            {
                var atlieka = _dbContext.ATLIEKA.FirstOrDefault(x => x.UZDUOTIES_ID == ID);
                if (atlieka == null) return StatusCode(404);
                _dbContext.Entry(atlieka).State = EntityState.Deleted;
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
