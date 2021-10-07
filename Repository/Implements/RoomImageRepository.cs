using Data;
using Data.Common;
using Data.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Implements
{
  public class RoomImageRepository : RepositoryBase<RoomImage>, IRoomImageRepository
  {
    public RoomImageRepository(DBContext repositoryContext) : base(repositoryContext)
    {
    }

    public void CreateRoomImage(RoomImage roomImage) => Create(roomImage);

    public void UpdateRoomImage(RoomImage roomImage) => Update(roomImage);
    

    public void DeleteRoomImage(RoomImage roomImage) => Delete(roomImage);

    public IEnumerable<RoomImage> GetImagesOfRoom(Guid roomId, bool trackChanges) =>
      FindByCondition(c => c.RoomId.Equals(roomId), trackChanges);

    public RoomImage GetImageById(Guid imageId, bool trackChanges) =>
      FindByCondition(c => c.Id.Equals(imageId), trackChanges)
      .SingleOrDefault();

    public IEnumerable<RoomImage> GetDescriptionImagesOfRoom(Guid roomId, bool trackChanges) =>
      FindByCondition(c =>
          c.RoomId.Equals(roomId) 
          && c.FileType == ImageType.DescriptionImage 
          && c.IsActive, 
        trackChanges);

    public RoomImage GetThumbnailImagesOfRoom(Guid roomId, bool trackChanges) =>
      FindByCondition(c => 
          c.RoomId.Equals(roomId) 
          && c.FileType == ImageType.Thumbnail
          && c.IsActive, 
        trackChanges)
      .SingleOrDefault();

  }
}
