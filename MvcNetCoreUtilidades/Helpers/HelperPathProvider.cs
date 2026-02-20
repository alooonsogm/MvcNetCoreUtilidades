namespace MvcNetCoreUtilidades.Helpers
{
    //Enumeracion con las carpetas que deseemos subir ficheros dentro de wwwroot
    public enum Folders {Images, Uploads, Facturas, Temporal}
    public class HelperPathProvider
    {
        private IWebHostEnvironment hostEnvironment;
        private IHttpContextAccessor httpContextAccessor;

        public HelperPathProvider(IWebHostEnvironment hostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            this.hostEnvironment = hostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
        }
        
        //Tendremos un metodo que se encargara de resolver la ruta como string cuando recibamos el fichero y la carpeta.
        public string MapPath(string fileName, Folders folder)
        {
            string carpeta = "";
            if (folder == Folders.Images)
            {
                carpeta = "images";
            }
            else if (folder == Folders.Uploads)
            {
                carpeta = "uploads";
            }
            else if (folder == Folders.Facturas)
            {
                carpeta = "facturas";
            }
            else if (folder == Folders.Temporal)
            {
                carpeta = "temp";
            }
            string rootPath = this.hostEnvironment.WebRootPath;
            string path = Path.Combine(rootPath, carpeta, fileName);
            return path;
        }

        public string MapUrlPath(string fileName, Folders folder)
        {
            string carpeta = "";
            if (folder == Folders.Images)
            {
                carpeta = "images";
            }
            else if (folder == Folders.Uploads)
            {
                carpeta = "uploads";
            }
            else if (folder == Folders.Facturas)
            {
                carpeta = "facturas";
            }
            else if (folder == Folders.Temporal)
            {
                carpeta = "temp";
            }
            var request = this.httpContextAccessor.HttpContext.Request;
            string baseUrl = request.Scheme + "://" + request.Host + request.PathBase;
            return baseUrl + "/" + carpeta + "/" + fileName;
        }
    }
}
