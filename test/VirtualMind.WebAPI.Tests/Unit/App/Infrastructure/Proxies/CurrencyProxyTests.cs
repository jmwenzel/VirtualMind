using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VirtualMind.WebAPI.App.Models;
using VirtualMind.WebAPI.Infrastructure.Proxies;
using VirtualMind.WebAPI.Service;
using Xunit;

namespace VirtualMind.WebAPI.Tests.Unit.Infrastructure.Proxies
{
    /// <summary>
    /// Tests for <see cref="CurrencyProxy"/>
    /// </summary>
    public class CurrencyProxyTests
    {
        private readonly Mock<IExchange> _providerMock;

        public CurrencyProxyTests()
        {
            _providerMock = new Mock<IExchange>();
        }

        [Theory, InlineData("USD"), InlineData("BRL")]
        public async Task GetExchange_Returns_Model(string currency)
        {
            // Arrange
            _providerMock.Setup(p => p.IsCurrency(currency)).Returns(true);
            _providerMock.Setup(p => p.GetExchange()).ReturnsAsync(new CurrencyExchangeModel());

            var providers = new List<IExchange> { _providerMock.Object };
            
            // Act
            var result = await new CurrencyProxy(providers).GetExchange(currency);

            // Assert
            Assert.IsType<CurrencyExchangeModel>(result);
        }

        [Theory, InlineData("PEN")]
        public async Task GetExchange_Returns_NotImplemented(string currency)
        {
            // Arrange
            var providers = new List<IExchange>() ;

            // Act
            var result = await Assert.ThrowsAsync<NotImplementedException>(
                async () => await new CurrencyProxy(providers).GetExchange(currency)
                );

            // Assert
            Assert.IsType<NotImplementedException>(result);
        }

        [Theory, InlineData("USD"), InlineData("BRL")]
        public async Task GetExchangeForTransaction_Returns_Model(string currency)
        {
            // Arrange
            var model = new CurrencyExchangeModelTransaction
            {
                Currency = currency,
                Exchange = 1000,
                Date = DateTime.Now.ToString(),
                Threshold = 200
            };

            _providerMock.Setup(p => p.IsCurrency(currency)).Returns(true);
            _providerMock.Setup(p => p.GetExchangeForTransaction()).ReturnsAsync(model);

            var providers = new List<IExchange> { _providerMock.Object };

            // Act
            var result = await new CurrencyProxy(providers).GetExchangeForTransaction(currency);

            // Assert
            Assert.Equal(model, result);
        }
    }
}
