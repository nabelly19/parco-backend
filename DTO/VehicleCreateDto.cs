namespace DTO

public class VehicleCreateDto
{
    public Guid UserID { get; set; }
    public string VehicleModel { get; set; } = string.Empty;
    public string VehiclePlate { get; set; } = string.Empty;
}