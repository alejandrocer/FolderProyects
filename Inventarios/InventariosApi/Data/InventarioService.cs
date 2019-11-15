using InventariosApi.Entity;
using InventariosApi.Helper;
using InventariosApi.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace InventariosApi.Data
{
    public class InventarioService : IInventarioService
    {
        private readonly InventarioContext _InventarioDbContext;
        public InventarioService(InventarioContext InventarioDbContext)
        {
            _InventarioDbContext = InventarioDbContext;
        }

        public async Task<string> AddInventario(Inventarios Invent)
        {
            await _InventarioDbContext.Inventarios.AddAsync(Invent);
            await _InventarioDbContext.SaveChangesAsync();
            return "Inventario Grabado";
        }

        public async Task<Inventarios> BuscarInventario(int id)
        {
            var invent = await _InventarioDbContext.Inventarios.FirstOrDefaultAsync(x => x.IdInventario == id);

            return invent;
        }

        public async Task<bool> ActualizarInventario(Inventarios invent)
        {
            bool Actualizado = false;
            Inventarios i = await _InventarioDbContext.Inventarios.FirstOrDefaultAsync(x => x.IdInventario == invent.IdInventario);

            if(i != null)
            {
                Utilities.CopyPropertiesTo(invent, i);
                _InventarioDbContext.Inventarios.Update(i);

                var resultado = await _InventarioDbContext.SaveChangesAsync();

                Actualizado = (resultado == 1);
            }
            return Actualizado;
        }

        public async Task DeleteInventario(Inventarios invent)
        {
            if(invent != null && invent.IdInventario > 0)
            {
                _InventarioDbContext.Inventarios.Attach(invent);
                _InventarioDbContext.Entry(invent).Property(x => x.Activo).IsModified = true;
                await _InventarioDbContext.SaveChangesAsync();
            }

            await Task.Factory.StartNew(() => { });
        }
    }
}
