using GoogleMaps.LocationServices;

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


    [Microsoft.AspNetCore.Mvc.HttpGet("coordinates")]
    public IActionResult GetCoordinates(string awardId, string provinceId, string cityId)
    {
      var award = _repository.Award.GetAwardAreaById(awardId, false);
      var province = _repository.Province.GetProvinceById(provinceId, false);
      var city = _repository.City.GetCityById(cityId, false);

      if(award == null || province == null || city == null)
      {
        return NotFound("Address not found");
      }

      var address = $"{award.AreaName}, {province.AreaName}, {city.AreaName}";

      var locationService = new GoogleLocationService("AIzaSyDKQazlHfJNQB4b2WGDi3l7ZdmalItmtJ8");

      var point = locationService.GetLatLongFromAddress(address);

      var latitude = point.Latitude;
      var longitude = point.Longitude;

      return Ok(new { latitude, longitude });
    }

  }
}
