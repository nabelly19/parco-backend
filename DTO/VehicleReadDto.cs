namespace DTO

public class VehicleReadDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string VehicleModel { get; set; } = string.Empty;
    public string VehiclePlate { get; set; } = string.Empty;
    public DateTime Created { get; set; }
    public DateTIme? Updated { get; set; } 
}