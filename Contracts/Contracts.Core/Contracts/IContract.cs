using System;

namespace Contracts
{
    /// <summary>
    /// Contract interface
    /// </summary>
    public interface IContract
    {
        #region Properties

        /// <summary>
        /// Function to check
        /// </summary>
        Func<bool> Predicate { get; }

        /// <summary>
        /// Invokes, when value of predicate is <see langword="false"/>.
        /// </summary>
        Action<object> OnFailure { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Checks the predicate and invoke OnFailure, when result is <see langword="false"/>.
        /// </summary>
        void Check();

        #endregion
    }
}