﻿using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Merchants.Queries.AllMerchants
{
    public class AllMerchantsQueryResponse
    { 
        public List<MerchantBaseVM> MerchantList { get; set; }
    }
}
