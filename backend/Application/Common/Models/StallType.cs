using System.Collections.Generic;

namespace Application.Common.Models
{
    public class StallType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int TotalStallCount { get; set; }
        public Market? Market { get; set; }
        public List<Stall> Stalls { get; set; }
    }
}
