using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParcoBackend.Services;

public class PricingRuleService : IPricingRuleService
{
    private static readonly List<PricingRuleDto> _rules = new();

    public Task<IEnumerable<PricingRuleDto>> GetAllRulesAsync()
        => Task.FromResult(_rules.AsEnumerable());

        public Task<PricingRuleDto?> GetRuleByNameAsync(string name)
        => Task.FromResult(_rules.FirstOrDefault(r => r.PlaneName.Equals(name, StringComparison.OrdinalIgnoreCase)));

        public Task<PricingRuleDto> CreateRuleAsync(PricingRuleDto dto)
        {
            _rules.Add(dto);
            return Task.FromResult(dto);
        }

        public Task<PricingRuleDto?> UpdateRuleAsync(string name, PricingRuleDto dto)
        {
            var existing = _rules.FirstOrDefault(r => r.PlaneName.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (existing == null) return Task.FromResult<PricingRuleDto?>(null);

            existing.Description = dto.Description;
            existing.Multiplier = dto.Multiplier;
            existing.StartTime = dto.StartTime;
            existing.EndTime = dto.EndTime;
            existing.WeekendOnly = dto.WeekendOnly;
            existing.Active = dto.Active;

            return Task.FromResult(existing);
        }

        public Task<bool> DeleteRuleAsync(string name)
        {
            var rule = _rules.FirstOrDefault(r => r.PlaneName.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (rule == null) return Task.FromResult(false);

            _rules.Remove(rule);
            return Task.FromResult(true);
        }

        public decimal ApplyRule(decimal baseFee, DateTime reservationTime)
        {
            var rule = _rules.FirstOrDefault(r =>
                r.Active &&
                (!r.WeekendOnly || reservationTime.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday) &&
                (!r.StartTime.HasValue || r.StartTime <= TimeOnly.FromDateTime(reservationTime)) &&
                (!r.EndTime.HasValue || r.EndTime >= TimeOnly.FromDateTime(reservationTime)));

            if (rule == null) return baseFee;
            return baseFee * rule.Multiplier;
        }
}