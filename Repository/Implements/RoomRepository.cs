using Data;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Interfaces
{
  public class RoomRepository : RepositoryBase<Room>, IRoomRepository
  {
    public RoomRepository(DBContext repositoryContext) : base(repositoryContext)
    {
    }

    public Room GetRoom(Guid id, bool trackChanges) =>
      FindByCondition(c => c.Id.Equals(id), trackChanges)
      .SingleOrDefault();

    public IEnumerable<Room> GetAllRooms(bool trackChanges) =>
     FindAll(trackChanges)
     .OrderBy(c => c.CreateDate)
     .ToList();

    public void CreateRoom(Room room) => Create(room);

    public void DeleteRoom(Room room) => Delete(room);

    public void UpdateRoom(Room room) => Update(room);
  }
}
