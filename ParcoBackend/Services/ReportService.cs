using DTO;
using ParcoBackend.Model;
using System.Data.Common;

using Microsoft.EntityFrameworkCore;
using ParcoBackend.Services;

public class ReportService : IReportService 
{
    ParcoContext _context;

    public async Task<List<RevenueReportDto>> GetRevenueByParkingAsync(DateTime start, DateTime end)
    {
        return await _context.Payments
            .Where(p => p.Paymentdate >= start && p.Paymentdate <= end)
            .GroupBy(p => p.Parkingspaceid)
            .Select(g => new RevenueReportDto
            {
                ParkingName = g.First().Parkingspace.Name,
                TotalRevenue = g.Sum(x => x.Amount),
                TotalBookings = g.Count()
            })
            .ToListAsync();
    }

    public async Task<List<OccupancyReportDto>> GetAverageOccupancyRateAsync(DateTime start, DateTime end)
    {
        return await _context.Parkingspaces
            .Select(ps => new OccupancyReportDto
            {
                ParkingName = ps.Name,
                AverageoccupancyRate = ps.Parkingslots.Count(s => s.Isocuppied) / (double)ps.Totalcapacity * 100
            })
            .ToListAsync();
    }

    public async Task<List<BookingSummaryDto>> GetBookingSummaryAsync(DateOnly start, DateOnly end)
    {
        return await _context.Bookings
            .Where(b => b.Reservationdate >= start && b.Reservationdate <= end)
            .GroupBy(b => b.Reservationdate)
            .Select(g => new BookingSummaryDto
            {
                Date = g.Key.ToDateTime(TimeOnly.MinValue),
                TotalBookings = g.Count()
            })
            .OrderBy(r => r.Date)
            .ToListAsync();
    }

}   