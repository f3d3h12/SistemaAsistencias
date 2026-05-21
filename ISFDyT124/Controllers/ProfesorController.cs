using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ISFDyT124.Controllers
{
    public class ProfesorController : Controller
    {
        rivate readonly InstitutoDbContext _context;

        // Constantes del sistema para mapear con los IDs de la tabla ROLES
        private const int ROL_DOCENTE_ID = 2; // ID del rol Docente en la base de datos
        private const int ROL_ALUMNO_ID = 3;  // ID del rol Alumno en la base de datos

        public ProfesorController(InstitutoDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
