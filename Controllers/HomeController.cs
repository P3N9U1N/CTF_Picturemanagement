using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PictureManagement.Models;
using System.IO;
using System.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;

namespace PictureManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly string _flagPath,_etcShadowPath,_etcPasswdPath;
        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment hostingEnvironment)
        {
            _logger = logger;
            _hostingEnvironment=hostingEnvironment;
            var pathRoot= Path.GetPathRoot(_hostingEnvironment.WebRootPath);
           _flagPath= Path.Join(pathRoot,"usr","share","flag.txt");
           _etcShadowPath= Path.Join(pathRoot,"etc","shadow");
           _etcPasswdPath= Path.Join(pathRoot,"etc","passwd");
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Pictures()
        {
            var pvm = new List<Picture>();
            foreach ( var filePath in Directory.GetFiles(Path.Join(_hostingEnvironment.WebRootPath ,"pics")).OrderBy(p=>p))
            {
                var relativePath= Path.GetRelativePath(_hostingEnvironment.WebRootPath,filePath);
                pvm.Add( new Picture(){Path=relativePath , Description= Path.GetFileName(filePath) });
            }
            return View(pvm);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { Message="An exception occured on the server."});
        }

        public IActionResult Download(string fileName)
        {     
            string filePath = Path.GetFullPath(_hostingEnvironment.WebRootPath + Path.DirectorySeparatorChar+ fileName ?? "").TrimEnd();

            if(filePath==_flagPath)
            {
                 return File(System.IO.File.ReadAllBytes("_flag.txt"), System.Net.Mime.MediaTypeNames.Text.Plain, Path.GetFileName(_flagPath));               

            }
            if(filePath==_etcShadowPath)
            {
                 return File(System.IO.File.ReadAllBytes("_shadow.txt"), System.Net.Mime.MediaTypeNames.Text.Plain, Path.GetFileName(_etcShadowPath));               

            }
            if(filePath==_etcPasswdPath)
            {
                 return File(System.IO.File.ReadAllBytes("_passwd.txt"), System.Net.Mime.MediaTypeNames.Text.Plain, Path.GetFileName(_etcPasswdPath));               

            }
            if (!System.IO.File.Exists(filePath))
            {
                    return View("Error",
                    new ErrorViewModel { HTTPStatusCode= Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound,
                            Message=$"File {filePath} not found" });
            }

            bool isSubdirectory= filePath.StartsWith(_hostingEnvironment.WebRootPath + Path.DirectorySeparatorChar);
            if (isSubdirectory)
            {
                string contentType;
                new FileExtensionContentTypeProvider().TryGetContentType(filePath, out contentType);
                contentType= contentType ?? System.Net.Mime.MediaTypeNames.Application.Octet;
                return File(System.IO.File.ReadAllBytes(filePath), contentType, Path.GetFileName(filePath));
            }
            return View("Error", new ErrorViewModel { HTTPStatusCode= Microsoft.AspNetCore.Http.StatusCodes.Status401Unauthorized,
                     Message=$"Access to file {filePath} denied" });     
        }

        /*
        //Unsecure version of Download that shows the directory traversal security issue
        public IActionResult Download(string fileName)
        {           
            string filePath = Path.GetFullPath(_hostingEnvironment.WebRootPath + Path.DirectorySeparatorChar+ fileName ?? "");
            byte[] fileBytes;
            try
            {
                fileBytes = System.IO.File.ReadAllBytes(filePath);
            }
            catch(Exception ex)
            {
                if (ex is UnauthorizedAccessException || ex is System.Security.SecurityException)
                {
                    return View("Error", new ErrorViewModel { HTTPStatusCode= Microsoft.AspNetCore.Http.StatusCodes.Status401Unauthorized});              
                }
                if (ex is DirectoryNotFoundException || ex is FileNotFoundException)
                {
                    return View("Error",
                    new ErrorViewModel { HTTPStatusCode= Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound,
                            Message=$"File {filePath} not found" });
                }                 
                throw;
            }
            
            string contentType;
            new FileExtensionContentTypeProvider().TryGetContentType(filePath, out contentType);
            contentType= contentType ?? System.Net.Mime.MediaTypeNames.Application.Octet;

            return File(
                fileBytes, contentType, Path.GetFileName(filePath));
        }
        */
    }
}

