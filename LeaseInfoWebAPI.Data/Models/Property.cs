using System.Collections.Generic;

namespace LeaseInfoWebAPI.Data.Models
{
    public partial class Property
    {
        public Property()
        {
            InstrumentPropertyAssoc = new HashSet<InstrumentPropertyAssoc>();
        }

        public int PropertyId { get; set; }
        public int PhysicalAddressId { get; set; }
        public string PropertyTypeCode { get; set; }

        public virtual Address PhysicalAddress { get; set; }
        public virtual ICollection<InstrumentPropertyAssoc> InstrumentPropertyAssoc { get; set; }
    }
}
