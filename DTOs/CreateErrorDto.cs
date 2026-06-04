namespace SmartError.API.DTOs
{
    public class CreateErrorDto
    {
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string StackTrace { get; set; } = string.Empty;

        public string ModuleName { get; set; } = string.Empty;
        public string Status { get; set; } = "Open";
    }
}