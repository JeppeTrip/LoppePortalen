using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Markets.Commands.BookStalls
{
    public class BookStallsRequest
    {
        public int MarketId { get; set; }
        public int MerchantId { get; set; }
        public List<StallBooking> Stalls { get; set; }
    }

    public class StallBooking
    {
        public int StallTypeId { get; set; }
        public int BookingAmount { get; set; }
    }
}
