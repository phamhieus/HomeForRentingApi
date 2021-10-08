using Data;
using Data.Common;
using Data.DTO.Requests;
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
     FindByCondition(r=>r.Status != Data.Common.RoomStatus.SoltOut, trackChanges)
     .OrderByDescending(c => c.CreateDate)
     .ToList();

    public IEnumerable<Room> GetMyRoom(string userId, bool trackChanges) =>
     FindByCondition(r => r.CreatedBy == userId, trackChanges)
     .OrderByDescending(c => c.CreateDate)
     .ToList();

    public void CreateRoom(Room room) => Create(room);

    public void DeleteRoom(Room room) => Delete(room);

    public void UpdateRoom(Room room) => Update(room);

    public IEnumerable<Room> GetRoomsByKey(string key, bool trackChanges) =>
      FindByCondition(room => room.ShortName.ToLower().Contains(key)
        || room.Address.ToLower().Contains(key)
        || room.Description.ToLower().Contains(key), 
      trackChanges);
    

    public IEnumerable<Room> GetRoomsRequest(SearchingRoomRequest searchingRoomRequest, bool trackChanges) =>
      FindByCondition(room =>
          (string.IsNullOrEmpty(searchingRoomRequest.CityId) || room.City == searchingRoomRequest.CityId)
          && (string.IsNullOrEmpty(searchingRoomRequest.ProvinceId) || room.Province == searchingRoomRequest.ProvinceId)
          && (string.IsNullOrEmpty(searchingRoomRequest.AwardId) || room.Street == searchingRoomRequest.AwardId)
          && (searchingRoomRequest.MaxCost == null || room.Cost <= searchingRoomRequest.MaxCost)
          && (searchingRoomRequest.MinCost == null || room.Cost >= searchingRoomRequest.MinCost)
          && (searchingRoomRequest.RoomType == null || room.RoomType == searchingRoomRequest.RoomType)
          && (searchingRoomRequest.Type == null || room.Type == searchingRoomRequest.Type)
          && room.Status == RoomStatus.Empty,
        false)
      .ToList();


  }
}
