using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using LeerPlatform_Team2.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace LeerPlatform_Team2
{
    public partial class GIPContext : IdentityDbContext<TblGebruiker>
    {
        public GIPContext()
        {
        }

        public GIPContext(DbContextOptions<GIPContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblFunctionaliteiten> TblFunctionaliteiten { get; set; }
        public virtual DbSet<TblLessen> TblLessen { get; set; }
        public virtual DbSet<TblLessenreeks> TblLessenreeks { get; set; }
        public virtual DbSet<TblLokalen> TblLokalen { get; set; }
        public virtual DbSet<TblPlanning> TblPlanning { get; set; }
        public virtual DbSet<TblGebruiker> TblGebruiker { get; set; }

        public virtual DbSet<Nieuwsberichten> Nieuwsberichten { get; set; }

        public virtual DbSet<StudentenPerPlanning> StudentenPerPlannings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("server=localhost;database=GIP;trusted_connection=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TblFunctionaliteiten>(entity =>
            {
                entity.HasKey(e => e.FunctionaliteitId);

                entity.Property(e => e.FunctionaliteitId).HasColumnName("FunctionaliteitID");

                entity.Property(e => e.Beschrijving).IsUnicode(false);
            });

            modelBuilder.Entity<TblLessen>(entity =>
            {
                entity.HasKey(e => e.Lescode);

                entity.Property(e => e.Lescode)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Titel)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblLessenreeks>(entity =>
            {
                entity.HasKey(e => e.Reekscode);

                entity.Property(e => e.Reekscode)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Titel)
                    .IsRequired()
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblLokalen>(entity =>
            {
                entity.HasKey(e => e.Lokaalnummer);

                entity.Property(e => e.Lokaalnummer)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.FunctionaliteitenId).HasColumnName("FunctionaliteitenID");

                entity.Property(e => e.Locatie)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Functionaliteiten)
                    .WithMany(p => p.TblLokalen)
                    .HasForeignKey(d => d.FunctionaliteitenId)
                    .HasConstraintName("FK_TblFunctionaliteiten_TblLokalen");
            });

            modelBuilder.Entity<TblPlanning>(entity =>
            {
                entity.HasKey(e => e.PlanningId);

                entity.Property(e => e.PlanningId).HasColumnName("PlanningID");

                entity.Property(e => e.EindTijdstip)
                    .HasColumnName("Eind tijdstip")
                    .HasColumnType("smalldatetime");

                entity.Property(e => e.ExtraInfo)
                    .HasColumnName("Extra info")
                    .IsUnicode(false);

                entity.Property(e => e.Lescode)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Lokaalnummer)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Reekscode)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.StartTijdstip)
                    .HasColumnName("Start tijdstip")
                    .HasColumnType("smalldatetime");

                entity.HasOne(d => d.LescodeNavigation)
                    .WithMany(p => p.TblPlanning)
                    .HasForeignKey(d => d.Lescode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblLessen_TblPlanning");

                entity.HasOne(d => d.LokaalnummerNavigation)
                    .WithMany(p => p.TblPlanning)
                    .HasForeignKey(d => d.Lokaalnummer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblLokalen_TblPlanning");

                entity.HasOne(d => d.ReekscodeNavigation)
                    .WithMany(p => p.TblPlanning)
                    .HasForeignKey(d => d.Reekscode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblLessenreeks_TblPlanning");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public DbSet<LeerPlatform_Team2.Models.Inschrijvingen> Inschrijvingen { get; set; }
    }
}
