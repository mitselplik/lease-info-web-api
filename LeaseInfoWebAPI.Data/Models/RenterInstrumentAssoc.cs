namespace LeaseInfoWebAPI.Data.Models
{
    public partial class RenterInstrumentAssoc
    {
        public int RenterId { get; set; }
        public int InstrumentId { get; set; }

        public virtual Instrument Instrument { get; set; }
        public virtual Renter Renter { get; set; }
    }
}
