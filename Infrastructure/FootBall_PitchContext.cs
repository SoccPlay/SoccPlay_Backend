using System;
using System.Collections.Generic;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Infrastructure.Entities
{
    public partial class FootBall_PitchContext : DbContext
    {
        public FootBall_PitchContext()
        {
        }

        public FootBall_PitchContext(DbContextOptions<FootBall_PitchContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<Admin> Admins { get; set; } = null!;
        public virtual DbSet<Booking> Bookings { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Feedback> Feedbacks { get; set; } = null!;
        public virtual DbSet<Land> Lands { get; set; } = null!;
        public virtual DbSet<Owner> Owners { get; set; } = null!;
        public virtual DbSet<Pitch> Pitches { get; set; } = null!;
        public virtual DbSet<PitchImage> PitchImages { get; set; } = null!;
        public virtual DbSet<Price> Prices { get; set; } = null!;
        public virtual DbSet<Schedule> Schedules { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https: //go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
               optionsBuilder.UseSqlServer("Data Source=mssql-139728-0.cloudclusters.net,19689;Initial Catalog=FootBall;User ID=admin;pwd=sMZHCqF7;TrustServerCertificate=True;MultipleActiveResultSets=true");
               //optionsBuilder.UseSqlServer("Data Source=NhaPhan;Initial Catalog=11;User ID=sa;pwd=1;TrustServerCertificate=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.Property(e => e.AccountId).ValueGeneratedOnAdd().HasColumnName("AccountID");

                entity.Property(e => e.Password)
                    .HasMaxLength(int.MaxValue - 1)
                    .IsUnicode(false);

                entity.Property(e => e.Role)
                    .HasMaxLength(int.MaxValue - 1)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(int.MaxValue - 1)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("Admin");
                entity.Property(e => e.AdminId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("AdminId");
                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.Address)
                    .HasMaxLength(int.MaxValue - 1)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(int.MaxValue - 1)
                    .IsUnicode(false);

                entity.Property(e => e.FullName)
                    .HasMaxLength(int.MaxValue - 1)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(int.MaxValue - 1)

                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(int.MaxValue - 1)
                    .IsUnicode(false);

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Admins)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKAdmin327316");
            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.ToTable("Booking");
                entity.Property(e => e.BookingId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("BookingId");
                entity.Property(e => e.DateBooking).HasColumnType("datetime");

                entity.Property(e => e.Note)
                    .HasMaxLength(int.MaxValue - 1)
                    .IsUnicode(false);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKBooking249093");

                entity.HasOne(d => d.Land)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.LandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKBooking362392");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");
                entity.Property(e => e.CustomerId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("CustomerId");
                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.Address)
                    .HasMaxLength(int.MaxValue - 1)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(int.MaxValue - 1)
                    .IsUnicode(false);

                entity.Property(e => e.FullName)
                    .HasMaxLength(int.MaxValue - 1)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(int.MaxValue - 1)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(int.MaxValue - 1)
                    .IsUnicode(false);

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKCustomer31171");
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.ToTable("Feedback");
                entity.Property(e => e.FeedbackId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("FeedbackId");
                entity.Property(e => e.Description).IsUnicode(false);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKFeedback523885");

                entity.HasOne(d => d.Land)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.LandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKFeedback81330");
            });

            modelBuilder.Entity<Land>(entity =>
            {
                entity.ToTable("Land");
                entity.Property(e => e.LandId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("LandId");
                entity.Property(e => e.Description)
                    .HasMaxLength(int.MaxValue - 1)
                    .IsUnicode(false);

                entity.Property(e => e.Policy)
                    .HasMaxLength(int.MaxValue - 1)
                    .IsUnicode(false);
                
                entity.Property(e => e.Location)
                    .HasMaxLength(int.MaxValue - 1)
                    .IsUnicode(false);

                entity.Property(e => e.NameLand)
                    .HasMaxLength(int.MaxValue - 1)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(int.MaxValue - 1)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasMaxLength(int.MaxValue - 1)
                    .IsUnicode(false);

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.Lands)
                    .HasForeignKey(d => d.OwnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKLand822092");
                
                entity.HasMany(d => d.Images)
                    .WithOne(p => p.Land)
                    .HasForeignKey(d => d.ImageId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
                
                
                entity.HasMany(l => l.Bookings)
                    .WithOne(b => b.Land)
                    .HasForeignKey(b => b.LandId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Owner>(entity =>
            {
                entity.ToTable("Owner");
                entity.Property(e => e.OwnerId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("OwnerId");
                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.Address)
                    .HasMaxLength(int.MaxValue - 1)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(int.MaxValue - 1)
                    .IsUnicode(false);

                entity.Property(e => e.FullName)
                    .HasMaxLength(int.MaxValue - 1)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(int.MaxValue - 1)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(int.MaxValue - 1)
                    .IsUnicode(false);

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Owners)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKOwner823493");
                
            });

            modelBuilder.Entity<Pitch>(entity =>
            {
                entity.ToTable("Pitch");
                entity.Property(e => e.PitchId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("PitchId");
                entity.Property(e => e.Name)
                    .HasMaxLength(int.MaxValue - 1)
                    .IsUnicode(false);

                entity.Property(e => e.Size)
                    .HasColumnType(("int"))
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(int.MaxValue - 1)
                    .IsUnicode(false);

                entity.HasOne(d => d.Land)
                    .WithMany(p => p.Pitches)
                    .HasForeignKey(d => d.LandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKPitch63225");

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.Pitches)
                    .HasForeignKey(d => d.OwnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKPitch585708");
            });

            modelBuilder.Entity<PitchImage>(entity =>
            {
                entity.ToTable("PitchImage");
                entity.HasKey(e => e.ImageId); // Đặt ImageId là primary key
                entity.Property(e => e.ImageId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ImageId");

                entity.Property(e => e.Name)
                    .HasMaxLength(int.MaxValue - 1)
                    .IsUnicode(false);

                entity.HasOne(d => d.Land)
                    .WithMany(d=>d.Images)
                    .HasForeignKey(d => d.LandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKPitchImage851248");
            });

            modelBuilder.Entity<Price>(entity =>
            {
                entity.ToTable("Price");
                entity.Property(e => e.PriceId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("PriceId");
                entity.Property(e => e.EndTime).HasColumnType("int");

                entity.Property(e => e.Price1).HasColumnName("Price");

                entity.Property(e => e.Size).HasColumnName("Size");

                entity.Property(e => e.StarTime).HasColumnType("int");

                entity.HasOne(d => d.LandLand)
                    .WithMany(p => p.Prices)
                    .HasForeignKey(d => d.LandLandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKPrice403220");
            });

            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.ToTable("Schedule");
                entity.Property(e => e.ScheduleId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ScheduleId");
                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.StarTime).HasColumnType("datetime");

                entity.Property(e => e.Status)
                    .HasMaxLength(int.MaxValue - 1)
                    .IsUnicode(false);

                entity.HasOne(d => d.BookingBooking)
                    .WithMany(p => p.Schedules)
                    .HasForeignKey(d => d.BookingBookingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKSchedule603514");

                entity.HasOne(d => d.PitchPitch)
                    .WithMany(p => p.Schedules)
                    .HasForeignKey(d => d.PitchPitchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKSchedule967594");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}