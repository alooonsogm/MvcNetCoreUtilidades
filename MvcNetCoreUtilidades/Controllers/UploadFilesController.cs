using Microsoft.AspNetCore.Mvc;
using MvcNetCoreUtilidades.Helpers;

namespace MvcNetCoreUtilidades.Controllers
{
    public class UploadFilesController : Controller
    {
        private HelperPathProvider helper;

        public UploadFilesController(HelperPathProvider helper)
        {
            this.helper = helper;
        }

        public IActionResult SubirFile()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SubirFile(IFormFile fichero)
        {
            string fileName = fichero.FileName;
            string path = this.helper.MapPath(fileName, Folders.Images);
            //Para subir ficheros utilizamos Stream
            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                await fichero.CopyToAsync(stream);
            }
            ViewData["MENSAJE"] = "Fichero subido a " + path;
            ViewData["PATH"] = this.helper.MapUrlPath(fileName, Folders.Images);
            return View();
        }
    }
}
