using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class OrganiserImage : AuditableEntity
    {
        public int OrganiserId { get; set; }
        public virtual Organiser Organiser {get ; set;}
        public string ImageTitle { get; set; }
        public byte[] ImageData { get; set; }
    }
}
