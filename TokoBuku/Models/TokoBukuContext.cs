using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace TokoBuku.Models
{
    public partial class TokoBukuContext : DbContext
    {
        public TokoBukuContext()
        {
        }

        public TokoBukuContext(DbContextOptions<TokoBukuContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Buku> Bukus { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Transaksi> Transaksis { get; set; }
        public virtual DbSet<TransaksiDetail> TransaksiDetails { get; set; }
        public virtual DbSet<User> Users { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Server=DESKTOP-ECRNKVS;Database=TokoBuku;user id=sa;password=Bismillah12;");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Buku>(entity =>
            {
                entity.HasKey(e => e.KodeBuku);

                entity.ToTable("Buku");

                entity.Property(e => e.KodeBuku)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Kode_Buku");

                entity.Property(e => e.CreatedBy).HasColumnName("Created_By");

                entity.Property(e => e.Deskripsi).HasColumnType("text");

                entity.Property(e => e.Harga).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Judul)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Penerbit)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.Bukus)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Buku_User");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.IdRole);

                entity.ToTable("Role");

                entity.Property(e => e.IdRole).HasColumnName("Id_Role");

                entity.Property(e => e.NamaRole)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Nama_Role");
            });

            modelBuilder.Entity<Transaksi>(entity =>
            {
                entity.HasKey(e => e.KodeTransaksi);

                entity.ToTable("Transaksi");

                entity.Property(e => e.KodeTransaksi)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Kode_Transaksi");

                entity.Property(e => e.IdUser).HasColumnName("Id_User");

                entity.Property(e => e.Tanggal).HasColumnType("datetime");

                entity.Property(e => e.TotalNominal)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("Total_Nominal");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Transaksis)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transaksi_User");
            });

            modelBuilder.Entity<TransaksiDetail>(entity =>
            {
                entity.HasKey(e => e.IdTransaksiDetail);

                entity.ToTable("Transaksi_Detail");

                entity.Property(e => e.IdTransaksiDetail)
                    .ValueGeneratedNever()
                    .HasColumnName("Id_Transaksi_Detail");

                entity.Property(e => e.KodeBuku)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Kode_Buku");

                entity.Property(e => e.KodeTransaksi)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Kode_Transaksi");

                entity.HasOne(d => d.KodeBukuNavigation)
                    .WithMany(p => p.TransaksiDetails)
                    .HasForeignKey(d => d.KodeBuku)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transaksi_Detail_Buku");

                entity.HasOne(d => d.KodeTransaksiNavigation)
                    .WithMany(p => p.TransaksiDetails)
                    .HasForeignKey(d => d.KodeTransaksi)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transaksi_Detail_Transaksi");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser);

                entity.ToTable("User");

                entity.Property(e => e.IdUser).HasColumnName("Id_User");

                entity.Property(e => e.Alamat)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdRole).HasColumnName("Id_Role");

                entity.Property(e => e.Nama)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NoTelp)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("No_Telp");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdRoleNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.IdRole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_User");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
