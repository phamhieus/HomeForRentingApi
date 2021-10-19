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

    IEnumerable<CommentedUser> GetAllCommentsOfUser(string userId, bool trackChanges);

    IEnumerable<CommentedUser> GetAllCommentsOfUser(string userId, string evaluatedUser, bool trackChanges);

    void CreateCommentedUser(CommentedUser commentedUser);

    void DeleteCommentedUser(CommentedUser commentedUser);

    void UpdateCommentedUser(CommentedUser commentedUser);
  }
}
