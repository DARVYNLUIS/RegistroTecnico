using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RegistroTecnico.DAL;
using System.Linq;
using System.Threading.Tasks;

public class PrestamosController : Controller
{
    private readonly Contexto _context;

    public PrestamosController(Contexto context)
    {
        _context = context;
    }

    // Método para mostrar una lista de préstamos en la vista
    public async Task<IActionResult> Index()
    {
        var prestamos = await _context.Prestamos.Include(p => p.Cliente).ToListAsync();
        return View(prestamos);
    }

  
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Prestamos prestamo)
    {
        if (!ModelState.IsValid)
            return View(prestamo);

        prestamo.Balance = prestamo.Monto;
        _context.Prestamos.Add(prestamo);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }


    public async Task<IActionResult> Details(int id)
    {
        var prestamo = await _context.Prestamos
            .Include(p => p.PrestamosDetalles)
            .FirstOrDefaultAsync(p => p.PrestamoId == id);

        if (prestamo == null)
            return NotFound();

        return View(prestamo);
    }
}