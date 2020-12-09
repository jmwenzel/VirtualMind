using AutoFixture;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VirtualMind.WebAPI.App.Cqs;
using VirtualMind.WebAPI.App.Models;
using VirtualMind.WebAPI.Infrastructure.Proxies;
using Xunit;

namespace VirtualMind.WebAPI.Tests.Unit.App.Cqs.Queries
{
    public class GetCurrencyExchangeQueryHandlerTests
    {
        private readonly Mock<ICurrencyProxy> _providerProxy;
        private readonly Fixture _fixture;

        public GetCurrencyExchangeQueryHandlerTests()
        {
            _providerProxy = new Mock<ICurrencyProxy>();
            _fixture = new Fixture();
        }

        [Fact]
        public async Task Handle_Returns_Model()
        {
            // Arrange
            _providerProxy.Setup(p => p.GetExchange(It.IsAny<string>()))
                .ReturnsAsync(_fixture.Create<CurrencyExchangeModel>());

            // Act
            var result = await new GetCurrencyExchangeQueryHandler(_providerProxy.Object)
                .Handle(_fixture.Create<GetCurrencyExchangeQuery>(), CancellationToken.None);

            // Assert
            Assert.IsType<CurrencyExchangeModel>(result);
        }
    }
}
