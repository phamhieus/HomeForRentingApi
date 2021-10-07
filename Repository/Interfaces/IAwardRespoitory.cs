using Data.Entities;

using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interfaces
{
  public interface IAwardRepository
  {
    IEnumerable<AwardArea> GetAwardAreas(string provinceCode, bool trackChanges);

    AwardArea GetAwardAreaById(string awardCode, bool trackChanges);
  }
}
