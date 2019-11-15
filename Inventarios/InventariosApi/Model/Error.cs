using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventariosApi.Model
{
    public class Error
    {
        public int Code { get; set; }
        public int HttpCode { get; set; }
        public string Message { get; set; }
        public string Description { get; set; }
        public string Path { get; set; }
        public DateTime Timestamp { get; set; }
        public string TransactionId { get; set; }
    }
}
