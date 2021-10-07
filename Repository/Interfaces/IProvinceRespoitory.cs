using Data.Entities;
using System.Collections.Generic;

namespace Repository.Interfaces
{
  public interface IProvinceRepository
  {
    IEnumerable<ProvinceArea> GetProvincesOfCity(string cityCode, bool trackChanges);

    ProvinceArea GetProvinceById(string provinceCode, bool trackChanges);
  }
}
