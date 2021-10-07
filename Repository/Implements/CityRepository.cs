using Data;
using Data.Entities;
using Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Implements
{
  public class CityRepository : RepositoryBase<CityArea>, ICityRepository
  {
    public CityRepository(DBContext repositoryContext) : base(repositoryContext)
    {
    }

    public IEnumerable<CityArea> GetAllCity(bool trackChanges) =>
     FindAll(trackChanges)
       .OrderBy(c => c.AreaName)
       .ToList();

    public CityArea GetCityById(string cityId, bool trackChanges) =>
     FindByCondition(c=>c.AreaCode.Equals(cityId), trackChanges)
       .SingleOrDefault();
  }
}
