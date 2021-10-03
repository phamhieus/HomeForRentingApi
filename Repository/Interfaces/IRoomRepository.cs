using Data.Entities;
using System;
using System.Collections.Generic;

namespace Repository.Interfaces
{
  public interface IRoomRepository
  {
    Room GetRoom(Guid id, bool trackChanges);

    IEnumerable<Room> GetAllRooms(bool trackChanges);

    void CreateRoom(Room room);

    void DeleteRoom(Room room);

    void UpdateRoom(Room room);
  }
}
