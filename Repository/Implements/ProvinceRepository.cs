using Data;
using Data.Entities;

using Repository.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.Implements
{
  public class ProvinceRepository : RepositoryBase<ProvinceArea>, IProvinceRepository
  {
    public ProvinceRepository(DBContext repositoryContext) : base(repositoryContext)
    {

    }

    public ProvinceArea GetProvinceById(string provinceCode, bool trackChanges) =>
     FindByCondition(c => c.AreaCode.Equals(provinceCode), trackChanges)
       .SingleOrDefault();

    public IEnumerable<ProvinceArea> GetProvincesOfCity(string cityCode, bool trackChanges) =>
      FindByCondition(p=>p.ParentArea.Equals(cityCode), trackChanges)
      .OrderBy(c => c.AreaName)
      .ToList();
  }
}
