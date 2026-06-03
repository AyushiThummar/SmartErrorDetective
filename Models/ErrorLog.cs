namespace SmartError.API.Models
{
    public class ErrorLog
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string StackTrace { get; set; } = string.Empty;

        public string ModuleName { get; set; } = string.Empty;

        public int CategoryId { get; set; }

        public Category? Category { get; set; }

        public int SeverityId { get; set; }

        public Severity? Severity { get; set; }

        public int OccurrenceCount { get; set; } = 1;

        public string Status { get; set; } = "Open";

        public string SuggestedFix { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}