using InventariosApi.Entity;
using System.Threading.Tasks;

namespace InventariosApi.Data
{
    public interface IInventarioService
    {
        Task<string> AddInventario(Inventarios Invent);
        Task<Inventarios> BuscarInventario(int id);
        Task<bool> ActualizarInventario(Inventarios invent);
        Task DeleteInventario(Inventarios invent);
    }
}
