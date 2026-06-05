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
            var totalErrors = _context.ErrorLogs.Count();

            var criticalErrors = _context.ErrorLogs
                .Include(x => x.Severity)
                .Count(x => x.Severity!.Name == "Critical");

            var highErrors = _context.ErrorLogs
                .Include(x => x.Severity)
                .Count(x => x.Severity!.Name == "High");

            var duplicateErrors = _context.ErrorLogs
                .Count(x => x.OccurrenceCount > 1);

            var severityData = _context.ErrorLogs
                .Include(x => x.Severity)
                .GroupBy(x => x.Severity!.Name)
                .ToDictionary(
                    g => g.Key,
                    g => g.Count());

            var categoryData = _context.ErrorLogs
                .Include(x => x.Category)
                .GroupBy(x => x.Category!.Name)
                .ToDictionary(
                    g => g.Key,
                    g => g.Count());

            var model = new DashboardViewModel
            {
                TotalErrors = totalErrors,
                CriticalErrors = criticalErrors,
                HighErrors = highErrors,
                DuplicateErrors = duplicateErrors,

                SeverityData = severityData,
                CategoryData = categoryData
            };

            return View(model);
        }
    }
}