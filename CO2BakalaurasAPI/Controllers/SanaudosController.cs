﻿using CO2BakalaurasAPI.Data;
using CO2BakalaurasAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpPut("UpdateUsage")]
        public IActionResult UpdateUsage([FromBody] SanaudosRequest request)
        {
            try
            {
                var sanaudos = _dbContext.SANAUDOS.FirstOrDefault(x => x.SANAUDU_ID == request.SANAUDU_ID);
                if (sanaudos == null) return StatusCode(404);

                sanaudos.AUTOMOBILIO_RIDA = request.AUTOMOBILIO_RIDA;
                sanaudos.ELEKTROS_SANAUDOS = request.ELEKTROS_SANAUDOS;
                sanaudos.VANDENS_SANAUDOS = request.VANDENS_SANAUDOS;
                sanaudos.DUJU_SANAUDOS = request.DUJU_SANAUDOS;
                _dbContext.Entry(sanaudos).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

        }

        [HttpDelete("DeleteUsageByUserID/{ID}")]
        public IActionResult DeleteUsageByUserID([FromRoute] int ID)
        {
            try
            {
                var sanaudos = _dbContext.SANAUDOS.FirstOrDefault(x => x.VARTOTOJO_ID == ID);
                if (sanaudos == null) return StatusCode(404);
                _dbContext.Entry(sanaudos).State = EntityState.Deleted;
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
