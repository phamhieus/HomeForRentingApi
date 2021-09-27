using Data.Entities;

using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interfaces
{
  public interface IMessageRepository
  {
    Message GetMessage(Guid id, bool trackChanges);

    IEnumerable<Message> GetAllMessages(bool trackChanges);

    void CreateMessage(Message message);

    void DeleteMessage(Message message);

    void UpdateMessage(Message message);
  }
}
