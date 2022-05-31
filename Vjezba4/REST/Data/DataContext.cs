using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Transakcije.Models;

namespace Transakcije.Data
{
    public partial class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Korisnik> Korisnik { get; set; } = null!;
        public virtual DbSet<Uplatnica> Uplatnicas { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Korisnik>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("korisnik");

                entity.Property(e => e.Grad).HasColumnName("grad");

                entity.Property(e => e.IdKorisnika).HasColumnName("idKorisnika");

                entity.Property(e => e.Ime).HasColumnName("ime");
            });

            modelBuilder.Entity<Uplatnica>(entity =>
            {
                entity.HasKey(e => e.IdUplatnica);

                entity.ToTable("uplatnica");

                entity.Property(e => e.IdUplatnica).HasColumnName("idUplatnica");

                entity.Property(e => e.Iban)
                    .HasColumnType("VARCHAR(45)")
                    .HasColumnName("iban");

                entity.Property(e => e.Iznos).HasColumnName("iznos");

                entity.Property(e => e.Platitelj)
                    .HasColumnType("VARCHAR(45)")
                    .HasColumnName("platitelj");

                entity.Property(e => e.Primatelj)
                    .HasColumnType("VARCHAR(45)")
                    .HasColumnName("primatelj");

                entity.Property(e => e.Valuta)
                    .HasColumnType("VARCHAR(45)")
                    .HasColumnName("valuta");

                entity.Property(e => e.VrijemeUplate)
                    .HasColumnType("DATETIME(45)")
                    .HasColumnName("vrijemeUplate");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
