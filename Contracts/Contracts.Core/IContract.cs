using System;

namespace Contracts
{
    public interface IContract
    {
        #region Properties

        /// <summary>
        /// Function to check
        /// </summary>
        Func<bool> Predicate { get; set; }

        /// <summary>
        /// Invokes, when value of predicate is <see langword="false"/>.
        /// </summary>
        Action<object> OnFailure { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Checks the predicate and invoke OnFailure, when result is <see langword="false"/>.
        /// </summary>
        void Check();

        #endregion
    }
}