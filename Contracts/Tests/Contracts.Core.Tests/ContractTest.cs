using System;

using Xunit;

namespace Contracts.Tests
{
    public class ContractTest
    {
        #region Constructors

        [Fact]
        public void Constructor1_WhenArgumentIsNull_ShouldThrowArgumentNullException()
        {
            Func<bool> predicate = () => true, nullPredicate = null;
            Action<object> onFailure = args => predicate(), nullOnFailure = null;

            Assert.Throws<ArgumentNullException>(() => new Contract(nullPredicate, onFailure));
            Assert.Throws<ArgumentNullException>(() => new Contract(predicate, nullOnFailure));
        }

        [Fact]
        public void Constructor1_WhenArgumentIsValid_ShouldSetProperties()
        {
            Func<bool> predicate = () => true;
            Action<object> onFailure = args => predicate();
        }

        #endregion
    }
}
