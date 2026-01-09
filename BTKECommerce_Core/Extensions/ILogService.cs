using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTKECommerce_Core.Extensions
{
    public interface ILogService
    {
        Task LogAsync(string level, string message, HttpContext context);
    }
}
