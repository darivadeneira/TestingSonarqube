using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static GestionProductos.AppDBContext;

namespace GestionProductos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : Controller
    {
        private readonly AppDBContext _appDBcontext;
        private string? password;

        public ProductoController(AppDBContext appDBcontext)
        {
            _appDBcontext = appDBcontext;
        }

        [HttpGet]

        public async Task<IActionResult> GetProductos()
        {
            return Ok(await _appDBcontext.Productos.ToListAsync());
        }

        [HttpPost]

        public async Task<IActionResult> CreateProducto(Producto producto)
        {
            if (producto.Precio < 0) return BadRequest("El precio no puede ser negativo");
            _appDBcontext.Productos.Add(producto);
            await _appDBcontext.SaveChangesAsync();
            return Ok(producto);
        }

        [HttpPut]

        public async Task<IActionResult> UpdateProducto(Producto producto)
        {
            _appDBcontext.Entry(producto).State = EntityState.Modified;
            await _appDBcontext.SaveChangesAsync();
            return Ok(producto);
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteProducto(int id)
        {
            var producto = await _appDBcontext.Productos.FindAsync(id);
            if (producto == null) return NotFound();
            _appDBcontext.Productos.Remove(producto);
            await _appDBcontext.SaveChangesAsync();
            return Ok(producto);
        }
    }
}
