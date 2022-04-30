namespace Application.Stalls.Commands.RemoveStallsFromMarket
{
    public class RemoveStallsFromMarketRequest
    {
        public int MarketId { get; set; }
        public int StallTypeId { get; set; }
        public int Diff { get; set; }
    }
}
