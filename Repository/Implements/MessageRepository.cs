using Data;
using Data.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Implements
{
  public class MessageRepository : RepositoryBase<Message>, IMessageRepository
  {
    public MessageRepository(DBContext repositoryContext) : base(repositoryContext)
    {
    }

    public Message GetMessage(Guid id, bool trackChanges) =>
      FindByCondition(c => c.Id.Equals(id), trackChanges)
      .SingleOrDefault();

    public IEnumerable<Message> GetAllMessages(bool trackChanges) =>
     FindAll(trackChanges)
     .OrderBy(c => c.SentDate)
     .ToList();

    public void CreateMessage(Message message) => Create(message);

    public void DeleteMessage(Message message) => Delete(message);

    public void UpdateMessage(Message message) => Update(message);
  }
}
