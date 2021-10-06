namespace Repository.Interfaces
{
  public interface IRepositoryManager
  {
    ICompanyRepository Company { get; }

    IEmployeeRepository Employee { get; }

    ICommentedUserRepository CommentedUser { get; }

    IMessageRepository Message { get; }

    IRoomRepository Room { get; }

    IRoomImageRepository RoomImage { get; }

    IUserImageRepository UserImage { get; }

    ICityRepository City { get; }

    IProvinceRepository Province { get; }
    IAwardRepository Award { get; }

    void Save();
  }
}
