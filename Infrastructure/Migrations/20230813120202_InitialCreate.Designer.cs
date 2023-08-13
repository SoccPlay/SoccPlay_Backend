﻿// <auto-generated />
using System;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(FootBall_PitchContext))]
    [Migration("20230813120202_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Domain.Entities.Account", b =>
                {
                    b.Property<Guid>("AccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("AccountID");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(2147483646)
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(2147483646)
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(2147483646)
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.HasKey("AccountId");

                    b.ToTable("Account", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Admin", b =>
                {
                    b.Property<Guid>("AdminId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("AdminId");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("AccountID");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(2147483646)
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(2147483646)
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(2147483646)
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(2147483646)
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(2147483646)
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.HasKey("AdminId");

                    b.HasIndex("AccountId");

                    b.ToTable("Admin", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Booking", b =>
                {
                    b.Property<Guid>("BookingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("BookingId");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateBooking")
                        .HasColumnType("datetime");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasMaxLength(2147483646)
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("TotalPrice")
                        .HasColumnType("real");

                    b.HasKey("BookingId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Booking", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Customer", b =>
                {
                    b.Property<Guid>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("CustomerId");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("AccountID");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(2147483646)
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(2147483646)
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(2147483646)
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(2147483646)
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(2147483646)
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.HasKey("CustomerId");

                    b.HasIndex("AccountId");

                    b.ToTable("Customer", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Feedback", b =>
                {
                    b.Property<Guid>("FeedbackId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("FeedbackId");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<Guid>("LandId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Rate")
                        .HasColumnType("int");

                    b.HasKey("FeedbackId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("LandId");

                    b.ToTable("Feedback", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Land", b =>
                {
                    b.Property<Guid>("LandId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("LandId");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(2147483646)
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(2147483646)
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<string>("NameLand")
                        .IsRequired()
                        .HasMaxLength(2147483646)
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Policy")
                        .IsRequired()
                        .HasMaxLength(2147483646)
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(2147483646)
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(2147483646)
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<int>("TotalPitch")
                        .HasColumnType("int");

                    b.HasKey("LandId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Land", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Owner", b =>
                {
                    b.Property<Guid>("OwnerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("OwnerId");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("AccountID");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(2147483646)
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(2147483646)
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(2147483646)
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(2147483646)
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(2147483646)
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.HasKey("OwnerId");

                    b.HasIndex("AccountId");

                    b.ToTable("Owner", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Pitch", b =>
                {
                    b.Property<Guid>("PitchId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("PitchId");

                    b.Property<Guid>("LandId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(2147483646)
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("PriceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Size")
                        .IsUnicode(false)
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(2147483646)
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.HasKey("PitchId");

                    b.HasIndex("LandId");

                    b.HasIndex("OwnerId");

                    b.HasIndex("PriceId");

                    b.ToTable("Pitch", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.PitchImage", b =>
                {
                    b.Property<Guid>("ImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ImageId");

                    b.Property<Guid>("LandId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(2147483646)
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.HasIndex("LandId");

                    b.ToTable("PitchImage", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Price", b =>
                {
                    b.Property<Guid>("PriceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("PriceId");

                    b.Property<int>("EndTime")
                        .HasColumnType("int");

                    b.Property<Guid>("LandLandId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("Price1")
                        .HasColumnType("real")
                        .HasColumnName("Price");

                    b.Property<int>("Size")
                        .HasColumnType("int")
                        .HasColumnName("Size");

                    b.Property<int>("StarTime")
                        .HasColumnType("int");

                    b.HasKey("PriceId");

                    b.HasIndex("LandLandId");

                    b.ToTable("Price", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Schedule", b =>
                {
                    b.Property<Guid>("ScheduleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ScheduleId");

                    b.Property<Guid>("BookingBookingId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime");

                    b.Property<Guid>("PitchPitchId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<DateTime>("StarTime")
                        .HasColumnType("datetime");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(2147483646)
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.HasKey("ScheduleId");

                    b.HasIndex("BookingBookingId");

                    b.HasIndex("PitchPitchId");

                    b.ToTable("Schedule", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Admin", b =>
                {
                    b.HasOne("Domain.Entities.Account", "Account")
                        .WithMany("Admins")
                        .HasForeignKey("AccountId")
                        .IsRequired()
                        .HasConstraintName("FKAdmin327316");

                    b.Navigation("Account");
                });

            modelBuilder.Entity("Domain.Entities.Booking", b =>
                {
                    b.HasOne("Domain.Entities.Customer", "Customer")
                        .WithMany("Bookings")
                        .HasForeignKey("CustomerId")
                        .IsRequired()
                        .HasConstraintName("FKBooking249093");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Domain.Entities.Customer", b =>
                {
                    b.HasOne("Domain.Entities.Account", "Account")
                        .WithMany("Customers")
                        .HasForeignKey("AccountId")
                        .IsRequired()
                        .HasConstraintName("FKCustomer31171");

                    b.Navigation("Account");
                });

            modelBuilder.Entity("Domain.Entities.Feedback", b =>
                {
                    b.HasOne("Domain.Entities.Customer", "Customer")
                        .WithMany("Feedbacks")
                        .HasForeignKey("CustomerId")
                        .IsRequired()
                        .HasConstraintName("FKFeedback523885");

                    b.HasOne("Domain.Entities.Land", "Land")
                        .WithMany("Feedbacks")
                        .HasForeignKey("LandId")
                        .IsRequired()
                        .HasConstraintName("FKFeedback81330");

                    b.Navigation("Customer");

                    b.Navigation("Land");
                });

            modelBuilder.Entity("Domain.Entities.Land", b =>
                {
                    b.HasOne("Domain.Entities.Owner", "Owner")
                        .WithMany("Lands")
                        .HasForeignKey("OwnerId")
                        .IsRequired()
                        .HasConstraintName("FKLand822092");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Domain.Entities.Owner", b =>
                {
                    b.HasOne("Domain.Entities.Account", "Account")
                        .WithMany("Owners")
                        .HasForeignKey("AccountId")
                        .IsRequired()
                        .HasConstraintName("FKOwner823493");

                    b.Navigation("Account");
                });

            modelBuilder.Entity("Domain.Entities.Pitch", b =>
                {
                    b.HasOne("Domain.Entities.Land", "Land")
                        .WithMany("Pitches")
                        .HasForeignKey("LandId")
                        .IsRequired()
                        .HasConstraintName("FKPitch63225");

                    b.HasOne("Domain.Entities.Owner", "Owner")
                        .WithMany("Pitches")
                        .HasForeignKey("OwnerId")
                        .IsRequired()
                        .HasConstraintName("FKPitch585708");

                    b.HasOne("Domain.Entities.Price", null)
                        .WithMany("Pitches")
                        .HasForeignKey("PriceId");

                    b.Navigation("Land");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Domain.Entities.PitchImage", b =>
                {
                    b.HasOne("Domain.Entities.Land", "Land")
                        .WithMany()
                        .HasForeignKey("LandId")
                        .IsRequired()
                        .HasConstraintName("FKPitchImage851248");

                    b.Navigation("Land");
                });

            modelBuilder.Entity("Domain.Entities.Price", b =>
                {
                    b.HasOne("Domain.Entities.Land", "LandLand")
                        .WithMany("Prices")
                        .HasForeignKey("LandLandId")
                        .IsRequired()
                        .HasConstraintName("FKPrice403220");

                    b.Navigation("LandLand");
                });

            modelBuilder.Entity("Domain.Entities.Schedule", b =>
                {
                    b.HasOne("Domain.Entities.Booking", "BookingBooking")
                        .WithMany("Schedules")
                        .HasForeignKey("BookingBookingId")
                        .IsRequired()
                        .HasConstraintName("FKSchedule603514");

                    b.HasOne("Domain.Entities.Pitch", "PitchPitch")
                        .WithMany("Schedules")
                        .HasForeignKey("PitchPitchId")
                        .IsRequired()
                        .HasConstraintName("FKSchedule967594");

                    b.Navigation("BookingBooking");

                    b.Navigation("PitchPitch");
                });

            modelBuilder.Entity("Domain.Entities.Account", b =>
                {
                    b.Navigation("Admins");

                    b.Navigation("Customers");

                    b.Navigation("Owners");
                });

            modelBuilder.Entity("Domain.Entities.Booking", b =>
                {
                    b.Navigation("Schedules");
                });

            modelBuilder.Entity("Domain.Entities.Customer", b =>
                {
                    b.Navigation("Bookings");

                    b.Navigation("Feedbacks");
                });

            modelBuilder.Entity("Domain.Entities.Land", b =>
                {
                    b.Navigation("Feedbacks");

                    b.Navigation("Pitches");

                    b.Navigation("Prices");
                });

            modelBuilder.Entity("Domain.Entities.Owner", b =>
                {
                    b.Navigation("Lands");

                    b.Navigation("Pitches");
                });

            modelBuilder.Entity("Domain.Entities.Pitch", b =>
                {
                    b.Navigation("Schedules");
                });

            modelBuilder.Entity("Domain.Entities.Price", b =>
                {
                    b.Navigation("Pitches");
                });
#pragma warning restore 612, 618
        }
    }
}
