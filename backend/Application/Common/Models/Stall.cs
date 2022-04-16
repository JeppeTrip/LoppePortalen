using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models
{
    public class Stall
    {
        public int Id { get; set; }
        public StallType StallType { get; set; }
        public Market? Market { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
