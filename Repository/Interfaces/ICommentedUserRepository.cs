using Data.Entities;

using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interfaces
{
  public interface ICommentedUserRepository
  {
    CommentedUser GetCommentedUser(Guid id, bool trackChanges);

    IEnumerable<CommentedUser> GetAllCommentedUsers(bool trackChanges);

    void CreateCommentedUser(CommentedUser commentedUser);

    void DeleteCommentedUser(CommentedUser commentedUser);

    void UpdateCommentedUser(CommentedUser commentedUser);
  }
}
