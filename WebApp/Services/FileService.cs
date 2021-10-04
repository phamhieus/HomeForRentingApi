using Contracts;

using Data.Common;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

using System;
using System.IO;

namespace AspImp.Services
{
  public class FileService
  {
    public FileService()
    {
    }

    public string WriteImageFile(string fileName, string fileFolder, IFormFile file)
    {
      try
      {
        string imageFolderPath = GetDirectoryPath(Constant.IMG_DIRECTORY, fileFolder);
        string filePath = $"{imageFolderPath}/{fileName}";

        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
          file.CopyTo(fileStream);
        }

        return filePath;
      }
      catch 
      {
        throw;
      }
    }

    public void DeleteFile(string rootFolder, string folder, string fileName)
    {
      string imageFolderPath = GetDirectoryPath(rootFolder, folder);
      string fullPath = $"{imageFolderPath}/{fileName}";

      System.IO.File.Delete(fullPath);
    }

    private string GetDirectoryPath(string rootFolder, string folder)
    {
      try
      {
        string imageFolderPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot", rootFolder);
        
        string destinationFolderPath = Path.Combine(
          Directory.GetCurrentDirectory(), 
          $"{imageFolderPath}/{folder}")
        .Replace("\\", "/");

        if (!Directory.Exists(imageFolderPath))
        {
          Directory.CreateDirectory(imageFolderPath);
        }

        if (!Directory.Exists(destinationFolderPath))
        {
          Directory.CreateDirectory(destinationFolderPath);
        }

        return destinationFolderPath;
      }
      catch 
      {
        throw;
      }
    }
  }
}
