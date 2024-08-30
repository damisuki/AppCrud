using Microsoft.AspNetCore.Mvc;

//contexto de base de datos
using AppCrud.Data;
using AppCrud.Models;
using Microsoft.EntityFrameworkCore;


namespace AppCrud.Controllers
{

    //inyeccion de dependencias
    public class EmpleadoController : Controller
    {

        private readonly AppDbContext _appDbConext;

        public EmpleadoController(AppDbContext appDbConext)
        {
            _appDbConext = appDbConext;
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<Empleado> lista = await _appDbConext.Empleados.ToListAsync();
            return View(lista);
        }

        [HttpGet]
        public IActionResult Nuevo()
        {
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Nuevo(Empleado empleado)
        {
            await _appDbConext.Empleados.AddAsync(empleado);
            await _appDbConext.SaveChangesAsync();
            return RedirectToAction(nameof(Lista)); 

        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            Empleado empleado = await _appDbConext.Empleados.FirstAsync(e => e.IdEmpleado == id);
            return View(empleado);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Empleado empleado)
        {
            _appDbConext.Empleados.Update(empleado);
            await _appDbConext.SaveChangesAsync();
            return RedirectToAction(nameof(Lista));

        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int id)
        {
            Empleado empleado = await _appDbConext.Empleados.FirstAsync(e => e.IdEmpleado == id);
            
            _appDbConext.Empleados.Remove(empleado);
            await _appDbConext.SaveChangesAsync();
            return RedirectToAction(nameof(Lista));
        }

    }
}
