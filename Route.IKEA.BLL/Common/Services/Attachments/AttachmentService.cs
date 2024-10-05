using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.IKEA.BLL.Common.Services.Attachments
{
    public class AttachmentService : IAttachmentService
    {
        private List<string> _allowedExtensions = new() { ".png",".jpg", ".jpeg" };
        private const int _allowedMaxSize = 2_079_125;


        public async Task  <string?> UploadFileAsync(IFormFile file, string folderName)
        {
            var extension =  Path.GetExtension(file.FileName); // "maged.png"

            if(! _allowedExtensions.Contains(extension) )
                return null;

            if(file.Length > _allowedMaxSize )
                return null;

            //var folderPath = $"{Directory.GetCurrentDirectory()}\\wwwroot\\files\\{folderName}";

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", folderName);

            if(!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var fileName = $"{Guid.NewGuid()} {extension}"; // must be uniqe

            var filePath = Path.Combine(folderPath, fileName); // file location placed

            // Sreaming : Data per time 
            using  var fileStream = new FileStream(filePath, FileMode.Create);

            //  using var fileStream = File.Create(filePath);

            await file.CopyToAsync(fileStream);

            return  fileName;
            

        }

        public   bool DeleteFile(string filePath)
        {
           if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return true;
            }
           return false;
         
        }

       
    }
}
