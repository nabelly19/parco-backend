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
    ParcoContext _context;

    public VehicleService(ParcoContext context)
    {
        _context = context;
    }


    //Create
    public async Task<VehicleReadDto> CreateAsync(VehicleCreateDto dto)
    {
        var userExists = await _context.Users.AnyAsync(u => u.Id == dto.UserId);
        if (!userExists)
            throw new ArgumentException("Usuário associado não encontrado.");

        var normalizedPlate = dto.VehiclePlate.Trim().ToUpper();

        var plateExists = await _context.Vehicles
            .AnyAsync(v => v.Vehicleplate.ToLower() == normalizedPlate.ToLower());

        if (plateExists)
          throw new ArgumentException("Já existe um veículo com esta placa cadastrado.");

        var vehicle = new Vehicle
        {
            Id = Guid.NewGuid(),
            Userid = dto.UserId,
            Vehiclemodel = dto.VehicleModel.Trim(),
            Vehicleplate = normalizedPlate,
            Created = DateTime.UtcNow
        };

        _context.Vehicles.Add(vehicle);
        await _context.SaveChangesAsync();

        return new VehicleReadDto
        {
            Id = vehicle.Id,
            UserId = vehicle.Userid,
            VehicleModel = vehicle.Vehiclemodel,
            VehiclePlate = vehicle.Vehicleplate,
            Created = vehicle.Created ?? DateTime.UtcNow,
        };
    }

    //GetAll
    public async Task<IEnumerable<VehicleReadDto>> GetAllAsync()
    {
        var vehicles = await _context.Vehicles
            .AsNoTracking()
            .OrderBy(v => v.Vehicleplate)
            .ToListAsync();

        return vehicles.Select(vehicles => new VehicleReadDto
        {
            Id = vehicles.Id,
            UserId = vehicles.Userid,
            VehicleModel = vehicles.Vehiclemodel,
            VehiclePlate = vehicles.Vehicleplate,
            Created = vehicles.Created ?? DateTime.UtcNow,
            Updated = vehicles.Updated,
        });
    }

    //GetById
    public async Task<VehicleReadDto?> GetByIdAsync(Guid id)
    {
        var vehicle = await _context.Vehicles
            .AsNoTracking()
            .FirstOrDefaultAsync(v => v.Id == id);

        if (vehicle == null) return null;

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

    //Update
    public async Task<VehicleReadDto?> UpdateAsync(Guid id, VehicleCreateDto dto)
    {
        var vehicle = await _context.Vehicles.FindAsync(id);

        if (vehicle == null) return null;

        if (vehicle.Userid != dto.UserId)
            throw new InvalidCastException("Não é permitido alterar o usuário proprietário do veículo");

        vehicle.Vehiclemodel = dto.VehicleModel;
        vehicle.Vehicleplate = dto.VehiclePlate;
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

    //Delete
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

