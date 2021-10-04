using Data;
using Repository.Interfaces;

namespace Repository.Implements
{
  public class RepositoryManager : IRepositoryManager
  {
    private DBContext _context;
    
    private ICompanyRepository _companyRepository;
    private IUserImageRepository _userImageRepository;
    private ICommentedUserRepository _commentedUserRepository;
    private IRoomImageRepository _roomImageRepository;
    private IEmployeeRepository _employeeRepository;
    private IMessageRepository _messageRepository;
    private IRoomRepository _roomRepository;

    public RepositoryManager(DBContext context)
    {
      _context = context;
    }

    public ICompanyRepository Company
    {
      get
      {
        if (_companyRepository == null)
        {
          _companyRepository = new CompanyRepository(_context);
        }

        return _companyRepository;
      }
    }
    
    public IEmployeeRepository Employee
    {
      get
      {
        if (_employeeRepository == null)
        {
          _employeeRepository = new EmployeeRepository(_context);
        }

        return _employeeRepository;
      }
    }

    public IMessageRepository Message
    {
      get
      {
        if (_messageRepository == null)
        {
          _messageRepository = new MessageRepository(_context);
        }

        return _messageRepository;
      }
    }

    public IRoomRepository Room
    {
      get
      {
        if (_roomRepository == null)
        {
          _roomRepository = new RoomRepository(_context);
        }

        return _roomRepository;
      }
    }

    public ICommentedUserRepository CommentedUser
    {
      get
      {
        if (_commentedUserRepository == null)
        {
          _commentedUserRepository = new CommentedUserRepository(_context);
        }

        return _commentedUserRepository;
      }
    }

    public IRoomImageRepository RoomImage
    {
      get
      {
        if (_roomImageRepository == null)
        {
          _roomImageRepository = new RoomImageRepository(_context);
        }

        return _roomImageRepository;
      }
    }

    public IUserImageRepository UserImage
    {
      get
      {
        if (_userImageRepository == null)
        {
          _userImageRepository = new UserImageRepository(_context);
        }

        return _userImageRepository;
      }
    }

    public void Save() => _context.SaveChanges();
  }

}
