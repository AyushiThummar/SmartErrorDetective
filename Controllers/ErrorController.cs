using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartError.API.Data;
using SmartError.API.DTOs;
using SmartError.API.Models;
using SmartError.API.Services;

namespace SmartError.API.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ErrorAnalyzerService _analyzer;

        public ErrorController(
            ApplicationDbContext context,
            ErrorAnalyzerService analyzer)
        {
            _context = context;
            _analyzer = analyzer;
        }

        public IActionResult Index()
        {
            var errors = _context.ErrorLogs
    .Include(x => x.Category)
    .Include(x => x.Severity)
    .ToList();

            return View(errors);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateErrorDto dto)
        {
            var categoryName =
                _analyzer.DetectCategory(dto.StackTrace);

            var severityName =
                _analyzer.DetectSeverity(dto.Title);

            var fix =
                _analyzer.GenerateFix(dto.Title);

            var category =
                _context.Categories
                .FirstOrDefault(x => x.Name == categoryName);

            var severity =
                _context.Severities
                .FirstOrDefault(x => x.Name == severityName);

            if (category == null || severity == null)
            {
                return Content("Category or Severity not found");
            }

            var error = new ErrorLog
            {
                Title = dto.Title,
                Description = dto.Description,
                StackTrace = dto.StackTrace,
                ModuleName = dto.ModuleName,
                CategoryId = category.Id,
                SeverityId = severity.Id,
                SuggestedFix = fix
            };

            var existingError = _context.ErrorLogs
    .FirstOrDefault(x =>
        x.Title == dto.Title &&
        x.StackTrace == dto.StackTrace);

            if (existingError != null)
            {
                existingError.OccurrenceCount++;

                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            _context.ErrorLogs.Add(error);

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}