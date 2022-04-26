using MetricsManagerService.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;


namespace MetricsManagerService.Controllers
{
    [Route("api/temp")]
    [ApiController]
    public class TemperatureController : Controller
    {
        private ValuesHolder _holder;
        
        public TemperatureController(ValuesHolder holder)
        {
            _holder = holder;
        }

        [HttpGet("read")]
        public IActionResult Read([FromQuery] string from, string to)
        {
            DateTime timeFrom;
            DateTime timeTo;

            if (from == null && to == null)
            {
                return Ok(_holder.Get());
            } 
            else if (DateTime.TryParse(from, out timeFrom) && DateTime.TryParse(to, out timeTo) )
            {
                return Ok(_holder.Get(timeFrom, timeTo));
            }
            else
            {
                return BadRequest("Field Time has wrong format.");
            }
        }

        [HttpPost("create")]
        public IActionResult Create([FromQuery] string time, string temp)
        {
            DateTime date;
            if (DateTime.TryParse(time, out date))
            {
                if (!_holder.Values.Keys.Any(key => key.ToString().Contains(time)))
                { 
                    _holder.Add(temp, time);
                    return Ok();
                }
                return BadRequest("Time has been already added");
            }
            else
            {
                return BadRequest("Field Time has wrong format.");
            }
            
        }

        [HttpPut("update")]
        public IActionResult Update([FromQuery] string time,
        [FromQuery] string newValue)
        {
            DateTime date;

            if (DateTime.TryParse(time, out date))
            {
                if (_holder.Update(date, newValue))
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest("Field Time has wrong format.");
            }
        }

        [HttpDelete("delete")]
        public IActionResult Delete([FromQuery] string from, string to)
        {
            DateTime timeFrom;
            DateTime timeTo;

            if (DateTime.TryParse(from, out timeFrom) && DateTime.TryParse(to, out timeTo))
            {
                if (_holder.Delete(timeFrom, timeTo))
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest("Field Time has wrong format.");
            }
        }
    }
}
