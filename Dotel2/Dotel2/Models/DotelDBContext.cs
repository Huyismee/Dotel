using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Dotel2.Models
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

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Rental> Rentals { get; set; }
        public virtual DbSet<RentalListImage> RentalListImages { get; set; }
        public virtual DbSet<RentalVideo> RentalVideos { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<SponsorRental> SponsorRentals { get; set; }
        public virtual DbSet<Sponsorship> Sponsorships { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                optionsBuilder.UseSqlServer(config.GetConnectionString("ConnectionString"));
                
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("Admin");

                entity.Property(e => e.AdminId).HasColumnName("adminId");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Fullname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("fullname");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasColumnName("password");

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("phoneNumber");

                entity.Property(e => e.RoleId).HasColumnName("roleId");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Admins)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_Admin_Role");
            });

            modelBuilder.Entity<Rental>(entity =>
            {
                entity.ToTable("Rental");

                entity.Property(e => e.RentalId).HasColumnName("rentalId");

                entity.Property(e => e.Approval).HasColumnName("approval");

                entity.Property(e => e.Bathroom).HasColumnName("bathroom");

                entity.Property(e => e.BedroomNumber).HasColumnName("bedroomNumber");

                entity.Property(e => e.ContactPhoneNumber)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("contactPhoneNumber");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.GoogleMap)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("googleMap");

                entity.Property(e => e.Kitchen).HasColumnName("kitchen");

                entity.Property(e => e.Location)
                    .IsUnicode(false)
                    .HasColumnName("location");

                entity.Property(e => e.MaxPeople).HasColumnName("maxPeople");

                entity.Property(e => e.Price)
                    .HasColumnType("money")
                    .HasColumnName("price");

                entity.Property(e => e.RentalTitle)
                    .IsRequired()
                    .HasMaxLength(300)
                    .HasColumnName("rentalTitle");

                entity.Property(e => e.RoomArea)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("roomArea");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Type)
                    .HasMaxLength(50)
                    .HasColumnName("type");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.Property(e => e.ViewNumber).HasColumnName("viewNumber");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Rentals)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Rental_UserId");
            });

            modelBuilder.Entity<RentalListImage>(entity =>
            {
                entity.HasKey(e => e.ImageId);

                entity.ToTable("RentalListImage");

                entity.Property(e => e.ImageId).HasColumnName("imageId");

                entity.Property(e => e.RentalId).HasColumnName("rentalId");

                entity.Property(e => e.Sourse)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .HasColumnName("sourse");

                entity.HasOne(d => d.Rental)
                    .WithMany(p => p.RentalListImages)
                    .HasForeignKey(d => d.RentalId)
                    .HasConstraintName("FK_RentalListImage_Rental");
            });

            modelBuilder.Entity<RentalVideo>(entity =>
            {
                entity.HasKey(e => e.VideoId);

                entity.ToTable("RentalVideo");

                entity.Property(e => e.VideoId).HasColumnName("videoId");

                entity.Property(e => e.RentalId).HasColumnName("rentalId");

                entity.Property(e => e.Sourse)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .HasColumnName("sourse");

                entity.HasOne(d => d.Rental)
                    .WithMany(p => p.RentalVideos)
                    .HasForeignKey(d => d.RentalId)
                    .HasConstraintName("FK_RentalVideo_Rental");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.RoleId).HasColumnName("roleId");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("roleName");
            });

            modelBuilder.Entity<SponsorRental>(entity =>
            {
                entity.ToTable("SponsorRental");

                entity.Property(e => e.SponsorRentalId).HasColumnName("sponsorRentalId");

                entity.Property(e => e.RentalId).HasColumnName("rentalId");

                entity.Property(e => e.SponsorId).HasColumnName("sponsorId");

                entity.HasOne(d => d.Rental)
                    .WithMany(p => p.SponsorRentals)
                    .HasForeignKey(d => d.RentalId)
                    .HasConstraintName("FK_SponsorRental_Rental");

                entity.HasOne(d => d.Sponsor)
                    .WithMany(p => p.SponsorRentals)
                    .HasForeignKey(d => d.SponsorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SponsorRental_Sponsor");
            });

            modelBuilder.Entity<Sponsorship>(entity =>
            {
                entity.HasKey(e => e.SponsorId);

                entity.ToTable("Sponsorship");

                entity.Property(e => e.SponsorId).HasColumnName("sponsorId");

                entity.Property(e => e.SponsorDes)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .HasColumnName("sponsorDes");

                entity.Property(e => e.SponsorName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("sponsorName");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Fullname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("fullname");

                entity.Property(e => e.MainPhoneNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("mainPhoneNumber");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.RoleId).HasColumnName("roleId");

                entity.Property(e => e.SecondaryPhoneNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("secondaryPhoneNumber");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_User_Role");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
