using Data;
using Data.Entities;

using Repository.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.Implements
{
  class AwardRepository : RepositoryBase<AwardArea>, IAwardRepository
  {
    public AwardRepository(DBContext repositoryContext) : base(repositoryContext)
    {

    }

    public IEnumerable<AwardArea> GetAwardAreas(string provinceCode, bool trackChanges) =>
       FindByCondition(p => p.ProvinceCode.Equals(provinceCode), trackChanges)
      .OrderBy(c => c.AreaName)
      .ToList();
  }
}
