using Microsoft.AspNetCore.Mvc;

namespace MvcNetCoreUtilidades.Controllers
{
    public class UploadFilesController : Controller
    {
        private IWebHostEnvironment hostEnvironment;

        public UploadFilesController (IWebHostEnvironment hostEnvironment)
        {
            this.hostEnvironment = hostEnvironment;
        }

        public IActionResult SubirFile()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SubirFile(IFormFile fichero)
        {
            //Necesitamos la ruta hacia la carpeta wwwroot
            string rootFolder = this.hostEnvironment.WebRootPath;
            string fileName = fichero.FileName;
            //Cuando pensamos en ficheros y sus rutas, estamos pensando en algo parecido a esto:
            //C:\misficheros\1.txt
            //Net Core no es windows y esta ruta es de windows, y las rutas de linux o MACOS pueden ser distintas.
            //Debemos crear rutas con herramientas de Net Core: Path
            string path = Path.Combine(rootFolder, "uploads", fileName);
            //Para subir ficheros utilizamos Stream
            using(Stream stream = new FileStream(path, FileMode.Create))
            {
                await fichero.CopyToAsync(stream);
            }
            ViewData["MENSAJE"] = "Fichero subido a " + path;
            ViewData["FILENAME"] = fileName;
            return View();
        }
    }
}
