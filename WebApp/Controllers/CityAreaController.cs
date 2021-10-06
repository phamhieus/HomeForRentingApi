using Microsoft.AspNetCore.Mvc;

using Repository.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;


namespace AspImp.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CityAreaController : ControllerBase
  {
    private readonly IRepositoryManager _repository;

    public CityAreaController(IRepositoryManager repository)
    {
      _repository = repository;
    }

    [Microsoft.AspNetCore.Mvc.HttpGet("city")]
    public IActionResult GetAllCity()
    {
      return Ok(_repository.City.GetAllCity(false));
    }

    [Microsoft.AspNetCore.Mvc.HttpGet("province")]
    public IActionResult GetProvinces(string cityId)
    {
      return Ok(_repository.Province.GetProvincesOfCity(cityId.ToString(), false));
    }

    [Microsoft.AspNetCore.Mvc.HttpGet("award")]
    public IActionResult GetAwards(string provinceId)
    {
      return Ok(_repository.Award.GetAwardAreas(provinceId.ToString(), false));
    }
  }
}
