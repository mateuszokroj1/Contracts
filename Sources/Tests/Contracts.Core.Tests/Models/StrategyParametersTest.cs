using System;

using Contracts.Models;

using Xunit;

namespace Contracts.Tests.Models
{
    public class StrategyParametersTest
    {
        #region Constructor

        [Fact]
        public void Constructor_ShouldNotThrow()
        {
            var result = new StrategyParameters();

            Assert.NotNull(result);
        }

        #endregion

        #region Properties

        [Fact]
        public void Message_ShouldSetAndGetTheSameValue()
        {
            var parameters = new StrategyParameters();
            parameters.Message = null;

            var result1 = parameters.Message;

            parameters.Message = "TEST";

            Assert.Equal("TEST", parameters.Message);
        }

        #endregion
    }
}
