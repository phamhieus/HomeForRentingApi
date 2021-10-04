using Data.Entities;

using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interfaces
{
  public interface IUserImageRepository
  {
    UserImage GetUserThumbnail(string userId, bool trackChanges);

    UserImage GetImageById(Guid imageId, bool trackChanges);

    void CreateUserImage(UserImage userImage);
    
    void UpdateUserImage(UserImage userImage);

    void DeleteUserImage(UserImage userImage);

  }
}
