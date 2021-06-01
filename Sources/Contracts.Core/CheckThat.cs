using System;
using System.Collections.Generic;
using System.Linq;

using Contracts.Models;
using Contracts.Strategies;
using Contracts.Validators;

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
            if (toCheck is null)
                throw new ArgumentNullException(nameof(toCheck));

            toCheck.Check();
        }

        /// <summary>
        /// Runs action, when predicate is false.
        /// </summary>
        /// <param name="predicate">Predicate to check</param>
        /// <param name="onFailure">Action to run</param>
        /// <exception cref="ArgumentNullException" />
        public static void CheckAndRunIfFalse(Func<bool> predicate, Action onFailure)
        {
            if(predicate is null)
                throw new ArgumentNullException(nameof(predicate));

            if(onFailure is null)
                throw new ArgumentNullException(nameof(onFailure));

            if (!predicate())
                onFailure();
        }

        /// <summary>
        /// Validates value with specified validators and throws specified Exception 
        /// </summary>
        /// <typeparam name="Tvalue">Type of value to be validated</typeparam>
        /// <typeparam name="Texception">Type of exception to throw</typeparam>
        /// <param name="value">Value to be validated</param>
        /// <exception cref="Texception" />
        /// <exception cref="NullReferenceException" />
        public static void ValidateValue<Tvalue, Texception>(Tvalue? value, params IValidator<Tvalue>[] validators)
            where Texception : Exception
        {
            foreach (var validator in validators)
            {
                var result = validator?.Validate(value);

                if(result is null)
                    throw new NullReferenceException();
                else if(!result.Value)
                    throw (Texception)Activator.CreateInstance(typeof(Texception));
            }
        }

        /// <summary>
        /// Validates value with specified validators
        /// </summary>
        /// <typeparam name="Tvalue">Type of value to be validated</typeparam>
        /// <param name="value">Value to be validated</param>
        /// <returns><see langword="true"/> if value is valid.</returns>
        public static bool? IsValid<Tvalue>(Tvalue? value, params IValidator<Tvalue>[] validators)
        {
            foreach(var validator in validators)
            {
                var result = validator?.Validate(value);

                if(result is null)
                    return null;
                else if(!result.Value)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Throws specified exception when values are not equal
        /// </summary>
        /// <typeparam name="TException">Exception to throw</typeparam>
        /// <typeparam name="Tvalue">Type of value to check</typeparam>
        public static void ThrowIfNotEqual<TException, Tvalue>(Tvalue expected, Tvalue actual)
            where TException : Exception
            where Tvalue : IEquatable<Tvalue>
        {
            var strategy = ContractsGlobalSettings.UseDebugModeWhenThrowException
                ?
                new DebugModeStrategy
                {
                    Parameters = new StrategyParameters { Message = "Argument is not equal to expected value." }
                }
                :
                (IStrategy)new ThrowExceptionStrategy<TException>("Argument is not equal to expected value.");

            var contract = new StrategyContract(strategy, () => actual?.Equals(expected) ?? false);

            contract.Check();
        }

        /// <summary>
        /// Set <paramref name="destination"/> value from <paramref name="source"/>, when <paramref name="predicate"/> returns <see langword="false"/>.
        /// </summary>
        /// <typeparam name="T">Type of value to set</typeparam>
        /// <param name="predicate">Predicate to check</param>
        public static void SetValueIfFalse<T>(Func<bool> predicate, ref T destination, T source)
        {
            if(predicate is null)
                throw new ArgumentNullException(nameof(predicate));

            if (!predicate())
                destination = source;
        }

        /// <summary>
        /// Throws <see cref="ArgumentNullException"/>, when <paramref name="value"/> is <see langword="null"/>.
        /// </summary>
        /// <typeparam name="T">Reference type</typeparam>
        /// <param name="value">Value to check</param>
        /// <exception cref="ArgumentNullException" />
        public static void NotNullArgument<T>(T value, string argumentName = "")
            where T : class
        {
            var strategy = ContractsGlobalSettings.UseDebugModeWhenThrowException
                ?
                new DebugModeStrategy
                {
                    Parameters = new StrategyParameters { Message = $"{argumentName ?? "Argument"} is null." }
                }
                :
                (IStrategy)new ThrowExceptionStrategy<ArgumentNullException>(argumentName);

            var contract = new StrategyContract(strategy, () => value is not null);

            contract.Check();
        }

        /// <summary>
        /// Check, that <paramref name="value"/> is not null. Otherwise throws <see cref="ArgumentNullException"/>.
        /// </summary>
        /// <param name="value">Value to check</param>
        /// <param name="argumentName">Name of argument that is checked</param>
        public static void NotNullArgument<T>(T? value, string argumentName = "")
            where T : struct
        {
            var strategy = ContractsGlobalSettings.UseDebugModeWhenThrowException
                ?
                new DebugModeStrategy
                {
                    Parameters = new StrategyParameters { Message = $"{argumentName ?? "Argument"} is null." }
                }
                :
                (IStrategy)new ThrowExceptionStrategy<ArgumentNullException>(argumentName);

            var contract = new StrategyContract(strategy, () => value.HasValue);

            contract.Check();
        }

        /// <summary>
        /// Checks, that index is usable for specified enumerable type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="index"></param>
        public static void IndexIsValidForEnumerable<T>(IEnumerable<T> collection, int index)
        {
            var strategy = ContractsGlobalSettings.UseDebugModeWhenThrowException
                ?
                new DebugModeStrategy
                {
                    Parameters = new StrategyParameters { Message = "Index is out of range." }
                }
                :
                (IStrategy)new ThrowExceptionStrategy<IndexOutOfRangeException>("Index is out of range.");

            var contract = new StrategyContract(strategy, () => index >= 0 && collection.Count() > index);

            contract.Check();
        }

        /// <summary>
        /// Checks, that value exists in specified range
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rangeType">Range limit kind</param>
        public static void InRangeOf<T>(T value, T minimum, T maximum, RangeInclusiveKind rangeType = RangeInclusiveKind.Minimum | RangeInclusiveKind.Maximum)
            where T : IComparable<T>
        {
            if(value is null)
                throw new ArgumentNullException(nameof(value));

            if(minimum is null)
                throw new ArgumentNullException(nameof(minimum));

            if(maximum is null)
                throw new ArgumentNullException(nameof(maximum));

            if (minimum.CompareTo(maximum) > 0)
                throw new ArgumentException("Minimum is greater than maximum.");

            if (maximum.CompareTo(minimum) < 0)
                throw new ArgumentException("Maximum is lower than minimum.");

            var strategy = ContractsGlobalSettings.UseDebugModeWhenThrowException
                ?
                new DebugModeStrategy
                {
                    Parameters = new StrategyParameters { Message = "Argument is out of range." }
                }
                :
                (IStrategy)new ThrowExceptionStrategy<ArgumentOutOfRangeException>("Argument is out of range.");

            var contract = new StrategyContract(strategy,
                () =>
                    value.CompareTo(minimum) > 0  &&
                    value.CompareTo(maximum) < 0  ||
                    value.CompareTo(minimum) == 0 ||
                    value.CompareTo(maximum) == 0
            );

            contract.Check();
        }
    }
}
