public partial class Program
{
    public class Event
    {
        public int Id { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Location { get; set; }
        public int Priority { get; set; }
    }
}