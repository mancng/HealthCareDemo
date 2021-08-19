using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace DataAccess.Models
{
    public partial class KPDemoContext : DbContext
    {

        private readonly IConfiguration _configuration;
        public KPDemoContext(DbContextOptions<KPDemoContext> options, IConfiguration configuration)
            : base(options)
        {
            this._configuration = configuration;
        }

        public virtual DbSet<Claim> Claims { get; set; }
        public virtual DbSet<DiagnosisCode> DiagnosisCodes { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<Provider> Providers { get; set; }
        public virtual DbSet<ProviderSpeciality> ProviderSpecialties { get; set; }
        public virtual DbSet<Speciality> Specialties { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(this._configuration.GetConnectionString("KPDemoDb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Claim>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.DateOfService).HasColumnType("date");

                entity.HasOne(d => d.DiagnosisCode)
                    .WithMany()
                    .HasForeignKey(d => d.DiagnosisCodeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Claims_DiagnosisCodes");

                entity.HasOne(d => d.Member)
                    .WithMany()
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Claims_Members");

                entity.HasOne(d => d.Provider)
                    .WithMany()
                    .HasForeignKey(d => d.ProviderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Claims_Providers");
            });

            modelBuilder.Entity<DiagnosisCode>(entity =>
            {
                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.Property(e => e.Dob).HasColumnType("date");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Provider>(entity =>
            {
                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<ProviderSpeciality>(entity =>
            {
                entity.HasKey(e => new { e.ProviderId, e.SpecialtyId });

                entity.HasIndex(e => new { e.ProviderId, e.SpecialtyId }, "ProviderId_SpecialtyId")
                    .IsUnique();

                entity.HasOne(d => d.Provider)
                    .WithMany(p => p.ProviderSpecialties)
                    .HasForeignKey(d => d.ProviderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProviderSpecialties_Providers");

                entity.HasOne(d => d.Specialty)
                    .WithMany(p => p.ProviderSpecialties)
                    .HasForeignKey(d => d.SpecialtyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProviderSpecialties_Specialties");
            });

            modelBuilder.Entity<Speciality>(entity =>
            {
                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
