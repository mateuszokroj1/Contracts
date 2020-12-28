using System;
using System.Collections;
using Contracts.Models;
using Contracts.Strategies;

namespace Contracts
{
    public static class Contracts
    {
        public static void CheckCustom(IContract toCheck)
        {
            if (toCheck == null)
                throw new ArgumentNullException(nameof(toCheck));

            toCheck.Check();
        }

        public static void ThrowIfNotEqual<TException, Tvalue>(Tvalue actual, Tvalue expected)
            where TException : Exception
            where Tvalue : IEquatable<Tvalue>
        {
            IStrategy strategy;
            if (ContractsGlobalSettings.UseDebugModeWhenThrowException)
            {
                strategy = new DebugModeStrategy();
                strategy.Parameters = new StrategyParameters { Message = "Argument is not equal to expected value." };
            }
            else
                strategy = new ThrowExceptionStrategy<TException>("Argument is not equal to expected value.");
            var contract = new StrategyContract(strategy);
            contract.Predicate = () => actual?.Equals(expected) ?? false;

            contract.Check();
        }

        public static void SetValueIfConditionFalse()
        {

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
                strategy.Parameters = new StrategyParameters { Message = $"{argumentName ?? "Argument"} is null." };
            }
            else
                strategy = new ThrowExceptionStrategy<ArgumentNullException>(argumentName);
            var contract = new StrategyContract(strategy);
            contract.Predicate = () => value != null;

            contract.Check();
        }

        public static void IndexIsValidForCollection(ICollection collection, int index)
        {
            IStrategy strategy;
            if (ContractsGlobalSettings.UseDebugModeWhenThrowException)
            {
                strategy = new DebugModeStrategy();
                strategy.Parameters = new StrategyParameters { Message = "Index is out of range." };
            }
            else
                strategy = new ThrowExceptionStrategy<IndexOutOfRangeException>("Index is out of range.");
            var contract = new StrategyContract(strategy);
            contract.Predicate = () => index >= 0 && collection.Count > index;

            contract.Check();
        }

        public static void InRangeOf<T>(T value, T minimum, T maximum, RangeType rangeType = RangeType.MinInclusive | RangeType.MaxInclusive)
            where T : struct, IComparable
        {
            if (minimum.CompareTo(maximum) > 0)
                throw new ArgumentException("Minimum is greater than maximum.");

            if (maximum.CompareTo(minimum) < 0)
                throw new ArgumentException("Maximum is lower than minimum.");

            IStrategy strategy;
            if (ContractsGlobalSettings.UseDebugModeWhenThrowException)
            {
                strategy = new DebugModeStrategy();
                strategy.Parameters = new StrategyParameters { Message = "Argument is out of range." };
            }
            else
                strategy = new ThrowExceptionStrategy<ArgumentOutOfRangeException>("Argument is out of range.");
            
            var contract = new StrategyContract(strategy);
            contract.Predicate = () =>
            {
                if
                (
                    rangeType.HasFlag(RangeType.MaxInclusive) && minimum.CompareTo(value) < 0 ||
                    minimum.CompareTo(value) <= 0
                )
                    return false;

                if
                (
                    rangeType.HasFlag(RangeType.MaxInclusive) && value.CompareTo(maximum) > 0 ||
                    value.CompareTo(maximum) >= 0
                )
                    return false;

                return true;
            };

            contract.Check();
        }
    }
}
