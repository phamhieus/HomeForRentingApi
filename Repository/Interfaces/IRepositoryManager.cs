namespace Repository.Interfaces
{
  public interface IRepositoryManager
  {
    ICompanyRepository Company { get; }

    IEmployeeRepository Employee { get; }

    ICommentedUserRepository CommentedUser {get;}

    IMessageRepository Message { get; }

    IRoomRepository Room { get; }

    void Save();
  }
}
