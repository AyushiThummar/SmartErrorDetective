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

        public IActionResult Index(
    string searchTerm,
    string category,
    string severity,
    string status)
        {
            var query = _context.ErrorLogs
                .Include(x => x.Category)
                .Include(x => x.Severity)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(x =>
                    x.Title.Contains(searchTerm));
            }

            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(x =>
                    x.Category!.Name == category);
            }

            if (!string.IsNullOrEmpty(severity))
            {
                query = query.Where(x =>
                    x.Severity!.Name == severity);
            }

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(x =>
                    x.Status == status);
            }

            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Severities = _context.Severities.ToList();

            return View(query.ToList());
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
                Status = dto.Status,
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
        public IActionResult Details(int id)
        {
            var error = _context.ErrorLogs
                .Include(x => x.Category)
                .Include(x => x.Severity)
                .FirstOrDefault(x => x.Id == id);

            if (error == null)
            {
                return NotFound();
            }

            return View(error);
        }
        public IActionResult Edit(int id)
        {
            var error = _context.ErrorLogs.Find(id);

            if (error == null)
                return NotFound();

            return View(error);
        }
        [HttpPost]
        public IActionResult Edit(ErrorLog model)
        {
            var error = _context.ErrorLogs
                .FirstOrDefault(x => x.Id == model.Id);

            if (error == null)
            {
                return NotFound();
            }

            error.Title = model.Title;
            error.Status = model.Status;
            error.SuggestedFix = model.SuggestedFix;

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            var error = _context.ErrorLogs.Find(id);

            if (error != null)
            {
                _context.ErrorLogs.Remove(error);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}