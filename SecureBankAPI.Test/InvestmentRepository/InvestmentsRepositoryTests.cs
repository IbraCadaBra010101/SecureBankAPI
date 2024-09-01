// <copyright file="InvestmentsRepositoryTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
#nullable disable
namespace SecureBankAPI.Tests.Repository.Investments
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using SecureBankAPI.Models;
    using SecureBankAPI.Repository.Investments;

    /// <summary>
    /// Contains unit tests for the <see cref="InvestmentsRepository"/> class.
    /// </summary>
    [TestClass]
    public class InvestmentsRepositoryTests
    {
        private Mock<IInvestmentsRepository> mockInvestmentsRepository;
        private Investment testInvestment;

        /// <summary>
        /// Initializes test data and mocks before each test.
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            this.mockInvestmentsRepository = new Mock<IInvestmentsRepository>();

            this.testInvestment = new Investment
            {
                InvestmentId = Guid.NewGuid(),
                ClientId = Guid.NewGuid(),
                InvestmentCategory = 1,
                Amount = 1000,
                DateInvested = DateTime.UtcNow,
                CurrentValue = 1200,
                RiskLevel = 1,
                InvestmentStatus = 0,
            };
        }

        /// <summary>
        /// Tests the <see cref="IInvestmentsRepository.UpdateInvestmentAsync"/> method.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [TestMethod]
        public async Task UpdateInvestmentAsync_ShouldCallUpdateMethod()
        {
            // Arrange
            this.mockInvestmentsRepository.Setup(repo => repo.UpdateInvestmentAsync(It.IsAny<Investment>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            // Act
            await this.mockInvestmentsRepository.Object.UpdateInvestmentAsync(this.testInvestment);

            // Assert
            this.mockInvestmentsRepository.Verify(repo => repo.UpdateInvestmentAsync(It.Is<Investment>(i => i.InvestmentId == this.testInvestment.InvestmentId)), Times.Once);
        }

        /// <summary>
        /// Tests the <see cref="IInvestmentsRepository.GetInvestmentByIdAsync"/> method.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [TestMethod]
        public async Task GetInvestmentByIdAsync_ShouldReturnInvestment()
        {
            // Arrange
            this.mockInvestmentsRepository.Setup(repo => repo.GetInvestmentByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(this.testInvestment);

            // Act
            var result = await this.mockInvestmentsRepository.Object.GetInvestmentByIdAsync(this.testInvestment.InvestmentId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(this.testInvestment.InvestmentId, result?.InvestmentId);
        }
    }
}
