namespace SmartError.API.Models
{
    public class Severity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<ErrorLog>? ErrorLogs { get; set; }
    }
}