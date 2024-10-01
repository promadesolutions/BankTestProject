using Moq;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using RamaBankProj.Controllers;
using RamaBankProj.Dto;
using RamaBankProj.Services;
using RamaBankProj.Model;
using RamaBankProj.Enums;

namespace RamaBankProj.Tests
{
    public class AccountControllerTests
    {
        private readonly Mock<IBankAccountService> _mockAccountService;
        private readonly IMapper _mapper;
        private readonly Mock<ILogger<AccountController>> _mockLogger;
        private readonly AccountController _controller;

        public AccountControllerTests()
        {
            _mockAccountService = new Mock<IBankAccountService>();
            _mockLogger = new Mock<ILogger<AccountController>>();

            var config = new MapperConfiguration(cfg => cfg.CreateMap<CustomerRequestModel, Account>());
            _mapper = config.CreateMapper();

            _controller = new AccountController(_mockAccountService.Object, _mapper, _mockLogger.Object);
        }

        [Fact]
        public async Task CreateAccount_ReturnOkResult_WhenAccountIsCreated()
        {
            // Arrange
            var model = new CustomerRequestModel
            {
                FirstName = "Krish",
                LastName = "Radhe",
                Email = "krish.radhe@example.com",
                Phone = "1234567890",
                AccountType = (int)AccountType.Current,
                Balance = 5.0m
            };

            _mockAccountService.Setup(s => s.AccountExistsAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<AccountType>()))
                .ReturnsAsync(false);

            // Act
            var result = await _controller.CreateAccount(model);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Account created successfully", okResult.Value);
        }

        [Fact]
        public async Task CreateAccount_ReturnBadRequest_WhenAccountAlreadyExists()
        {
            // Arrange
            var model = new CustomerRequestModel
            {
                FirstName = "Krish",
                LastName = "Radhe",
                Email = "krish.radhe@example.com",
                Phone = "1234567890",
                AccountType = (int)AccountType.Current,
                Balance = 5.0m
            };

            _mockAccountService.Setup(s => s.AccountExistsAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<AccountType>()))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.CreateAccount(model);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Account already exists with same name & account type", badRequest.Value);
        }

        // [Fact]
        // public async Task GetAccount_ReturnOkResult_WhenAccountExists()
        // {
        //     // Arrange
        //     var accountId = 1;
        //     var account = new Account
        //     {
        //         Id = accountId,
        //         FirstName = "Krish",
        //         LastName = "Radhe",
        //         Email = "krish.radhe@example.com",
        //         PhonePrimary = "1234567890",
        //         AccountType = (int)AccountType.Current,
        //         Balance = 5.0m
        //     };

        //     _mockAccountService.Setup(s => s.GetAccountByIdAsync(accountId))
        //         .ReturnsAsync(account);

        //     // Act
        //     var result = await _controller.GetAccount(accountId);

        //     // Assert
        //     var okResult = Assert.IsType<OkObjectResult>(result);
        //     var returnedAccount = Assert.IsType<Account>(okResult.Value);
        //     //Assert.Equal(accountId, returnedAccount.Id);
        //     Assert.Equal(account, returnedAccount);
        // }

        // [Fact]
        // public async Task GetAccount_ReturnNotFound_WhenAccountDoesNotExist()
        // {
        //     // Arrange
        //     var accountId = 1;

        //     _mockAccountService.Setup(s => s.GetAccountByIdAsync(accountId))
        //                    .ReturnsAsync((Account)null);

        //     // Act
        //     var result = await _controller.GetAccount(accountId);

        //     // Assert
        //     var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        //     Assert.Equal($"Account with ID {accountId} not found.", notFoundResult.Value);
        // }

        [Fact]
        public async Task FreezeAccount_ReturnOkResult_WhenAccountExists()
        {
            // Arrange
            var accountId = 1;
            var account = new Account
            {
                Id = accountId,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                PhonePrimary = "1234567890",
                AccountType = (int)AccountType.Current,
                Status = (int)Status.Active
            };

            _mockAccountService.Setup(s => s.GetAccountByIdAsync(accountId))
                .ReturnsAsync(account);

            // Act
            var result = await _controller.FreezeAccount(accountId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Account frozen successfully", okResult.Value);
            _mockAccountService.Verify(s => s.FreezeAccountAsync(account), Times.Once);
        }

        [Fact]
        public async Task FreezeAccount_ReturnNotFound_WhenAccountDoesNotExist()
        {
            // Arrange
            var accountId = 1;

            _mockAccountService.Setup(s => s.GetAccountByIdAsync(accountId))
                           .ReturnsAsync((Account)null);

            // Act
            var result = await _controller.FreezeAccount(accountId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);
            Assert.Equal($"Account with ID {accountId} not found.", notFoundResult.Value);
        }
    }
}