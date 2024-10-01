using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RamaBankProj.Services;

namespace RamaBankProj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SavingsAccountController : ControllerBase
    {
        private readonly ISavingsAccountService _savingsAccountService;
        private readonly IMapper _mapper;
        private readonly ILogger<SavingsAccountController> _logger;

        public SavingsAccountController(ISavingsAccountService accountService, IMapper mapper, ILogger<SavingsAccountController> logger)
        {
            _savingsAccountService = accountService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("calculate-interest")]
        public async Task<IActionResult> CalculateInterest(int accountId)
        {
            var result = await _savingsAccountService.CalculateInterestAsync(accountId);

            if (result.IsSuccess)
            {
                return Ok($"Interest calculated successfully: {result.Message}");
            }
            else
            {
                return BadRequest($"Failed: {result.Message}");
            }
        }
    }
}