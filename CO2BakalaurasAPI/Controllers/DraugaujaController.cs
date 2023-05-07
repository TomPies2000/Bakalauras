using CO2BakalaurasAPI.Data;
using CO2BakalaurasAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CO2BakalaurasAPI.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class DraugaujaController : ControllerBase
    {
        private readonly CO2DatabaseContext _dbContext;

        public DraugaujaController(CO2DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("GetFriendByID/{ID}")]
        public IActionResult GetFriendByID([FromRoute] int ID)
        {
            try
            {
                var draugas = _dbContext.DRAUGAUJA.Where(x => x.PIRMO_DRAUGO_ID == ID);
                if (draugas == null)
                {
                    return StatusCode(404);
                }
                return Ok(draugas);
            }
            catch
            {
                return StatusCode(500);
            }
        }
        //
        [HttpGet("GetNewFriendByID/{ID}/{ID2}")]
        public IActionResult GetNewFriendByID([FromRoute] int ID, [FromRoute] int ID2)
        {
            try
            {
                var draugas = _dbContext.DRAUGAUJA.FirstOrDefault(x => x.PIRMO_DRAUGO_ID == ID && x.ANTRO_DRAUGO_ID == ID2);
                if (draugas == null)
                {
                    return StatusCode(404);
                }
                return Ok(draugas);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet("GetFriendRequestByID/{ID}")]
        public IActionResult GetFriendRequestByID([FromRoute] int ID)
        {
            try
            {
                var draugas = _dbContext.DRAUGAUJA.Where(x => x.ANTRO_DRAUGO_ID == ID);
                if (draugas == null)
                {
                    return StatusCode(404);
                }
                return Ok(draugas);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost("AddFriend")]
        public IActionResult AddFriend([FromBody] DraugaujaRequest request)
        {
            Draugauja draugauja = new()
            {
                PIRMO_DRAUGO_ID = request.PIRMO_DRAUGO_ID,
                ANTRO_DRAUGO_ID = request.ANTRO_DRAUGO_ID,
                PATVIRTINTAS = request.PATVIRTINTAS
            };
            try
            {
                _dbContext.DRAUGAUJA.Add(draugauja);
                _dbContext.SaveChanges();
                return Ok();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPut("UpdateDraugauja")]
        public IActionResult UpdateDraugauja([FromBody] DraugaujaRequest request)
        {
            try
            {
                var draugauja = _dbContext.DRAUGAUJA.FirstOrDefault(x => x.DRAUGO_ID == request.DRAUGO_ID);
                if (draugauja == null) return StatusCode(404);

                draugauja.PIRMO_DRAUGO_ID = request.PIRMO_DRAUGO_ID;
                draugauja.ANTRO_DRAUGO_ID = request.ANTRO_DRAUGO_ID;
                draugauja.PATVIRTINTAS = request.PATVIRTINTAS;

                _dbContext.Entry(draugauja).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        
        [HttpDelete("DeleteFriend/{ID}/{ID2}")]
        public IActionResult DeleteFriend([FromRoute] int ID, [FromRoute] int ID2 )
        {
            try
            {
                var draugauja = _dbContext.DRAUGAUJA.FirstOrDefault(x => x.PIRMO_DRAUGO_ID == ID && x.ANTRO_DRAUGO_ID == ID2);
                if (draugauja == null) return StatusCode(404);
                _dbContext.Entry(draugauja).State = EntityState.Deleted;

                draugauja = _dbContext.DRAUGAUJA.FirstOrDefault(x => x.PIRMO_DRAUGO_ID == ID2 && x.ANTRO_DRAUGO_ID == ID);
                if (draugauja == null) return StatusCode(404);
                _dbContext.Entry(draugauja).State = EntityState.Deleted;
                _dbContext.SaveChanges();

                return Ok();
            }
            catch 
            {
                return StatusCode(500);
            }

        }

        [HttpDelete("DeclineRequest/{ID}/{ID2}")]
        public IActionResult DeclineRequest([FromRoute] int ID, [FromRoute] int ID2)
        {
            try
            {
                var draugauja = _dbContext.DRAUGAUJA.FirstOrDefault(x => x.PIRMO_DRAUGO_ID == ID && x.ANTRO_DRAUGO_ID == ID2);
                if (draugauja == null) return StatusCode(404);
                _dbContext.Entry(draugauja).State = EntityState.Deleted;
                _dbContext.SaveChanges();
                return Ok();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("DeleteAllUserFriendships/{ID}")]
        public IActionResult DeleteAllUserFriendships([FromRoute] int ID)
        {
            try
            {
                var draugauja = _dbContext.DRAUGAUJA.Where(x => x.PIRMO_DRAUGO_ID == ID || x.ANTRO_DRAUGO_ID == ID);
                if (draugauja == null) return StatusCode(404);
                _dbContext.Entry(draugauja).State = EntityState.Deleted;
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
