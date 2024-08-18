using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace MVCPelicula.Controllers
{
    public class HelloWorldController : Controller
    {
        //GET :/HelloWorld/
        public ActionResult Index()
        {
            return View();
        }

        //GET :/HelloWorld/Welcome/
        public ActionResult Welcome(string nombre, string apellido, int numVeces = 1)
        {
            ViewData["nombre"] = "Hola " + nombre + " " + apellido;
            ViewData["numVeces"] = numVeces;

            return View();
        }
    }
}
