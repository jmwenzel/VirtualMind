using AutoFixture;
using AutoMapper;
using Microsoft.Extensions.Options;
using VirtualMind.WebAPI.Infrastructure.Core;
using VirtualMind.WebAPI.Service;
using Xunit;

namespace VirtualMind.WebAPI.Tests.Unit.Service
{
    public class RealExchangeTests
    {
        private readonly IOptions<ExchangeProviderSettings> _settings;
        private readonly IOptions<ThresholdSettings> _thresholdSettings;
        private readonly IMapper _mapper;
        private readonly Fixture _fixture;

        public RealExchangeTests()
        {
            _fixture = new Fixture();

            _settings = Options.Create(_fixture.Create<ExchangeProviderSettings>());
            _thresholdSettings = Options.Create(_fixture.Create<ThresholdSettings>());

            _mapper = MapperHelper.InitMappings();
        }

        [Fact]
        public void IsCurrency_Returns_True()
        {
            //Arrange
            var result = new RealExchange(_settings, _thresholdSettings, _mapper).IsCurrency("BRL");

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void IsCurrency_Returns_False()
        {
            //Arrange
            var result = new RealExchange(_settings, _thresholdSettings, _mapper).IsCurrency("USD");

            //Assert
            Assert.False(result);
        }
    }
}
