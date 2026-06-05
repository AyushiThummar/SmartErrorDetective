using Microsoft.EntityFrameworkCore;
using SmartError.API.Data;
using SmartError.API.Services;
using Microsoft.AspNetCore.Diagnostics;
using SmartError.API.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ErrorAnalyzerService>();

var app = builder.Build();

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var exceptionFeature =
            context.Features.Get<IExceptionHandlerFeature>();

        if (exceptionFeature != null)
        {
            var db =
                context.RequestServices
                .GetRequiredService<ApplicationDbContext>();

            var analyzer =
                context.RequestServices
                .GetRequiredService<ErrorAnalyzerService>();

            var exception =
                exceptionFeature.Error;

            var categoryName =
                analyzer.DetectCategory(
                    exception.StackTrace ?? "");

            var severityName =
                analyzer.DetectSeverity(
                    exception.Message);

            var fix =
                analyzer.GenerateFix(
                    exception.Message);

            var category =
                db.Categories
                .FirstOrDefault(x => x.Name == categoryName);

            var severity =
                db.Severities
                .FirstOrDefault(x => x.Name == severityName);

            if (category != null && severity != null)
            {
                var existingError =
                    db.ErrorLogs.FirstOrDefault(x =>
                        x.Title == exception.Message &&
                        x.StackTrace == exception.StackTrace);

                if (existingError != null)
                {
                    existingError.OccurrenceCount++;
                }
                else
                {
                    db.ErrorLogs.Add(new ErrorLog
                    {
                        Title = exception.Message,
                        Description = exception.Message,
                        StackTrace = exception.StackTrace ?? "",
                        ModuleName = "Auto Captured",
                        Status = "Open",
                        CategoryId = category.Id,
                        SeverityId = severity.Id,
                        SuggestedFix = fix,
                        OccurrenceCount = 1
                    });
                }

                db.SaveChanges();
            }
        }

        context.Response.Redirect("/Error");
    });
});

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");

app.Run();
