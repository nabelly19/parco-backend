using System;
using System.Collections.Generic;

namespace ParcoBackend.Model;

public partial class Vehicle
{
    public Guid Id { get; set; }

    public Guid Userid { get; set; }

    public string Vehiclemodel { get; set; } = null!;

    public string Vehicleplate { get; set; } = null!;

    public DateTime? Created { get; set; }

    public DateTime? Updated { get; set; }

    public virtual User User { get; set; } = null!;
}
