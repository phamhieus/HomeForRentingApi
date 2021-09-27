
using Data;
using Data.Entities;

using Repository.Interfaces;

namespace Repository.Implements
{
  public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
  {
    public EmployeeRepository(DBContext repositoryContext) : base(repositoryContext)
    {
    }
  }
}
