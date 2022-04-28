using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.EntityExtensions
{
    public static class MarketInstanceExtensions
    {
        public static int TotalStallCount(this MarketInstance instance)
        {
            return 0;
        }

        public static int AvailableStallCount(this MarketInstance instance)
        {
            return 0;
        }

        public static int OccupiedStallCount(this MarketInstance instance)
        {
            return 0;
        }

        public static List<string> ItemCategories(this MarketInstance instance)
        {
            var list = new List<string>();
            return list;
        }
    }
}
