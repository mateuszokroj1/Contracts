using System;
using System.Collections;
using Contracts.Models;
using Contracts.Strategies;

namespace Contracts
{
    /// <summary>
    /// Common checking operations
    /// </summary>
    public static class CheckThat
    {
        /// <summary>
        /// Checks custom contract
        /// </summary>
        /// <param name="toCheck">Contract object to check</param>
        /// <exception cref="ArgumentNullException"/>
        public static void CheckCustom(IContract toCheck)
        {
            if (toCheck == null)
                throw new ArgumentNullException(nameof(toCheck));

            toCheck.Check();
        }

        public static void CheckAndRunIfFalse(Func<bool> predicate, Action onFailure)
        {
            if (!predicate())
                onFailure();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Tvalue">Type of value to be validated</typeparam>
        /// <typeparam name="Texception"></typeparam>
        /// <param name="value">Value to be validated</param>
        /// <param name="validators"></param>
        /// <exception cref="Texception"/>
        public static void ValidateValue<Tvalue, Texception>(Tvalue value, params IValidator<Tvalue>[] validators)
            where Texception : Exception
        {
            foreach (var validator in validators)
                if (!validator.Validate(value))
                    throw Activator.CreateInstance(typeof(Texception)) as Texception;
        }

        public static void ThrowIfNotEqual<TException, Tvalue>(Tvalue expected, Tvalue actual)
            where TException : Exception
            where Tvalue : IEquatable<Tvalue>
        {
            IStrategy strategy;
            if (ContractsGlobalSettings.UseDebugModeWhenThrowException)
                strategy = new DebugModeStrategy
                {
                    Parameters = new StrategyParameters { Message = "Argument is not equal to expected value." }
                };
            else
                strategy = new ThrowExceptionStrategy<TException>("Argument is not equal to expected value.");

            var contract = new StrategyContract(strategy)
            {
                Predicate = () => actual?.Equals(expected) ?? false
            };

            contract.Check();
        }

        public static void SetValueIfFalse<T>(Func<bool> predicate, ref T destination, T source)
        {
            if (!predicate())
                destination = source;
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
                strategy = new DebugModeStrategy
                {
                    Parameters = new StrategyParameters { Message = $"{argumentName ?? "Argument"} is null." }
                };
            else
                strategy = new ThrowExceptionStrategy<ArgumentNullException>(argumentName);
            var contract = new StrategyContract(strategy)
            {
                Predicate = () => value != null
            };

            contract.Check();
        }

        public static void IndexIsValidForCollection(ICollection collection, int index)
        {
            IStrategy strategy;
            if (ContractsGlobalSettings.UseDebugModeWhenThrowException)
                strategy = new DebugModeStrategy
                {
                    Parameters = new StrategyParameters { Message = "Index is out of range." }
                };
            else
                strategy = new ThrowExceptionStrategy<IndexOutOfRangeException>("Index is out of range.");

            var contract = new StrategyContract(strategy)
            {
                Predicate = () => index >= 0 && collection.Count > index
            };

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
                strategy = new DebugModeStrategy
                {
                    Parameters = new StrategyParameters { Message = "Argument is out of range." }
                };
            else
                strategy = new ThrowExceptionStrategy<ArgumentOutOfRangeException>("Argument is out of range.");

            var contract = new StrategyContract(strategy)
            {
                Predicate = () =>
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
                }
            };

            contract.Check();
        }
    }
}
