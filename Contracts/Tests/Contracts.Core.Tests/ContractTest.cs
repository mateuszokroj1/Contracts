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

            var contract = new Contract(predicate, onFailure);

            Assert.NotNull(contract);
            Assert.Equal(predicate, contract.Predicate);
            Assert.Equal(onFailure, contract.OnFailure);
        }

        #endregion

        #region Properties

        [Fact]
        public void Predicate_ShouldSetAndGetTheSameValue()
        {
            Func<bool> predicate = () => true;
            Action<object> onFailure = args => predicate();

            var contract = new Contract(predicate, onFailure);

            var result1 = contract.Predicate;

            contract.Predicate = null;

            Assert.Equal(predicate, result1);
            Assert.Null(contract.Predicate);
        }

        [Fact]
        public void OnFailure_ShouldSetAndGetTheSameValue()
        {
            Func<bool> predicate = () => true;
            Action<object> onFailure = args => predicate();

            var contract = new Contract(predicate, onFailure);

            var result1 = contract.OnFailure;

            contract.OnFailure = null;

            Assert.Equal(onFailure, result1);
            Assert.Null(contract.OnFailure);
        }

        [Fact]
        public void Parameters_ShouldSetAndGetTheSameValue()
        {
            Func<bool> predicate = () => true;
            Action<object> onFailure = args => predicate();

            var contract = new Contract(predicate, onFailure);

            var result1 = contract.Parameters;

            contract.Parameters = 0.5f;

            Assert.Null(result1);
            Assert.Equal(contract.Parameters, 0.5f);
        }

        #endregion

        #region Methods

        [Fact]
        public void Check_WhenPredicateReturnTrue_NothingToDo()
        {
            Func<bool> predicate = () => true;
            bool areInvoked = false;
            Action<object> onFailure = args => areInvoked = true;

            var contract = new Contract(predicate, onFailure);
            contract.Check();

            Assert.False(areInvoked);
        }

        [Fact]
        public void Check_WhenPredicateReturnTrue_ShouldInvokeOnFailure()
        {
            Func<bool> predicate = () => false;
            bool areInvoked = false;
            Action<object> onFailure = args => areInvoked = true;

            var contract = new Contract(predicate, onFailure);
            contract.Check();

            Assert.True(areInvoked);
        }

        #endregion
    }
}
