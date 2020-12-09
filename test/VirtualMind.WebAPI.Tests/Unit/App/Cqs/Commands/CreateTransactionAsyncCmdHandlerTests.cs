using AutoFixture;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using VirtualMind.WebAPI.App.Cqs;
using VirtualMind.WebAPI.App.Models;
using VirtualMind.WebAPI.Infrastructure.Proxies;
using Xunit;

namespace VirtualMind.WebAPI.Tests.Unit.App.Cqs.Commands
{
    public class CreateTransactionAsyncCmdHandlerTests
    {
        private readonly Mock<ICurrencyProxy> _providerProxy;
        private readonly Mock<ITransactionProxy> _transactionProxy;
        private readonly Fixture _fixture;

        public CreateTransactionAsyncCmdHandlerTests()
        {
            _providerProxy = new Mock<ICurrencyProxy>();
            _transactionProxy = new Mock<ITransactionProxy>();
            _fixture = new Fixture();
        }

        [Fact]
        public async Task Handle_Returns_Model()
        {
            // Arrange
            _providerProxy.Setup(p => p.GetExchangeForTransaction(It.IsAny<string>()))
                .ReturnsAsync(_fixture.Create<CurrencyExchangeModelTransaction>());
            _transactionProxy.Setup(t => t.GetTotalTransactions(It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(It.IsAny<decimal>());
            _transactionProxy.Setup(t => t.Insert(It.IsAny<ExchangeTransactionInput>(), It.IsAny<decimal>(),
                It.IsAny<decimal>(), It.IsAny<decimal>()))
                .ReturnsAsync(_fixture.Create<ExchangeTransactionModel>());

            // Act
            var result = await new CreateTransactionAsyncCmdHandler(_providerProxy.Object, _transactionProxy.Object)
                .Handle(_fixture.Create<CreateTransactionAsyncCmd>(), CancellationToken.None);

            // Assert
            Assert.IsType<ExchangeTransactionModel>(result);
        }
    }
}
