﻿using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.StallTypes.Commands.CreateStallTypes
{
    public class CreateStallTypesResponse
    { 
        public List<StallTypeBaseVM> StallTypes { get; set; }
    }
}
