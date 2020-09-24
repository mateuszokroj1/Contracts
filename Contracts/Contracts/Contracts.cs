using System;
using System.Collections;
using Contracts.Models;
using Contracts.Strategies;

namespace Contracts
{
    public static class Contracts
    {
        /// <summary>
        /// Sets value in destination from source, when values are not equal.
        /// </summary>
        public static void SetValueIfNotEqual<T>(ref T destination, T source)
        {
            IStrategy strategy = new SetValueStrategy<T>(ref destination, source);
            var contract = new StrategyContract(strategy);
            var referencedValue = destination;
            contract.Predicate = () => !referencedValue.Equals(source);

            contract.Check();
        }

        /// <summary>
        /// Throws <see cref="ArgumentNullException"/>, when <paramref name="value"/> is <see langword="null"/>.
        /// </summary>
        /// <typeparam name="T">Reference type</typeparam>
        /// <param name="value">Value to check</param>
        /// <exception cref="ArgumentNullException"/>
        public static void NotNullArgument<T>(T value, string argumentName = "")
            where T : class
        {
            IStrategy strategy;
            if (ContractsGlobalSettings.UseDebugModeWhenThrowException)
            {
                strategy = new DebugModeStrategy();
                strategy.Parameters = new DebugModeStrategyParameters { Message = $"{argumentName ?? "Argument"} is null." };
            }
            else
                strategy = new ThrowExceptionStrategy<ArgumentNullException>(argumentName);
            var contract = new StrategyContract(strategy);
            contract.Predicate = () => value == null;

            contract.Check();
        }

        public static void IndexInRangeOfCollection(ICollection collection, int index)
        {
            IStrategy strategy;
            if (ContractsGlobalSettings.UseDebugModeWhenThrowException)
            {
                strategy = new DebugModeStrategy();
                strategy.Parameters = new DebugModeStrategyParameters { Message = "Index is out of range." };
            }
            else
                strategy = new ThrowExceptionStrategy<IndexOutOfRangeException>("Index is out of range.");
            var contract = new StrategyContract(strategy);
            contract.Predicate = () => index >= 0 && collection.Count > index;

            contract.Check();
        }
    }
}
