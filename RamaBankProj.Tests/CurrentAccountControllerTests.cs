using Moq;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using RamaBankProj.Controllers;
using RamaBankProj.Dto;
using RamaBankProj.Services;
using RamaBankProj.Model;
using RamaBankProj.Shared;

namespace RamaBankProj.Tests
{
    public class CurrentAccountControllerTests
    {
        private readonly Mock<ICurrentAccountService> _mockCurrentAccountService;
        private readonly IMapper _mapper;
        private readonly Mock<ILogger<CurrentAccountController>> _mockLogger;
        private readonly CurrentAccountController _controller;

        public CurrentAccountControllerTests()
        {
            _mockCurrentAccountService = new Mock<ICurrentAccountService>();
            _mockLogger = new Mock<ILogger<CurrentAccountController>>();

            var config = new MapperConfiguration(cfg => cfg.CreateMap<DirectDebitRequestModel, DirectDebit>());
            _mapper = config.CreateMapper();

            _controller = new CurrentAccountController(_mockCurrentAccountService.Object, _mapper, _mockLogger.Object);
        }

        [Fact]
        public async Task ScheduleDirectDebit_ReturnOkResult_WhenDirectDebitIsScheduled()
        {
            // Arrange
            var model = new DirectDebitRequestModel
            {
                AccountId = 1,
                Amount = 100.0m,
                ScheduledDate = DateTime.UtcNow.AddDays(1)
            };

            _mockCurrentAccountService.Setup(s => s.ScheduleDirectDebitAsync(model.AccountId, model.Amount, model.ScheduledDate))
                .ReturnsAsync(OperationResult.Success("Direct debit scheduled successfully"));

            // Act
            var result = await _controller.ScheduleDirectDebit(model);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Direct debit scheduled successfully", okResult.Value);
        }

        [Fact]
        public async Task ScheduleDirectDebit_ReturnNotFound_WhenAccountDoesNotExist()
        {
            // Arrange
            var model = new DirectDebitRequestModel
            {
                AccountId = 1,
                Amount = 100.0m,
                ScheduledDate = DateTime.UtcNow.AddDays(1)
            };

            _mockCurrentAccountService.Setup(s => s.ScheduleDirectDebitAsync(model.AccountId, model.Amount, model.ScheduledDate))
                .ReturnsAsync(OperationResult.Failure($"Account with ID {model.AccountId} not found."));

            // Act
            var result = await _controller.ScheduleDirectDebit(model);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal($"Failed: Account with ID {model.AccountId} not found.", badRequestResult.Value);
        }

        [Fact]
        public async Task ScheduleDirectDebit_ReturnBadRequest_WhenAccountIsNotCurrent()
        {
            // Arrange
            var model = new DirectDebitRequestModel
            {
                AccountId = 1,
                Amount = 100.0m,
                ScheduledDate = DateTime.UtcNow.AddDays(1)
            };

            _mockCurrentAccountService.Setup(s => s.ScheduleDirectDebitAsync(model.AccountId, model.Amount, model.ScheduledDate))
                .ReturnsAsync(OperationResult.Failure($"Direct debits can only be scheduled for Current accounts. Account {model.AccountId} is not a Current account."));

            // Act
            var result = await _controller.ScheduleDirectDebit(model);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal($"Failed: Direct debits can only be scheduled for Current accounts. Account {model.AccountId} is not a Current account.", badRequestResult.Value);
        }

        [Fact]
        public async Task ScheduleDirectDebit_ReturnBadRequest_WhenAccountIsFrozen()
        {
            // Arrange
            var model = new DirectDebitRequestModel
            {
                AccountId = 1,
                Amount = 100.0m,
                ScheduledDate = DateTime.UtcNow.AddDays(1)
            };

            _mockCurrentAccountService.Setup(s => s.ScheduleDirectDebitAsync(model.AccountId, model.Amount, model.ScheduledDate))
                .ReturnsAsync(OperationResult.Failure($"Account {model.AccountId} is frozen and cannot be used for transactions."));

            // Act
            var result = await _controller.ScheduleDirectDebit(model);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal($"Failed: Account {model.AccountId} is frozen and cannot be used for transactions.", badRequestResult.Value);
        }
    }
}