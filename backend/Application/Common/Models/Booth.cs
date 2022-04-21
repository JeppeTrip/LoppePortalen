using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models
{
    public class Booth
    {
        public string Id { get; set; }
        public Stall Stall { get; set; }
        public Merchant Merchant { get; set; }
        public string BoothName { get; set; }
        public string BoothDescription { get; set; }
    }
}
