using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using ParcoBackend.Model;
using DTO;

namespace ParcoBackend.Services;

public interface IVehicleservice 
{
    Task<IEnumerable<VehicleReadDto>> GetAllAsync();
    Task<VehicleReadDto?> GetByIdAsync(Guid id);
    Task<VehicleReadDto> CreateAsync(VehicleCreateDto dto);
    Task<VehicleReadDto?> UpdateAsync(Guid id, VehicleCreateDto dto);
    Task<bool> DeleteAsync(Guid id);
}