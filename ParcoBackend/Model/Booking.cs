using System;
using System.Collections.Generic;

namespace ParcoBackend.Model;

public partial class Booking
{
    public Guid Id { get; set; }

    public Guid Motoristid { get; set; }

    public Guid Parkingslotid { get; set; }

    public Guid Parkingspaceid { get; set; }

    public DateOnly Reservationdate { get; set; }

    public DateTime Fromtime { get; set; }

    public DateTime Totime { get; set; }

    public string Vehiclemodel { get; set; } = null!;

    public string Vehicleplate { get; set; } = null!;

    public int Parkingduration { get; set; }

    public decimal Totalfees { get; set; }

    public string Status { get; set; } = null!;

    public DateTime? Created { get; set; }

    public DateTime? Updated { get; set; }

    public virtual User Motorist { get; set; } = null!;

    public virtual Parkingslot Parkingslot { get; set; } = null!;

    public virtual Parkingspace Parkingspace { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
