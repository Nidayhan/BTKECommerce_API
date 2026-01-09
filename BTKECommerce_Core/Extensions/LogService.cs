using BTKECommerce_Domain.Data;
using BTKECommerce_Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTKECommerce_Core.Extensions
{
    public class LogService : ILogService
    {
        private readonly ApplicationDbContext _context;
        public LogService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task LogAsync(string level, string message, HttpContext context)
        {
            var log = new AppLog
            {
                Level = level,
                Message = message,
                Path = context.Request.Path,
                Method = context.Request.Method,
                Timestamp = DateTime.UtcNow
            };
            _context.AppLogs.Add(log);
            return _context.SaveChangesAsync();
        }
    }
}
//Program.cs eklenecek
//app.UseMiddleware<LoggingMiddleware>();