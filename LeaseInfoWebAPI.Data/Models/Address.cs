using System.Collections.Generic;

namespace LeaseInfoWebAPI.Data.Models
{
    public partial class Address
    {
        public Address()
        {
            PersonForwardingAddress = new HashSet<Person>();
            PersonMailingAddress = new HashSet<Person>();
            PersonShippingAddress = new HashSet<Person>();
            Property = new HashSet<Property>();
            Renter = new HashSet<Renter>();
        }

        public int AddressId { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string ZipRoute { get; set; }
        public string ZipPlus4 { get; set; }

        public virtual ICollection<Person> PersonForwardingAddress { get; set; }
        public virtual ICollection<Person> PersonMailingAddress { get; set; }
        public virtual ICollection<Person> PersonShippingAddress { get; set; }
        public virtual ICollection<Property> Property { get; set; }
        public virtual ICollection<Renter> Renter { get; set; }
    }
}
