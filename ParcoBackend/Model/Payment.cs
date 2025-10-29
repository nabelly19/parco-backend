using System;
using System.Collections.Generic;

namespace ParcoBackend.Model;

public partial class Payment
{
    public Guid Id { get; set; }

    public Guid Bookingid { get; set; }

    public Guid Motoristid { get; set; }

    public Guid Parkingspaceid { get; set; }

    public decimal Amount { get; set; }

    public string Status { get; set; } = null!;

    public string Paymentmethod { get; set; } = null!;

    public string? Transactionid { get; set; }

    public DateTime Paymentdate { get; set; }

    public DateTime? Created { get; set; }

    public DateTime? Updated { get; set; }

    public virtual Booking Booking { get; set; } = null!;

    public virtual User Motorist { get; set; } = null!;

    public virtual Parkingspace Parkingspace { get; set; } = null!;
}
