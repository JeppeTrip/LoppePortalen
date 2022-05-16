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
            return instance.Stalls.Count();
        }

        public static int AvailableStallCount(this MarketInstance instance)
        {
            return instance.Stalls.Where(x => x.Bookings.Count() == 0).Count();  
        }

        public static int OccupiedStallCount(this MarketInstance instance)
        {
            return instance.Stalls.Where(x => x.Bookings.Count() > 0).Count();
        }

        public static List<string> ItemCategories(this MarketInstance instance)
        {
            var booths = instance.Stalls.Where(x => x.Bookings.Count() > 0).SelectMany(x => x.Bookings);
            var itemCategories = booths.Where(x => x.ItemCategories.Count > 0).SelectMany(x => x.ItemCategories);
            var categories = itemCategories.Select(x => x.Name).Distinct().ToList();
            return categories;
        }
    }
}
