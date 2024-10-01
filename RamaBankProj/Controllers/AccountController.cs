using Microsoft.AspNetCore.Mvc;
using RamaBankProj.Dto;
using RamaBankProj.Enums;
using RamaBankProj.Model;
using RamaBankProj.Services;
using AutoMapper;

namespace RamaBankProj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IBankAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IBankAccountService accountService, IMapper mapper, ILogger<AccountController> logger)
        {
            _accountService = accountService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        [Route("create-account")]
        public async Task<IActionResult> CreateAccount(CustomerRequestModel model)
        {
            try
            {
                // check if account already exists with same name & account type
                if (await _accountService.AccountExistsAsync(model.FirstName, model.LastName, AccountType.Savings))
                {
                    return BadRequest("Account already exists with same name & account type");
                }

                // create account
                var newAccount = _mapper.Map<Account>(model);
                newAccount.CreatedOn = DateTime.Now;
                newAccount.UpdatedOn = DateTime.Now;
                newAccount.Status = (int)Status.Active;

                await _accountService.CreateAccountAsync(newAccount);

                return Ok("Account created successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating account");
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("get-account")]
        public async Task<ActionResult<Account>> GetAccount(int accountId)
        {
            try
            {
                var accnt = await _accountService.GetAccountByIdAsync(accountId);
                if (accnt == null)
                {
                    return NotFound($"Account with ID {accountId} not found.");
                }
                return Ok(accnt);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting account");
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("freeze-account")]
        public async Task<ActionResult> FreezeAccount(int accountId)
        {
            try
            {
                var accnt = await _accountService.GetAccountByIdAsync(accountId);
                if(accnt == null)
                {
                    return NotFound($"Account with ID {accountId} not found.");
                }

                await _accountService.FreezeAccountAsync(accnt);

                return Ok("Account frozen successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while freeze an account");
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}