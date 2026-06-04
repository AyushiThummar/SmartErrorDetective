using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartError.API.Data;
using SmartError.API.ViewModels;

namespace SmartError.API.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var model = new DashboardViewModel
            {
                TotalErrors = _context.ErrorLogs.Count(),

                CriticalErrors = _context.ErrorLogs
                    .Include(x => x.Severity)
                    .Count(x => x.Severity!.Name == "Critical"),

                HighErrors = _context.ErrorLogs
                    .Include(x => x.Severity)
                    .Count(x => x.Severity!.Name == "High"),

                DuplicateErrors = _context.ErrorLogs
                    .Count(x => x.OccurrenceCount > 1)
            };

            return View(model);
        }
    }
}