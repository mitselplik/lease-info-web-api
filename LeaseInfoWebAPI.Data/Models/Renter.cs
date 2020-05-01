using System.Collections.Generic;

namespace LeaseInfoWebAPI.Data.Models
{
    public partial class Renter
    {
        public Renter()
        {
            RenterInstrumentAssoc = new HashSet<RenterInstrumentAssoc>();
        }

        public int RenterId { get; set; }
        public int ResidentialAddressId { get; set; }
        public decimal LeaseAmount { get; set; }
        public string LeasePaymentPeriod { get; set; }

        public virtual Person RenterNavigation { get; set; }
        public virtual Address ResidentialAddress { get; set; }
        public virtual ICollection<RenterInstrumentAssoc> RenterInstrumentAssoc { get; set; }
    }
}
