using Application.Common.Models;

namespace Application.Markets.Queries.GetFilteredMarkets
{
    public class FilteredMarketVM : MarketBaseVM
    {
        public OrganiserBaseVM Organiser { get; set; }
    }
}
