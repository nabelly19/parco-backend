using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using DTO;
using ParcoBackend.Model;
using System.Data.Common;

namespace ParcoBackend.Services;

public class VehicleService : IVehicleService
{
    ParcoDbContext _context;

    public VehicleService(ParcoDbContext context)
    {
        _context = context;
    }

    public async Task<VehicleReadDto> CreateAsync(VehicleReadDto dto)
    {
        var exists = await _context.Vehicles.
        AnyAsync(v => v.VehiclePlate.ToLower() == dto.VehiclePlate.ToLower());

        if (exists)
            throw new InvalidOperationException("Já existe um veículo com esta placa cadastrado.");

        var vehicle = new Vehicle
        {
            Id = Guid.NewGuid(),
            Userid = dto.Userid,
            VehicleModel = dto.VehicleModel,
            VehiclePlate = dto.VehiclePlate,
            Created = DateTime.Now,
        };

        _context.Vehicles.Add(vehicle);
        await _context.SaveChangesAsync();

        return new VehicleReadDto
        {
            Id = vehicle.Id,
            UserId = vehicle.UserId,
            VehicleModel = vehicle.VehicleModel,
            VehiclePlate = vehicle.VehiclePlate,
            Created = vehicle.Created ?? DateTime.UtcNow,
        };
    }

    //GetAll
    public async Task<IEnumerable<VehicleReadDto>> GetAllAsync()
    {
        var vehicles = await _context.Vehicles
            .AsNoTracking()
            .OrderBy(v => v.VehiclePlate)
            .ToListAsync();

        return vehicles.Select(vehicles => new VehicleReadDto
        {
            IdentifierCase = vehicles.Id,
            UserId = vehicles.UserId,
            VehicleModel = vehicles.VehicleModel,
            VehiclePlate = vehicles.VehiclePlate,
            Created = vehicles.Created ?? DateTime.UtcNow,
            Update = vehicles.Update,
        });
    }

    //GetById
    public async Task<VehicleReadDto?> GetByIdAsync(int id)
    {
        var vehicle = await _context.Vehicle
            .AsNoTracking()
            .FirstOrDefaultAsync(v => v.Id == id);

        if (vehicle == null) return "Veículo não encontrado";

        return new VehicleReadDto
        {
            Id = vehicle.Id,
            UserId = vehicle.Userid,
            VehicleModel = vehicle.Vehiclemodel,
            VehiclePlate = vehicle.Vehicleplate,
            Created = vehicle.Created ?? DateTime.UtcNow,
            Updated = vehicle.Updated
        };
    }

    public async Task<VehicleReadDto?> UpdateAsync(Guid id, VehicleCreateDto dto)
    {
        var vehicle = await _context.Vehicle.FindAsync(id);

        if (vehicle == null) return "Veículo não encontrado";

        vehicle.VehicleModel = dto.VehicleModel;
        vehicle.VehiclePlate = dto.VehiclePlate;
        vehicle.UserId = dto.UserId;
        vehicle.Updated = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return new VehicleReadDto
        {
            Id = vehicle.Id,
            UserId = vehicle.Userid,
            VehicleModel = vehicle.Vehiclemodel,
            VehiclePlate = vehicle.Vehicleplate,
            Created = vehicle.Created ?? DateTime.UtcNow,
            Updated = vehicle.Updated
        };
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var vehicle = await _context.Vehicles.FindAsync(id);

        if (vehicle == null)
            return false;

        _context.Vehicles.Remove(vehicle);
        await _context.SaveChangesAsync();
        return true;
    }
}

