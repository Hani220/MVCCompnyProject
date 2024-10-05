using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.IKEA.BLL.Common.Services.Attachments
{
    public interface IAttachmentService
    {
        public Task <string?> UploadFileAsync(IFormFile file, string folderName);

        public  bool DeleteFile(string filePath);


    }
}
