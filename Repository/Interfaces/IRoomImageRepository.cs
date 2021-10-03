using System;
using System.Collections.Generic;
using Data.Entities;

namespace Repository.Interfaces
{
  public interface IRoomImageRepository
  {
    IEnumerable<RoomImage> GetImagesOfRoom(Guid roomId, bool trackChanges);

    RoomImage GetImageById(Guid imageId, bool trackChanges);

    IEnumerable<RoomImage> GetDescriptionImagesOfRoom(Guid roomId, bool trackChanges);

    RoomImage GetThumbnailImagesOfRoom(Guid roomId, bool trackChanges);

    void CreateRoomImage(RoomImage roomImage);

    void UpdateRoomImage(RoomImage roomImage);

    void DeleteRoomImage(RoomImage roomImage);
  }
}
