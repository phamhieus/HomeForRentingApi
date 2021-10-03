using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Common
{
  public class Constant
  {
    public const string ROOM_DIRECTORY = "room";
    public const string IMG_DIRECTORY = "images";

    private const string _baseUrl = "http:localhost:5000/";
    private static readonly string[] vietnameseSigns = new string[]
    {
      "aAeEoOuUiIdDyY", "áàạảãâấầậẩẫăắằặẳẵ", "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ", "éèẹẻẽêếềệểễ",
      "ÉÈẸẺẼÊẾỀỆỂỄ", "óòọỏõôốồộổỗơớờợởỡ", "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ", "úùụủũưứừựửữ",
      "ÚÙỤỦŨƯỨỪỰỬỮ", "íìịỉĩ", "ÍÌỊỈĨ", "đ", "Đ", "ýỳỵỷỹ", "ÝỲỴỶỸ"
    };

    public static string GetImageRoomPath(string fileName) =>
      $"{IMG_DIRECTORY}/{ROOM_DIRECTORY}/{fileName}";

    public static string GetFileUrl(string filePath) =>
      $"{_baseUrl}{filePath}";

    public static string RemoveSign4VietnameseString(string str)
    {
      for (int i = 1; i < vietnameseSigns.Length; i++)
      {
        for (int j = 0; j < vietnameseSigns[i].Length; j++)
          str = str.Replace(vietnameseSigns[i][j], vietnameseSigns[0][i - 1]);
      }
      return str;
    }
  }
}
