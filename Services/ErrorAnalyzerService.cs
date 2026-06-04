namespace SmartError.API.Services
{
    public class ErrorAnalyzerService
    {
        public string DetectCategory(string stackTrace)
        {
            if (stackTrace.Contains("Sql"))
                return "Database";

            if (stackTrace.Contains("Auth"))
                return "Authentication";

            if (stackTrace.Contains("Network"))
                return "Network";

            return "Application";
        }

        public string DetectSeverity(string title)
        {
            if (title.Contains("Database Down"))
                return "Critical";

            if (title.Contains("Timeout"))
                return "High";

            if (title.Contains("Validation"))
                return "Low";

            return "Medium";
        }

        public string GenerateFix(string title)
        {
            if (title.Contains("NullReference"))
                return "Initialize object before accessing it.";

            if (title.Contains("Sql"))
                return "Check database connection string.";

            if (title.Contains("Timeout"))
                return "Increase timeout or optimize query.";

            return "Review stack trace and logs.";
        }
    }
}