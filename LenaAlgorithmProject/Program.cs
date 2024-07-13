public partial class Program
{
    public static void Main()
    {
        List<Event> events = new List<Event>
        {
            new Event {Id = 1,StartTime = new TimeSpan(10,0,0), EndTime = new TimeSpan(12,0,0), Location = "A", Priority = 50},
            new Event {Id = 2,StartTime = new TimeSpan(10,0,0), EndTime = new TimeSpan(11,0,0), Location = "B", Priority = 30},
            new Event {Id = 3,StartTime = new TimeSpan(11,30,0), EndTime = new TimeSpan(12,30,0), Location = "A", Priority = 40},
            new Event {Id = 4,StartTime = new TimeSpan(14,30,0), EndTime = new TimeSpan(16,0,0), Location = "C", Priority = 70},
            new Event {Id = 5,StartTime = new TimeSpan(14,25,0), EndTime = new TimeSpan(15,30,0), Location = "B", Priority = 60},
            new Event {Id = 6,StartTime = new TimeSpan(13,0,0), EndTime = new TimeSpan(14,0,0), Location =  "D", Priority = 80},

        };

        List<TimeBetweenTwoLocation> times = new List<TimeBetweenTwoLocation>
        {
            new TimeBetweenTwoLocation {FromLocation = "A", ToLocation = "B", DurationMinute = 15},
            new TimeBetweenTwoLocation {FromLocation = "A", ToLocation = "C", DurationMinute = 20},
            new TimeBetweenTwoLocation {FromLocation = "A", ToLocation = "D", DurationMinute = 10},
            new TimeBetweenTwoLocation {FromLocation = "B", ToLocation = "C", DurationMinute = 5},
            new TimeBetweenTwoLocation {FromLocation = "B", ToLocation = "D", DurationMinute = 25},
            new TimeBetweenTwoLocation {FromLocation = "C", ToLocation = "D", DurationMinute = 25},
        };

        var sortedEvents = events.OrderBy(s => s.StartTime).ThenByDescending(s => s.Priority).ToList();
        var currentTime = new TimeSpan(10, 0, 0);
        List<Event> selectedEvents = new();


        for (int i = 0; i < sortedEvents.Count; i++)
        {
            int index = i;
            var data = sortedEvents.ElementAt(index);

            if (selectedEvents.Count >= 3)
                break;

            if (currentTime <= data.StartTime)
            {
                var nextData = sortedEvents.OrderByDescending(s => s.Priority).FirstOrDefault(s => s.StartTime >= currentTime);

                if (nextData != null && nextData.Priority > data.Priority && nextData.StartTime.Hours == data.StartTime.Hours)
                    data = nextData;

                selectedEvents.Add(data);
                currentTime = data.EndTime;
                var travelTime = times.FirstOrDefault(s => s.FromLocation == data.Location && s.ToLocation == nextData?.Location)?.DurationMinute;

                if (travelTime != null)
                    currentTime = currentTime.Add(new TimeSpan(0, (int)travelTime, 0));

            }
        }

        string ids = String.Join(", ", selectedEvents.Select(s => s.Id));
        Console.WriteLine($"Katılınabilecek Etkinliklerin ID'leri: {ids}");
        Console.WriteLine($"Toplam Değer: {selectedEvents.Sum(s => s.Priority)}");

        Console.ReadLine();

    }
}