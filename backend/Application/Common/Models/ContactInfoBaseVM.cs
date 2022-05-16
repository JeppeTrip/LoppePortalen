using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models
{
    public class ContactInfoBaseVM
    {
        public string Value { get; set; }
        public ContactInfoType Type { get; set; }
    }
}
