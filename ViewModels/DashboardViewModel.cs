namespace SmartError.API.ViewModels
{
    public class DashboardViewModel
    {
        public int TotalErrors { get; set; }

        public int CriticalErrors { get; set; }

        public int HighErrors { get; set; }

        public int DuplicateErrors { get; set; }

        public Dictionary<string, int> SeverityData { get; set; }
            = new();

        public Dictionary<string, int> CategoryData { get; set; }
            = new();
    }
}