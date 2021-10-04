using Data.Common;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Entities
{
  public class UserImage : BaseEntity
  {
    public string FilePath { get; set; }

    public string FileName { get; set; }

    public string FileExtension { get; set; }

    public ImageType FileType { get; set; }

    [ForeignKey(nameof(User))]
    public string UserId { get; set; }

    [DefaultValue(true)]
    public bool IsActive { get; set; }
  }
}
