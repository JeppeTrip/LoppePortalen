using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Booths.Commands.UpdateBooth
{
    public class UpdateBoothRequest
    {
        public string Id { get; set; }
        public string BoothName { get; set; }
        public string BoothDescription { get; set; }
        public List<string> ItemCategories { get; set; } = new List<string>();
    }
}
