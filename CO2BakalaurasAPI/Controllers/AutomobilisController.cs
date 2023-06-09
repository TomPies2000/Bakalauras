﻿using CO2BakalaurasAPI.Data;
using CO2BakalaurasAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CO2BakalaurasAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AutomobilisController : ControllerBase
    {
        private readonly CO2DatabaseContext _dbContext;

        public AutomobilisController(CO2DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("CreateCar")]
        public IActionResult CreateCar([FromBody] AutomobilisRequest request)
        {
            Automobilis automobilis = new()
            {
                SANAUDU_ID = request.SANAUDU_ID,
                AUTOMOBILIO_MARKE = request.AUTOMOBILIO_MARKE,
                RIDA = request.RIDA,
                SVORIS = request.SVORIS
            };
            try
            {
                _dbContext.AUTOMOBILIS.Add(automobilis);
                _dbContext.SaveChanges();
                return Ok();
            }
            catch
            {
                return StatusCode(500);
            }
        }


        [HttpGet("GetCarByUsageId/{ID}")]
        public IActionResult GetCarByUsageId([FromRoute] int ID)
        {
            try
            {
                var automobilis = _dbContext.AUTOMOBILIS.FirstOrDefault(x => x.SANAUDU_ID == ID);
                if (automobilis == null)
                {
                    return StatusCode(404);
                }
                return Ok(automobilis);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("DeleteCarByUsageID/{ID}")]
        public IActionResult DeleteCarByUsageID([FromRoute] int ID)
        {
            try
            {
                var automobilis = _dbContext.AUTOMOBILIS.FirstOrDefault(x => x.SANAUDU_ID == ID);
                if (automobilis == null) return StatusCode(404);
                _dbContext.Entry(automobilis).State = EntityState.Deleted;
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
