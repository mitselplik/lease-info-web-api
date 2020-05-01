using System.Collections.Generic;

namespace LeaseInfoWebAPI.Data.Models
{
	public partial class Owner
	{
		public Owner()
		{
			OwnerInstrumentAssoc = new HashSet<OwnerInstrumentAssoc>();
		}

		public int OwnerId { get; set; }
		public decimal OwnershipPercent { get; set; }

		public virtual Person OwnerNavigation { get; set; }
		public virtual ICollection<OwnerInstrumentAssoc> OwnerInstrumentAssoc { get; set; }
	}
}
