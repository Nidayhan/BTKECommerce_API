using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTKECommerce_Domain.Entities
{
    public class AppLog
    {
        public int Id { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }
        public string Path { get; set; }
        public string Method { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
