using CCL.Entities;

namespace CCL.Interfaces
{
    public interface IProductRepository
    {
        Task<Producto?> ObtenerPorId(int idProducto);
        Task<int> ActualizarCantidad(int idProducto, int cantidad);
        List<Producto> ObtenerInventario();
    }   
}
