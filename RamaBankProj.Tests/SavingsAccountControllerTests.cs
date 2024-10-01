using Moq;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using RamaBankProj.Controllers;
using RamaBankProj.Services;
using System.Threading.Tasks;
using RamaBankProj.Shared;
using RamaBankProj.Dto;
using RamaBankProj.Model;

namespace RamaBankProj.Tests
{
    public class SavingsAccountControllerTests
    {
        private readonly Mock<ISavingsAccountService> _mockSavingsAccountService;
        private readonly IMapper _mapper;
        private readonly Mock<ILogger<SavingsAccountController>> _mockLogger;
        private readonly SavingsAccountController _controller;

        public SavingsAccountControllerTests()
        {
            _mockSavingsAccountService = new Mock<ISavingsAccountService>();
            _mockLogger = new Mock<ILogger<SavingsAccountController>>();

            var config = new MapperConfiguration(cfg => cfg.CreateMap<DirectDebitRequestModel, DirectDebit>());
            _mapper = config.CreateMapper();

            _controller = new SavingsAccountController(_mockSavingsAccountService.Object, _mapper, _mockLogger.Object);
        }

        [Fact]
        public async Task CalculateInterest_ReturnOkResult_WhenInterestIsCalculated()
        {
            // Arrange
            var accountId = 1;

            _mockSavingsAccountService.Setup(s => s.CalculateInterestAsync(accountId))
                .ReturnsAsync(OperationResult.Success("Interest calculated successfully"));

            // Act
            var result = await _controller.CalculateInterest(accountId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Interest calculated successfully: Interest calculated successfully", okResult.Value);
        }

        [Fact]
        public async Task CalculateInterest_ReturnNotFound_WhenAccountDoesNotExist()
        {
            // Arrange
            var accountId = 1;

            _mockSavingsAccountService.Setup(s => s.CalculateInterestAsync(accountId))
                .ReturnsAsync(OperationResult.Failure($"Account with ID {accountId} not found."));

            // Act
            var result = await _controller.CalculateInterest(accountId);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal($"Failed: Account with ID {accountId} not found.", badRequestResult.Value);
        }

        [Fact]
        public async Task CalculateInterest_ReturnBadRequest_WhenAccountIsNotSavings()
        {
            // Arrange
            var accountId = 1;

            _mockSavingsAccountService.Setup(s => s.CalculateInterestAsync(accountId))
                .ReturnsAsync(OperationResult.Failure($"Interest can only be calculated for Savings accounts. Account {accountId} is not a Savings account."));

            // Act
            var result = await _controller.CalculateInterest(accountId);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal($"Failed: Interest can only be calculated for Savings accounts. Account {accountId} is not a Savings account.", badRequestResult.Value);
        }
    }
}