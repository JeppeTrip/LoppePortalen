using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class ContactInfo : AuditableEntity
    {
        public ContactInfoType ContactType { get; set; }
        public string Value { get; set; }
        public int OrganiserId { get; set; }
        public virtual Organiser Organiser { get; set; }
    }
}
