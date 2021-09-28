using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspImp.Controllers
{
  [Route("weatherforecast")]
  [ApiController]
  public class WeatherForecastController : ControllerBase
  {
    // GET: api/<HomeController>
    [HttpGet]
    public IActionResult Get()
    {
      return Ok("Home controller");
    }
  }
}
