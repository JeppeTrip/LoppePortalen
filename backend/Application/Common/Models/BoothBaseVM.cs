using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models
{
    public class BoothBaseVM
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public StallBaseVM Stall { get; set; }
        public List<ItemCategory> Categories { get; set; }
    }
}
