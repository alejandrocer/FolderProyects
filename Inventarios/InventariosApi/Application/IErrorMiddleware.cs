using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace InventariosApi.Application
{
    interface IErrorMiddleware
    {
        Task Invoke(HttpContext context);
    }
}
