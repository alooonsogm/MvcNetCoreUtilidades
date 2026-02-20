using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace MvcNetCoreUtilidades.Controllers
{
    public class CachingController : Controller
    {
        private IMemoryCache memoryCache;

        public CachingController(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)]
        public IActionResult MemoriaDistribuida()
        {
            string fecha = DateTime.Now.ToLongDateString() + " -- " + DateTime.Now.ToLongTimeString();
            ViewData["FECHA"] = fecha;
            return View();
        }

        public IActionResult MemoriaPersonalizada(int? tiempo)
        {
            if(tiempo == null)
            {
                tiempo = 60;
            }
            string fecha = DateTime.Now.ToLongDateString() + " -- " + DateTime.Now.ToLongTimeString();
            //Como esto es manual, debemos preguntar si existe algo
            if(this.memoryCache.Get("FECHA") == null)
            {
                //No existe cache todavia
                //Creamos el objeto entry options con el tiempo
                MemoryCacheEntryOptions options = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(tiempo.Value));
                this.memoryCache.Set("FECHA", fecha, options);
                ViewData["MENSAJE"] = "Fecha almacenada correctamente";
                ViewData["FECHA"] = this.memoryCache.Get("FECHA");
            }
            else
            {
                //Existe cache y lo recuperamos
                fecha = this.memoryCache.Get<string>("FECHA");
                ViewData["MENSAJE"] = "Fecha recuperada correctamente";
                ViewData["FECHA"] = fecha;
            }
            return View();
        }
    }
}
