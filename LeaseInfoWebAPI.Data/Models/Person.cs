namespace LeaseInfoWebAPI.Data.Models
{
    public partial class Person
    {
        public int PersonId { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public int MailingAddressId { get; set; }
        public int? ShippingAddressId { get; set; }
        public int? ForwardingAddressId { get; set; }

        public virtual Address ForwardingAddress { get; set; }
        public virtual Address MailingAddress { get; set; }
        public virtual Address ShippingAddress { get; set; }
        public virtual Owner Owner { get; set; }
        public virtual Renter Renter { get; set; }
    }
}
