namespace LeaseInfoWebAPI.Data.Models
{
    public partial class OwnerInstrumentAssoc
    {
        public int OwnerId { get; set; }
        public int InstrumentId { get; set; }

        public virtual Instrument Instrument { get; set; }
        public virtual Owner Owner { get; set; }
    }
}
