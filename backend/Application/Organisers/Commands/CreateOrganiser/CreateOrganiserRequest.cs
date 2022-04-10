﻿using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Organisers.Commands.CreateOrganiser
{
    public class CreateOrganiserRequest
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } = "";
        public string Street { get; set; }
        public string Number { get; set; }
        public string Appartment { get; set; } = "";
        public string PostalCode { get; set; }
        public string City { get; set; }
    }
}
