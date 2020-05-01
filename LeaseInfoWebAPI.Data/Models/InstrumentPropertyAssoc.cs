namespace LeaseInfoWebAPI.Data.Models
{
    public partial class InstrumentPropertyAssoc
    {
        public int InstrumentId { get; set; }
        public int PropertyId { get; set; }

        public virtual Instrument Instrument { get; set; }
        public virtual Property Property { get; set; }
    }
}
