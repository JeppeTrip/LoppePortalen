﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Markets.Commands.CancelMarket
{
    public class CancelMarketInstanceResponse
    {
        public int MarketId { get; set; }
        public bool IsCancelled { get; set; }
    }
}
