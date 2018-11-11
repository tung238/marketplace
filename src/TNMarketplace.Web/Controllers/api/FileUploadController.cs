using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using TNMarketplace.Web.Models;

namespace TNMarketplace.Web.Controllers.api
{
    [Route("api/[controller]")]
  
    public class FileUploadController : BaseController
    {
        private readonly IHostingEnvironment _environment;

        public FileUploadController(IHostingEnvironment environment)
        {
            _environment = environment;

        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null)
            {
                var result = new FileUploadResult() { Name = "", Status = "fail"};
                return Ok( result);
            }
            using (Image<Rgba32> image = Image.Load(file.OpenReadStream()))
            {
                var path = Path.Combine(_environment.ContentRootPath, "upload/listing");
                var name = string.Format("{0}.{1}", Path.GetRandomFileName(), "jpg");
                image.Mutate(x => x
                        .Resize(image.Width / 2, image.Height / 2));
                Directory.CreateDirectory(path);
                image.Save(Path.Combine(path, name)); // Automatic encoder selected based on extension.
                var fileUploadResult = new FileUploadResult() { Name = name, Status = "done", Url = string.Format("{0}://{1}/upload/listing/{2}", HttpContext.Request.Scheme, HttpContext.Request.Host.ToString(), name) };
                //var result = new { Success = "true", Files = new [{ status="done",  }] };
                return Ok(fileUploadResult);
            }
          
        }
    }
}