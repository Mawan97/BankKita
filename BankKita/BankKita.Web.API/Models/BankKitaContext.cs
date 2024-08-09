using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BankKita.Web.API.Models;

public partial class BankKitaContext : DbContext
{
    public BankKitaContext()
    {
    }

    public BankKitaContext(DbContextOptions<BankKitaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<JenisRekening> JenisRekenings { get; set; }

    public virtual DbSet<RekeningNasabah> RekeningNasabahs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-1M6Q53R1;Database=BankKita;User Id=westraw1536;Password=westrawaja;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<JenisRekening>(entity =>
        {
            entity.HasKey(e => e.JenisRekeningId).HasName("PK__JenisRek__99D2B734F0D57C9F");

            entity.ToTable("JenisRekening");

            entity.Property(e => e.Deskripsi)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<RekeningNasabah>(entity =>
        {
            entity.HasKey(e => e.RekeningNasabahId).HasName("PK__Rekening__CF71AD5CB1C21102");

            entity.ToTable("RekeningNasabah");

            entity.Property(e => e.NoRekening)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.Saldo).HasColumnType("money");
            entity.Property(e => e.TanggalBuka).HasColumnType("datetime");

            entity.HasOne(d => d.JenisRekening).WithMany(p => p.RekeningNasabahs)
                .HasForeignKey(d => d.JenisRekeningId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RekeningN__Jenis__267ABA7A");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
