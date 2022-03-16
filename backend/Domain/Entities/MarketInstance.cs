﻿using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class MarketInstance : AuditableEntity
    {
        //TODO: I don't actually think this is a strictly necessary ID as this likely is a weak entity. 
        //TODO: Clean up the datamodel in general.
        public int Id {  get; set; } 
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }

        public virtual MarketTemplate MarketTemplate { get; set; }
    }
}