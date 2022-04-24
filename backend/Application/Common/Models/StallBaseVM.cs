using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models
{
    public class StallBaseVM
    {
        public int Id { get; set; }
        public StallTypeBaseVM StallType { get; set; }
    }
}
