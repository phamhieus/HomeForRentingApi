﻿using Data;
using Data.Entities;
using Repository.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Implements
{
  public class CommentedUserRepository : RepositoryBase<CommentedUser>, ICommentedUserRepository
  {
    public CommentedUserRepository(DBContext repositoryContext) : base(repositoryContext)
    {
    }

    public CommentedUser GetCommentedUser(Guid id, bool trackChanges) =>
      FindByCondition(c => c.Id.Equals(id), trackChanges)
        .SingleOrDefault();

    public IEnumerable<CommentedUser> GetAllCommentedUsers(bool trackChanges) =>
     FindAll(trackChanges)
     .OrderBy(c => c.CreateDate)
     .ToList();

    public IEnumerable<CommentedUser> GetAllCommentsOfUser(string userId, bool trackChanges) =>
      FindByCondition(cmt=>cmt.EvaluatedUser.Equals(userId) , trackChanges)
       .OrderBy(c => c.CreateDate)
       .ToList();

    public IEnumerable<CommentedUser> GetAllCommentsOfUser(string userId, string evaluatedUser, bool trackChanges)=>
      FindByCondition(cmt => 
        cmt.EvaluatedUser.Equals(evaluatedUser) 
        && ( cmt.CreatedBy.Equals(userId) 
          || cmt.UpdatedBy.Equals(userId)), 
        trackChanges)
       .OrderBy(c => c.CreateDate)
       .ToList();

    public void CreateCommentedUser(CommentedUser commentedUser) => Create(commentedUser);

    public void DeleteCommentedUser(CommentedUser commentedUser) => Delete(commentedUser);

    public void UpdateCommentedUser(CommentedUser commentedUser) => Update(commentedUser);
  }
}
