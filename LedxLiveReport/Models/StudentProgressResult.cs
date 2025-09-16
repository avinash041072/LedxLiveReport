
    using System.Collections.Generic;
using System.Text.Json.Serialization;

    namespace LedxLiveReport.Models
    {
    public sealed class StudentProgressResult
    {
        public bool success { get; set; }
        public int count { get; set; }
        public List<StudentProgress> data { get; set; } = new();
        public string? error { get; set; }
    }
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public sealed class StudentProgress
    {
        public long user_id { get; set; }
        public string? user_login { get; set; }
        public long course_id { get; set; }
        public string? course_name { get; set; }
        public long total_lessons { get; set; }
        public decimal? completed_lessons { get; set; }
        public decimal? completion_percent { get; set; }
        public string? last_completed_date { get; set; }
    }
}

