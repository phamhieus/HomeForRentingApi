using Data.Entities;

using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interfaces
{
  public interface ICityRepository
  {
    IEnumerable<CityArea> GetAllCity(bool trackChanges);
  }
}
