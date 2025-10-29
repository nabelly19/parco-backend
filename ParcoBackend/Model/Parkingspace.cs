using System;
using System.Collections.Generic;

namespace ParcoBackend.Model;

public partial class Parkingspace
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public Guid Ownerid { get; set; }

    public int Totalcapacity { get; set; }

    public decimal Hourlyfee { get; set; }

    public string City { get; set; } = null!;

    public string Street { get; set; } = null!;

    public decimal? Latitude { get; set; }

    public decimal? Longitude { get; set; }

    public byte[]? Image1 { get; set; }

    public byte[]? Image2 { get; set; }

    public DateTime? Created { get; set; }

    public DateTime? Updated { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual User Owner { get; set; } = null!;

    public virtual ICollection<Parkingslot> Parkingslots { get; set; } = new List<Parkingslot>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
