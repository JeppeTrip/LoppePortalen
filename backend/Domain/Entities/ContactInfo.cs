using Domain.Common;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ContactInfo : AuditableEntity
    {
        public ContactInfoType ContactType { get; set; }
        public string Value { get; set; }   
    }
}
