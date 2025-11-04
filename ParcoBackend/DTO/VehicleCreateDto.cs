using System.ComponentModel.DataAnnotations;

namespace DTO;

public class VehicleCreateDto
{
    [Required]
    public Guid UserId { get; set; }

    [Required]
    [StringLength(255, MinimumLength = 2, ErrorMessage = "O modelo do veículo deve ter entre 2 e 255 caracteres.")]
    public string VehicleModel { get; set; } = string.Empty;

    [Required]
    [StringLength(7, ErrorMessage = "A placa deve ter entre 7 caracteres alfanuméricos")]
    public string VehiclePlate { get; set; } = string.Empty;
}