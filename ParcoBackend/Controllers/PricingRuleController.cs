using Microsoft.AspNetCore.Mvc;
using DTO;
using ParcoBackend.Services;
using System;
using System.Threading.Tasks;

namespace ParcoBackend.Controllers;

[ApiController]
[Route("api/pricing")]
public class PricingRuleController : ControllerBase
{
    private readonly IPricingRuleService _service;

    public PricingRuleController(IPricingRuleService service)
    {
        _service = service;
    }

        [HttpGet]
        public async Task<IActionResult> GetAllRules()
        {
            var rules = await _service.GetAllRulesAsync();
            return Ok(rules);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetRule(string name)
        {
            var rule = await _service.GetRuleByNameAsync(name);
            if (rule == null) return NotFound();
            return Ok(rule);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRule([FromBody] PricingRuleDto dto)
        {
            var created = await _service.CreateRuleAsync(dto);
            if (created == null)
                return Conflict($"Já existe uma regra com o nome '{dto.PlaneName}'.");

            return CreatedAtAction(nameof(GetRule), new { name = dto.PlaneName }, created);
        }

        [HttpPut("{name}")]
        public async Task<IActionResult> UpdateRule(string name, [FromBody] PricingRuleDto dto)
        {
            var updated = await _service.UpdateRuleAsync(name, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{name}")]
        public async Task<IActionResult> DeleteRule(string name)
        {
            var deleted = await _service.DeleteRuleAsync(name);
            if (!deleted) return NotFound();
            return NoContent();
        }
}