using Microsoft.AspNetCore.Mvc;
using ParcoBackend.Services;

namespace ParcoBackend.Controllers;

[ApiController]
[Route("api/report")]
public class ReportController : ControllerBase
{
    private readonly IReportService _reportService;

    public ReportController(IReportService reportService)
    {
        _reportService = reportService;
    }

    [HttpGet("revenue")]
    public async Task<IActionResult> GetRevenueReport([FromQuery] DateTime start, [FromQuery] DateTime end)
    {
        var result = await _reportService.GetRevenueByParkingAsync(start, end);

        if (result == null || !result.Any())
            return NotFound("Nenhum dado de receita encontrado para o período informado.");

        return Ok(result);
    }

}
