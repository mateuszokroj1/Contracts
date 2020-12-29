using Xunit;

namespace Contracts.Tests
{
    public class ContractsGlobalSettingsTest
    {
        [Fact]
        public void UseDebugModeWhenThrowException_ShouldSetAndGetTheSameValue()
        {
            ContractsGlobalSettings.UseDebugModeWhenThrowException = false;

            var result1 = ContractsGlobalSettings.UseDebugModeWhenThrowException;

            ContractsGlobalSettings.UseDebugModeWhenThrowException = true;

            Assert.False(result1);
            Assert.True(ContractsGlobalSettings.UseDebugModeWhenThrowException);
        }
    }
}
