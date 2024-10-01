using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RamaBankProj.Dto;
using RamaBankProj.Services;

namespace RamaBankProj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrentAccountController : ControllerBase
    {
        private readonly ICurrentAccountService _currentAccountService;
        private readonly IMapper _mapper;
        private readonly ILogger<CurrentAccountController> _logger;

        public CurrentAccountController(ICurrentAccountService accountService, IMapper mapper, ILogger<CurrentAccountController> logger)
        {
            _currentAccountService = accountService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost("schedule-direct-debit")]
        public async Task<IActionResult> ScheduleDirectDebit(DirectDebitRequestModel ddModel)
        {
            var result = await _currentAccountService.ScheduleDirectDebitAsync(ddModel.AccountId, ddModel.Amount, ddModel.ScheduledDate);

            if (result.IsSuccess)
            {
                return Ok("Direct debit scheduled successfully");
            }
            else
            {
                return BadRequest($"Failed: {result.Message}");
            }
        }
    }
}