using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EXE_Dotel.Models
{
    public partial class DotelDBContext : DbContext
    {
        public DotelDBContext()
        {
        }

        public DotelDBContext(DbContextOptions<DotelDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Rental> Rentals { get; set; } = null!;
        public virtual DbSet<RentalListImage> RentalListImages { get; set; } = null!;
        public virtual DbSet<RentalVideo> RentalVideos { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                optionsBuilder.UseSqlServer(config.GetConnectionString("ConnectionString"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rental>(entity =>
            {
                entity.ToTable("Rental");

                entity.Property(e => e.RentalId)
                    .ValueGeneratedNever()
                    .HasColumnName("rentalId");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.MaxPeople).HasColumnName("maxPeople");

                entity.Property(e => e.Price)
                    .HasColumnType("money")
                    .HasColumnName("price");

                entity.Property(e => e.RoomArea)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("roomArea");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Rentals)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Rental_UserId");
            });

            modelBuilder.Entity<RentalListImage>(entity =>
            {
                entity.HasKey(e => e.ImageId);

                entity.ToTable("RentalListImage");

                entity.Property(e => e.ImageId)
                    .ValueGeneratedNever()
                    .HasColumnName("imageId");

                entity.Property(e => e.RentalId).HasColumnName("rentalId");

                entity.Property(e => e.Sourse)
                    .HasMaxLength(1000)
                    .HasColumnName("sourse");

                entity.HasOne(d => d.Rental)
                    .WithMany(p => p.RentalListImages)
                    .HasForeignKey(d => d.RentalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RentalListImage_Rental");
            });

            modelBuilder.Entity<RentalVideo>(entity =>
            {
                entity.HasKey(e => e.VideoId);

                entity.ToTable("RentalVideo");

                entity.Property(e => e.VideoId)
                    .ValueGeneratedNever()
                    .HasColumnName("videoId");

                entity.Property(e => e.RentalId).HasColumnName("rentalId");

                entity.Property(e => e.Sourse)
                    .HasMaxLength(1000)
                    .HasColumnName("sourse");

                entity.HasOne(d => d.Rental)
                    .WithMany(p => p.RentalVideos)
                    .HasForeignKey(d => d.RentalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RentalVideo_Rental");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.UserId)
                    .ValueGeneratedNever()
                    .HasColumnName("userId");

                entity.Property(e => e.Email)
                    .HasMaxLength(30)
                    .HasColumnName("email");

                entity.Property(e => e.Fullname)
                    .HasMaxLength(50)
                    .HasColumnName("fullname");

                entity.Property(e => e.MainPhoneNumber)
                    .HasMaxLength(20)
                    .HasColumnName("mainPhoneNumber");

                entity.Property(e => e.Password)
                    .HasMaxLength(30)
                    .HasColumnName("password");

                entity.Property(e => e.SecondaryPhoneNumber)
                    .HasMaxLength(20)
                    .HasColumnName("secondaryPhoneNumber");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
