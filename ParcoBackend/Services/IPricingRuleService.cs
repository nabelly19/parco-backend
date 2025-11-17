using DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParcoBackend.Services;

public interface IPricingRuleService
{
    Task<IEnumerable<PricingRuleDto>> GetAllRulesAsync();
    Task<PricingRuleDto?> GetRuleByNameAsync(string name);
    Task<PricingRuleDto> CreateRuleAsync(PricingRuleDto dto);
    Task<PricingRuleDto?> UpdateRuleAsync(string name, PricingRuleDto dto);
    Task<bool> DeleteRuleAsync(string name);
    decimal ApplyRule(decimal baseFee, DateTime reservationTime);
}

