using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    //TODO: Fix the redundancy that is built in due to the use of the base identity user´.
    //TODO: Update with more address information
    public class User : AuditableEntity
    {
        public string IdentityId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public string Country { get; set; }

        public virtual List<Organiser> Organisers { get; set; }
    }
}
