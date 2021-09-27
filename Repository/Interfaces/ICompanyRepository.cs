
using Data.Entities;

using System;
using System.Collections.Generic;

namespace Repository.Interfaces
{
  public interface ICompanyRepository
  {
    IEnumerable<Company> GetAllCompanies(bool trackChanges);
    Company GetCompany(Guid companyId, bool trackChanges);
  }
}
