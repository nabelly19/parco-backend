namespace DTO;

public class PricingRuleDto
{
    public string PlaneName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Multiplier { get; set; } = 1.0m;
    public TimeOnly? StartTime { get; set; }
    public TimeOnly? EndTime { get; set; }
    public bool WeekendOnly { get; set; } = false;
    public bool Active { get; set; } = true; 
}