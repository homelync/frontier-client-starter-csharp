using System.Collections.Generic;

namespace Frontier.Models
{
    public class PagedResponse<T>
    {
        public PagedResponse()
        {
            Results = new List<T>();
        }

        public List<T> Results { get; set; }
        public int Count { get; set; }
        public int Page { get; set; }
        public int Limit { get; set; }
        public int TotalPages { get; set; }
    }
}