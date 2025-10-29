using System;
using System.Collections.Generic;

namespace ParcoBackend.Model;

public partial class Parkingslot
{
    public Guid Id { get; set; }

    public Guid Parkingspaceid { get; set; }

    public string Slotnumber { get; set; } = null!;

    public bool Isocuppied { get; set; }

    public string Slottype { get; set; } = null!;

    public int? Slotfloor { get; set; }

    public DateTime? Created { get; set; }

    public DateTime? Updated { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Parkingspace Parkingspace { get; set; } = null!;
}
