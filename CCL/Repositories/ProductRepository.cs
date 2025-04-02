using CCL.Entities;
using CCL.Interfaces;

namespace CCL.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly CclContext _context;

        public ProductRepository(CclContext context)
        {
            _context = context;
        }

        public async Task<Producto?> ObtenerPorId(int idProducto)
        {
            var productoDb = await _context.Productos.FindAsync(idProducto);
            return productoDb ?? null;
        }

        public async Task<int> ActualizarCantidad(int idProducto, int cantidad)
        {
            var producto = await _context.Productos.FindAsync(idProducto);
            if (producto != null)
            {
                producto.Cantidadproducto = cantidad;
                return await _context.SaveChangesAsync();
            }
            else return 0;
        }

        public List<Producto> ObtenerInventario()
        {
            return _context.Productos.ToList();
        }
    }
}
