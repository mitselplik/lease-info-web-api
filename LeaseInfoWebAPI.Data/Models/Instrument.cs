using System;
using System.Collections.Generic;

namespace LeaseInfoWebAPI.Data.Models
{
    public partial class Instrument
    {
        public Instrument()
        {
            InstrumentPropertyAssoc = new HashSet<InstrumentPropertyAssoc>();
            OwnerInstrumentAssoc = new HashSet<OwnerInstrumentAssoc>();
            RenterInstrumentAssoc = new HashSet<RenterInstrumentAssoc>();
        }

        public int InstrumentId { get; set; }
        public string InstrumentTypeCode { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual ICollection<InstrumentPropertyAssoc> InstrumentPropertyAssoc { get; set; }
        public virtual ICollection<OwnerInstrumentAssoc> OwnerInstrumentAssoc { get; set; }
        public virtual ICollection<RenterInstrumentAssoc> RenterInstrumentAssoc { get; set; }
    }
}
