using Data.Common;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Data.Entities
{
  public class RoomImage : BaseEntity
  {
    public string FilePath { get; set; }

    public string FileName { get; set; }

    public string FileExtension { get; set; }

    public ImageType FileType { get; set; }

    public Guid RoomId { get; set; }

    [DefaultValue(true)]
    public bool IsActive { get; set; }
  }
}
