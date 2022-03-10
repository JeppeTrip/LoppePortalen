using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Address : AuditableEntity
    {
        public string Street { get; set; }
        public string Number { get; set; }
        public string? Appartment { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
    }
}
