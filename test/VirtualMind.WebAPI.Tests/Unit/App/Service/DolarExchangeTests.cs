using AutoFixture;
using AutoMapper;
using Microsoft.Extensions.Options;
using VirtualMind.WebAPI.Infrastructure.Core;
using VirtualMind.WebAPI.Service;
using Xunit;

namespace VirtualMind.WebAPI.Tests.Unit.Service
{
    public class DolarExchangeTests
    {
        private readonly IOptions<ExchangeProviderSettings> _settings;
        private readonly IOptions<ThresholdSettings> _thresholdSettings;
        private readonly IMapper _mapper;
        private readonly Fixture _fixture;

        public DolarExchangeTests()
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
            var result = new DolarExchange(_settings, _thresholdSettings, _mapper).IsCurrency("USD");

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void IsCurrency_Returns_False()
        {
            //Arrange
            var result = new DolarExchange(_settings, _thresholdSettings, _mapper).IsCurrency("BRL");

            //Assert
            Assert.False(result);
        }
    }
}
