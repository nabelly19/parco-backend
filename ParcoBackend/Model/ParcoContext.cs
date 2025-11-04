using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ParcoBackend.Model;

public partial class ParcoContext : DbContext
{
    public ParcoContext()
    {
    }

    public ParcoContext(DbContextOptions<ParcoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Parkingslot> Parkingslots { get; set; }

    public virtual DbSet<Parkingspace> Parkingspaces { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Vehicle> Vehicles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=Parco;Username=postgres;Password=1234");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("uuid-ossp");

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("booking_pkey");

            entity.ToTable("booking");

            entity.HasIndex(e => new { e.Parkingslotid, e.Fromtime, e.Totime }, "uq_slot_time").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("created");
            entity.Property(e => e.Fromtime).HasColumnName("fromtime");
            entity.Property(e => e.Motoristid).HasColumnName("motoristid");
            entity.Property(e => e.Parkingduration).HasColumnName("parkingduration");
            entity.Property(e => e.Parkingslotid).HasColumnName("parkingslotid");
            entity.Property(e => e.Parkingspaceid).HasColumnName("parkingspaceid");
            entity.Property(e => e.Reservationdate).HasColumnName("reservationdate");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValueSql("'Pendente'::character varying")
                .HasColumnName("status");
            entity.Property(e => e.Totalfees)
                .HasPrecision(10, 2)
                .HasColumnName("totalfees");
            entity.Property(e => e.Totime).HasColumnName("totime");
            entity.Property(e => e.Updated).HasColumnName("updated");
            entity.Property(e => e.Vehiclemodel)
                .HasMaxLength(255)
                .HasColumnName("vehiclemodel");
            entity.Property(e => e.Vehicleplate)
                .HasMaxLength(255)
                .HasColumnName("vehicleplate");

            entity.HasOne(d => d.Motorist).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.Motoristid)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_motorist");

            entity.HasOne(d => d.Parkingslot).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.Parkingslotid)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_parking_slot");

            entity.HasOne(d => d.Parkingspace).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.Parkingspaceid)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_parking_space");
        });

        modelBuilder.Entity<Parkingslot>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("parkingslot_pkey");

            entity.ToTable("parkingslot");

            entity.HasIndex(e => new { e.Parkingspaceid, e.Slotnumber }, "uq_slot_number_per_lot").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("created");
            entity.Property(e => e.Isocuppied).HasColumnName("isocuppied");
            entity.Property(e => e.Parkingspaceid).HasColumnName("parkingspaceid");
            entity.Property(e => e.Slotfloor)
                .HasDefaultValueSql("1")
                .HasColumnName("slotfloor");
            entity.Property(e => e.Slotnumber)
                .HasMaxLength(20)
                .HasColumnName("slotnumber");
            entity.Property(e => e.Slottype)
                .HasMaxLength(50)
                .HasDefaultValueSql("'Comum'::character varying")
                .HasColumnName("slottype");
            entity.Property(e => e.Updated).HasColumnName("updated");

            entity.HasOne(d => d.Parkingspace).WithMany(p => p.Parkingslots)
                .HasForeignKey(d => d.Parkingspaceid)
                .HasConstraintName("fk_parking_lot");
        });

        modelBuilder.Entity<Parkingspace>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("parkingspace_pkey");

            entity.ToTable("parkingspace");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.City)
                .HasMaxLength(255)
                .HasColumnName("city");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("created");
            entity.Property(e => e.Hourlyfee)
                .HasPrecision(10, 2)
                .HasColumnName("hourlyfee");
            entity.Property(e => e.Image1).HasColumnName("image1");
            entity.Property(e => e.Image2).HasColumnName("image2");
            entity.Property(e => e.Latitude).HasColumnName("latitude");
            entity.Property(e => e.Longitude).HasColumnName("longitude");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Ownerid).HasColumnName("ownerid");
            entity.Property(e => e.Street)
                .HasMaxLength(255)
                .HasColumnName("street");
            entity.Property(e => e.Totalcapacity).HasColumnName("totalcapacity");
            entity.Property(e => e.Updated).HasColumnName("updated");

            entity.HasOne(d => d.Owner).WithMany(p => p.Parkingspaces)
                .HasForeignKey(d => d.Ownerid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_owner");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("payments_pkey");

            entity.ToTable("payments");

            entity.HasIndex(e => e.Transactionid, "payments_transactionid_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasPrecision(10, 2)
                .HasColumnName("amount");
            entity.Property(e => e.Bookingid).HasColumnName("bookingid");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("created");
            entity.Property(e => e.Motoristid).HasColumnName("motoristid");
            entity.Property(e => e.Parkingspaceid).HasColumnName("parkingspaceid");
            entity.Property(e => e.Paymentdate).HasColumnName("paymentdate");
            entity.Property(e => e.Paymentmethod)
                .HasMaxLength(50)
                .HasColumnName("paymentmethod");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValueSql("'Sucesso'::character varying")
                .HasColumnName("status");
            entity.Property(e => e.Transactionid)
                .HasMaxLength(255)
                .HasColumnName("transactionid");
            entity.Property(e => e.Updated).HasColumnName("updated");

            entity.HasOne(d => d.Booking).WithMany(p => p.Payments)
                .HasForeignKey(d => d.Bookingid)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_booking");

            entity.HasOne(d => d.Motorist).WithMany(p => p.Payments)
                .HasForeignKey(d => d.Motoristid)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_motorist");

            entity.HasOne(d => d.Parkingspace).WithMany(p => p.Payments)
                .HasForeignKey(d => d.Parkingspaceid)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_parking_space");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("User_pkey");

            entity.ToTable("User");

            entity.HasIndex(e => e.Email, "User_email_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("created");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Firstname)
                .HasMaxLength(255)
                .HasColumnName("firstname");
            entity.Property(e => e.Lastname)
                .HasMaxLength(255)
                .HasColumnName("lastname");
            entity.Property(e => e.Nationalid)
                .HasMaxLength(50)
                .HasColumnName("nationalid");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Phonenumber)
                .HasMaxLength(50)
                .HasColumnName("phonenumber");
            entity.Property(e => e.Profilepic).HasColumnName("profilepic");
            entity.Property(e => e.Updated).HasColumnName("updated");
            entity.Property(e => e.Usercategory)
                .HasMaxLength(50)
                .HasColumnName("usercategory");
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("vehicle_pkey");

            entity.ToTable("vehicle");

            entity.HasIndex(e => e.Vehicleplate, "vehicle_vehicleplate_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("created");
            entity.Property(e => e.Updated).HasColumnName("updated");
            entity.Property(e => e.Userid).HasColumnName("userid");
            entity.Property(e => e.Vehiclemodel)
                .HasMaxLength(255)
                .HasColumnName("vehiclemodel");
            entity.Property(e => e.Vehicleplate)
                .HasMaxLength(20)
                .HasColumnName("vehicleplate");

            entity.HasOne(d => d.User).WithMany(p => p.Vehicles)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_user");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
