using KEShop_Api_N_Tier_Art.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace KEShop_Api_N_Tier_Art.BLL.Services.Classes
{
    public class FileService : IFileService
    {
        public void Delete(string fileName)
        {

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");


            var filePath = Path.Combine(folderPath, fileName);


            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        public async Task<string> UploadAsync(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);


                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");


                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }


                var filePath = Path.Combine(folderPath, fileName);

                using (var stream = File.Create(filePath))
                {
                    await file.CopyToAsync(stream);
                }

                return fileName;
            }

            throw new Exception("File is null or empty");
        }
        public async Task<List<string>> UploadManyAsync(List<IFormFile> files)
        {
            var fileNames = new List<string>();
            foreach (var file in files)
            {
                if (file != null && file.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);


                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");




                    using (var stream = File.Create(filePath))
                    {
                        await file.CopyToAsync(stream);
                    }
                    fileNames.Add(fileName);


                }

            }
            return fileNames;
        }
    }
}