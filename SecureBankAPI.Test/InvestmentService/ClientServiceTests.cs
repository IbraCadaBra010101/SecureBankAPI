// <copyright file="ClientServiceTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
#nullable disable
namespace SecureBankAPI.Tests.InvestmentService
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using SecureBankAPI.Data;
    using SecureBankAPI.Models;
    using SecureBankAPI.Repository.Clients;
    using SecureBankAPI.Repository.Investments;
    using SecureBankAPI.Services.Clients;

    /// <summary>
    /// Contains unit tests for the <see cref="ClientService"/> class.
    /// </summary>
    [TestClass]
    public class ClientServiceTests
    {
        private Mock<IInvestmentsRepository> investmentsRepositoryMock;
        private Mock<IClientsRepository> clientRepositoryMock;
        private SecureBankDBContext secureBankDBContext;
        private ClientService clientService;

        private Investment sourceInvestment;
        private Investment destinationInvestment;

        /// <summary>
        /// Initializes test data and mocks before each test.
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            var options = new DbContextOptionsBuilder<SecureBankDBContext>()
                .UseInMemoryDatabase(databaseName: "SecureBankTestDb")
                .Options;

            this.secureBankDBContext = new SecureBankDBContext(options);

            this.investmentsRepositoryMock = new Mock<IInvestmentsRepository>();
            this.clientRepositoryMock = new Mock<IClientsRepository>();

            this.clientService = new ClientService(this.clientRepositoryMock.Object, this.investmentsRepositoryMock.Object, this.secureBankDBContext);

            this.sourceInvestment = new Investment
            {
                InvestmentId = Guid.NewGuid(),
                ClientId = Guid.NewGuid(),
                CurrentValue = 1000,
            };

            this.destinationInvestment = new Investment
            {
                InvestmentId = Guid.NewGuid(),
                ClientId = this.sourceInvestment.ClientId,
                CurrentValue = 500,
            };

            this.secureBankDBContext.Investments.Add(this.sourceInvestment);
            this.secureBankDBContext.Investments.Add(this.destinationInvestment);
            this.secureBankDBContext.SaveChanges();
        }

        /// <summary>
        /// Tests the <see cref="ClientService.TransferInvestmentFundsAsync"/> method when the investments belong to different clients.
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task TransferInvestmentFundsAsync_ShouldThrowInvalidOperationException_WhenInvestmentsBelongToDifferentClients()
        {
            // Arrange
            var transferAmount = 200;
            this.destinationInvestment.ClientId = Guid.NewGuid(); // Set different ClientId

            this.investmentsRepositoryMock.Setup(repo => repo.GetInvestmentByIdAsync(this.sourceInvestment.InvestmentId))
                .ReturnsAsync(this.sourceInvestment);

            this.investmentsRepositoryMock.Setup(repo => repo.GetInvestmentByIdAsync(this.destinationInvestment.InvestmentId))
                .ReturnsAsync(this.destinationInvestment);

            // Act
            await this.clientService.TransferInvestmentFundsAsync(this.sourceInvestment.InvestmentId, this.destinationInvestment.InvestmentId, transferAmount);
        }

        /// <summary>
        /// Tests the <see cref="ClientService.TransferInvestmentFundsAsync"/> method when the source investment has insufficient funds.
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task TransferInvestmentFundsAsync_ShouldThrowInvalidOperationException_WhenInsufficientFunds()
        {
            // Arrange
            var transferAmount = 2000;

            this.investmentsRepositoryMock.Setup(repo => repo.GetInvestmentByIdAsync(this.sourceInvestment.InvestmentId))
                .ReturnsAsync(this.sourceInvestment);

            this.investmentsRepositoryMock.Setup(repo => repo.GetInvestmentByIdAsync(this.destinationInvestment.InvestmentId))
                .ReturnsAsync(this.destinationInvestment);

            // Act
            await this.clientService.TransferInvestmentFundsAsync(this.sourceInvestment.InvestmentId, this.destinationInvestment.InvestmentId, transferAmount);
        }
    }
}
