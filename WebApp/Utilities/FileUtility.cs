using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace WebApp.Utilities
{
    public static class FileUtility
    {
        public static async Task UploadFile(IFormFile file, IWebHostEnvironment environment)
        {
            var baseDirectory = Path.Combine(environment.WebRootPath, "Images");
            if (!Directory.Exists(baseDirectory))
            {
                Directory.CreateDirectory(baseDirectory);
            }
            var filePath = Path.Combine(baseDirectory, file.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
        }
    }
}
