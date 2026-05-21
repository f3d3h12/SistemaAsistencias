
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ISFDyT124.Models;
using ISFDyT124.Data;

public class MateriasController : Controller
{
    private readonly AsistenciaContext _context;

    public MateriasController(AsistenciaContext context)
    {
        _context = context;
    }

    // GET: MATERIAS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Materias.ToListAsync());
    }

    // GET: MATERIAS/Details/5
    public async Task<IActionResult> Details(int? ma_id)
    {
        if (ma_id == null)
        {
            return NotFound();
        }

        var materia = await _context.Materias
            .FirstOrDefaultAsync(m => m.ma_id == ma_id);
        if (materia == null)
        {
            return NotFound();
        }

        return View(materia);
    }

    // GET: MATERIAS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: MATERIAS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ma_id,ma_denominacion,ma_modalidad,ma_cant_modulos,CarrerasMaterias")] Materia materia)
    {
        if (ModelState.IsValid)
        {
            _context.Add(materia);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(materia);
    }

    // GET: MATERIAS/Edit/5
    public async Task<IActionResult> Edit(int? ma_id)
    {
        if (ma_id == null)
        {
            return NotFound();
        }

        var materia = await _context.Materias.FindAsync(ma_id);
        if (materia == null)
        {
            return NotFound();
        }
        return View(materia);
    }

    // POST: MATERIAS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? ma_id, [Bind("ma_id,ma_denominacion,ma_modalidad,ma_cant_modulos,CarrerasMaterias")] Materia materia)
    {
        if (ma_id != materia.ma_id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(materia);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MateriaExists(materia.ma_id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(materia);
    }

    // GET: MATERIAS/Delete/5
    public async Task<IActionResult> Delete(int? ma_id)
    {
        if (ma_id == null)
        {
            return NotFound();
        }

        var materia = await _context.Materias
            .FirstOrDefaultAsync(m => m.ma_id == ma_id);
        if (materia == null)
        {
            return NotFound();
        }

        return View(materia);
    }

    // POST: MATERIAS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? ma_id)
    {
        var materia = await _context.Materias.FindAsync(ma_id);
        if (materia != null)
        {
            _context.Materias.Remove(materia);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool MateriaExists(int? ma_id)
    {
        return _context.Materias.Any(e => e.ma_id == ma_id);
    }
}
