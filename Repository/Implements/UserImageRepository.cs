using Data;
using Data.Entities;

using Repository.Interfaces;

using System;
using System.Linq;

namespace Repository.Implements
{
  public class UserImageRepository : RepositoryBase<UserImage>, IUserImageRepository
  {
    public UserImageRepository(DBContext repositoryContext) : base(repositoryContext)
    {
    }

    public void CreateUserImage(UserImage userImage) => Create(userImage);

    public void DeleteUserImage(UserImage userImage) => Delete(userImage);

    public void UpdateUserImage(UserImage userImage) => Update(userImage);

    public UserImage GetImageById(Guid imageId, bool trackChanges) =>
      FindByCondition(c => c.Id.Equals(imageId), trackChanges)
      .SingleOrDefault();

    public UserImage GetUserThumbnail(string userId, bool trackChanges) =>
     FindByCondition(c => 
        c.UserId.Equals(userId) 
        && c.IsActive,
       trackChanges)
     .SingleOrDefault();
  }
}
