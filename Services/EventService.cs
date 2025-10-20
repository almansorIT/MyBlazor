namespace BlazorPerformanceApp.Services
{
    using System;

    public class EventItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Location { get; set; } = string.Empty;
    }

    public class EventService
    {
        private readonly List<EventItem> _events = new();
        private int _nextId = 1;
        private readonly object _lock = new();

        // Return a snapshot array to avoid exposing internal list and reduce race conditions
        public EventItem[] Events
        {
            get
            {
                lock (_lock)
                {
                    return _events.ToArray();
                }
            }
        }

        public EventItem Add(string name, DateTime date, string location)
        {
            // Defensive validation: ensure service is never given invalid data.
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Event name is required.", nameof(name));

            if (string.IsNullOrWhiteSpace(location))
                throw new ArgumentException("Event location is required.", nameof(location));

            // Normalize inputs
            name = name.Trim();
            location = location.Trim();
            date = date.Date;

            lock (_lock)
            {
                var item = new EventItem
                {
                    Id = _nextId++,
                    Name = name,
                    Date = date,
                    Location = location
                };
                _events.Add(item);
                return item;
            }
        }

        public EventItem? Get(int id)
        {
            lock (_lock)
            {
                return _events.FirstOrDefault(e => e.Id == id);
            }
        }
    }
}