using LeaseInfoWebAPI.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace LeaseInfoWebAPI.Data
{
    public partial class LeaseInfoDbContext : DbContext
    {
        public LeaseInfoDbContext()
        {
        }

        public LeaseInfoDbContext(DbContextOptions<LeaseInfoDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<Instrument> Instrument { get; set; }
        public virtual DbSet<InstrumentPropertyAssoc> InstrumentPropertyAssoc { get; set; }
        public virtual DbSet<Owner> Owner { get; set; }
        public virtual DbSet<OwnerInstrumentAssoc> OwnerInstrumentAssoc { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<Property> Property { get; set; }
        public virtual DbSet<Renter> Renter { get; set; }
        public virtual DbSet<RenterInstrumentAssoc> RenterInstrumentAssoc { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=LeaseInfo;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.Property(e => e.AddressId)
                    .HasColumnName("address_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasColumnName("city")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Line1)
                    .HasColumnName("line1")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Line2)
                    .HasColumnName("line2")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Line3)
                    .HasColumnName("line3")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasColumnName("state")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.ZipCode)
                    .IsRequired()
                    .HasColumnName("zip_code")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.ZipPlus4)
                    .IsRequired()
                    .HasColumnName("zip_plus_4")
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .HasComputedColumnSql("([zip_code]+isnull([zip_route],''))");

                entity.Property(e => e.ZipRoute)
                    .HasColumnName("zip_route")
                    .HasMaxLength(4)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Instrument>(entity =>
            {
                entity.Property(e => e.InstrumentId)
                    .HasColumnName("instrument_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.BeginDate)
                    .HasColumnName("begin_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.EndDate)
                    .HasColumnName("end_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.InstrumentTypeCode)
                    .IsRequired()
                    .HasColumnName("instrument_type_code")
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<InstrumentPropertyAssoc>(entity =>
            {
                entity.HasKey(e => new { e.InstrumentId, e.PropertyId })
                    .HasName("cpk_InstrumentPropertyAssoc");

                entity.Property(e => e.InstrumentId).HasColumnName("instrument_id");

                entity.Property(e => e.PropertyId).HasColumnName("property_id");

                entity.HasOne(d => d.Instrument)
                    .WithMany(p => p.InstrumentPropertyAssoc)
                    .HasForeignKey(d => d.InstrumentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cfk_InstrumentPropertyAssoc_Instrument");

                entity.HasOne(d => d.Property)
                    .WithMany(p => p.InstrumentPropertyAssoc)
                    .HasForeignKey(d => d.PropertyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cfk_InstrumentPropertyAssoc_Property");
            });

            modelBuilder.Entity<Owner>(entity =>
            {
                entity.Property(e => e.OwnerId)
                    .HasColumnName("owner_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.OwnershipPercent)
                    .HasColumnName("ownership_percent")
                    .HasColumnType("numeric(5, 2)")
                    .HasDefaultValueSql("((100.00))");

                entity.HasOne(d => d.OwnerNavigation)
                    .WithOne(p => p.Owner)
                    .HasForeignKey<Owner>(d => d.OwnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cfk_Owner_Person");
            });

            modelBuilder.Entity<OwnerInstrumentAssoc>(entity =>
            {
                entity.HasKey(e => new { e.OwnerId, e.InstrumentId })
                    .HasName("cpk_OwnerInstrumentAssoc");

                entity.Property(e => e.OwnerId).HasColumnName("owner_id");

                entity.Property(e => e.InstrumentId).HasColumnName("instrument_id");

                entity.HasOne(d => d.Instrument)
                    .WithMany(p => p.OwnerInstrumentAssoc)
                    .HasForeignKey(d => d.InstrumentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cfk_OwnerInstrumentAssoc_Instrument");

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.OwnerInstrumentAssoc)
                    .HasForeignKey(d => d.OwnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cfk_OwnerInstrumentAssoc_Owner");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.Property(e => e.PersonId)
                    .HasColumnName("person_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ForwardingAddressId).HasColumnName("forwarding_address_id");

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasColumnName("full_name")
                    .HasMaxLength(107)
                    .IsUnicode(false)
                    .HasComputedColumnSql("(((isnull([title]+' ','')+[first_name])+' ')+[last_name])");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MailingAddressId).HasColumnName("mailing_address_id");

                entity.Property(e => e.ShippingAddressId).HasColumnName("shipping_address_id");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.HasOne(d => d.ForwardingAddress)
                    .WithMany(p => p.PersonForwardingAddress)
                    .HasForeignKey(d => d.ForwardingAddressId)
                    .HasConstraintName("cfk_Person_forwarding_address");

                entity.HasOne(d => d.MailingAddress)
                    .WithMany(p => p.PersonMailingAddress)
                    .HasForeignKey(d => d.MailingAddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cfk_Person_mailing_address");

                entity.HasOne(d => d.ShippingAddress)
                    .WithMany(p => p.PersonShippingAddress)
                    .HasForeignKey(d => d.ShippingAddressId)
                    .HasConstraintName("cfk_Person_shipping_address");
            });

            modelBuilder.Entity<Property>(entity =>
            {
                entity.Property(e => e.PropertyId)
                    .HasColumnName("property_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.PhysicalAddressId).HasColumnName("physical_address_id");

                entity.Property(e => e.PropertyTypeCode)
                    .IsRequired()
                    .HasColumnName("property_type_code")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.PhysicalAddress)
                    .WithMany(p => p.Property)
                    .HasForeignKey(d => d.PhysicalAddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cfk_Property_physical_address");
            });

            modelBuilder.Entity<Renter>(entity =>
            {
                entity.Property(e => e.RenterId)
                    .HasColumnName("renter_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.LeaseAmount)
                    .HasColumnName("lease_amount")
                    .HasColumnType("numeric(8, 2)");

                entity.Property(e => e.LeasePaymentPeriod)
                    .IsRequired()
                    .HasColumnName("lease_payment_period")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ResidentialAddressId).HasColumnName("residential_address_id");

                entity.HasOne(d => d.RenterNavigation)
                    .WithOne(p => p.Renter)
                    .HasForeignKey<Renter>(d => d.RenterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cfk_Renter_Person");

                entity.HasOne(d => d.ResidentialAddress)
                    .WithMany(p => p.Renter)
                    .HasForeignKey(d => d.ResidentialAddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cfk_Renter_residential_address");
            });

            modelBuilder.Entity<RenterInstrumentAssoc>(entity =>
            {
                entity.HasKey(e => new { e.RenterId, e.InstrumentId })
                    .HasName("cpk_RentalInstrumentAssoc");

                entity.Property(e => e.RenterId).HasColumnName("renter_id");

                entity.Property(e => e.InstrumentId).HasColumnName("instrument_id");

                entity.HasOne(d => d.Instrument)
                    .WithMany(p => p.RenterInstrumentAssoc)
                    .HasForeignKey(d => d.InstrumentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cfk_RenterInstrumentAssoc_Instrument");

                entity.HasOne(d => d.Renter)
                    .WithMany(p => p.RenterInstrumentAssoc)
                    .HasForeignKey(d => d.RenterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cfk_RenterInstrumentAssoc_Renter");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
