namespace DTO; 

public class RevenueReportDto 
{
    public string ParkingName { get; set; }
    public decimal TotalRevenue { get; set; }
    public int TotalBookings { get; set; }
}

public class OccupancyReportDto
{
    public string ParkingName { get; set; }
    public double AverageoccupancyRate { get; set; }
}

public class BookingSummaryDto
{
    public DateTime Date { get; set; }
    public int TotalBookings{ get; set; }
}