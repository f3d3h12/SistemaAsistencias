using Microsoft.AspNetCore.Mvc;

namespace ISFDyT124.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UsuariosABM()
        {
            return View();
        }

        public IActionResult UsuarioAgregar()
        {
            return View();
        }
    }
}