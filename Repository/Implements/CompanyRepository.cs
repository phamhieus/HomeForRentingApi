using Data;
using Data.Entities;

using Repository.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Implements
{
  public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
  {
    public CompanyRepository(DBContext repositoryContext) : base(repositoryContext)
    {

    }

    public Company GetCompany(Guid companyId, bool trackChanges) =>
      FindByCondition(c => c.Id.Equals(companyId), trackChanges)
      .SingleOrDefault();

    public IEnumerable<Company> GetAllCompanies(bool trackChanges) =>
     FindAll(trackChanges)
     .OrderBy(c => c.Name)
     .ToList();
  }
}
