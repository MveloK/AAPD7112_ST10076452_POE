using System;
using System.Collections.Generic;
using System.Linq;

namespace AAPD7112_ST10076452_MveloKhumalo
{
    
    public class EventManager
    {
        private readonly SortedDictionary<DateTime, List<Event>> _eventsByDate;
        private readonly HashSet<string> _uniqueCategories;
        private readonly Queue<string> _recentSearches;
        private readonly Dictionary<string, int> _recommendationCounts;

        public IEnumerable<string> Categories => _uniqueCategories;

        public EventManager()
        {
            _eventsByDate = new SortedDictionary<DateTime, List<Event>>();
            _uniqueCategories = new HashSet<string>();
            _recentSearches = new Queue<string>();
            _recommendationCounts = new Dictionary<string, int>();

            LoadSampleData();
        }

        private void LoadSampleData()
        {
            var sampleEvents = new List<Event>
            {
                new Event { Id = 1, Title = "Community Fun Run", Date = DateTime.Today.AddDays(5), Category = "Sports", Description = "5k fun run for charity.", Priority = 2 },
                new Event { Id = 2, Title = "Tech Meetup: C# Basics", Date = DateTime.Today.AddDays(10), Category = "Education", Description = "An introduction to C# for beginners.", Priority = 3 },
                new Event { Id = 3, Title = "Town Hall Meeting", Date = DateTime.Today.AddDays(1), Category = "Announcement", Description = "Discussing the new city park project.", Priority = 5 },
                new Event { Id = 4, Title = "Local Art Exhibition", Date = DateTime.Today.AddDays(15), Category = "Arts", Description = "Featuring local painters and sculptors.", Priority = 1 },
                new Event { Id = 5, Title = "Volunteer Day", Date = DateTime.Today.AddDays(7), Category = "Community", Description = "Help clean up the river banks.", Priority = 3 },
                new Event { Id = 6, Title = "Book Club: 'Data Structures'", Date = DateTime.Today.AddDays(25), Category = "Education", Description = "Discussion on advanced programming concepts.", Priority = 2 }
            };

            foreach (var evt in sampleEvents)
            {
                AddEvent(evt);
            }
        }

       
        public void AddEvent(Event evt)
        {
            if (!_eventsByDate.ContainsKey(evt.Date.Date))
            {
                _eventsByDate[evt.Date.Date] = new List<Event>();
            }
            _eventsByDate[evt.Date.Date].Add(evt);
            _uniqueCategories.Add(evt.Category);
        }

        
        public List<Event> GetAllEvents()
        {
            return _eventsByDate.Values.SelectMany(list => list).ToList();
        }

        
        public List<Event> SearchEvents(string keyword, string category, DateTime? date)
        {
            var results = GetAllEvents();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                results = results.Where(e =>
                    e.Title.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    e.Description.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0).ToList();

                if (_recentSearches.Count >= 5) _recentSearches.Dequeue();
                _recentSearches.Enqueue(keyword.Trim());

                UpdateRecommendationCount(keyword.Trim());
            }

            if (!string.IsNullOrWhiteSpace(category) && category != "All Categories")
            {
                results = results.Where(e => e.Category.Equals(category)).ToList();
                UpdateRecommendationCount(category);
            }

            if (date.HasValue)
            {
                results = results.Where(e => e.Date.Date == date.Value.Date).ToList();
            }

            return results;
        }

        
        private void UpdateRecommendationCount(string term)
        {
            term = term.ToLower();
            if (_recommendationCounts.ContainsKey(term))
            {
                _recommendationCounts[term]++;
            }
            else
            {
                _recommendationCounts[term] = 1;
            }
        }

        
        public List<Event> GetRecommendations()
        {
            if (!_recommendationCounts.Any())
            {
                return new List<Event>();
            }

            var topSearch = _recommendationCounts
                .OrderByDescending(kvp => kvp.Value)
                .Select(kvp => kvp.Key)
                .FirstOrDefault();

            if (topSearch == null) return new List<Event>();

            var recommendations = GetAllEvents()
                .Where(e => e.Category.Equals(topSearch, StringComparison.OrdinalIgnoreCase) ||
                            e.Title.IndexOf(topSearch, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();

            return recommendations.Take(3).ToList();
        }

       
        public List<Event> GetPriorityAnnouncements()
        {
            return GetAllEvents()
                   .Where(e => e.Category == "Announcement")
                   .OrderByDescending(e => e.Priority) 
                   .ToList();
        }
    }
}
