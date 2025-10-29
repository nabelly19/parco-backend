using System;
using System.Collections.Generic;

namespace ParcoBackend.Model;

public partial class User
{
    public Guid Id { get; set; }

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public byte[]? Profilepic { get; set; }

    public string Email { get; set; } = null!;

    public string Phonenumber { get; set; } = null!;

    public string Usercategory { get; set; } = null!;

    public string? Nationalid { get; set; }

    public string Password { get; set; } = null!;

    public DateTime? Created { get; set; }

    public DateTime? Updated { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<Parkingspace> Parkingspaces { get; set; } = new List<Parkingspace>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
