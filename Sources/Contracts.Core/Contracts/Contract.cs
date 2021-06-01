using System;

namespace Contracts
{
    /// <summary>
    /// Base contract
    /// </summary>
    public class Contract : IContract
    {
        #region Constructors

        /// <summary>
        /// Creates a new instance and sets predicate and final action.
        /// </summary>
        /// <param name="predicate">Predicate to check</param>
        /// <param name="onFailure">Action to invoke, if predicate returns <see langword="false"/></param>
        /// <exception cref="ArgumentNullException" />
        public Contract(Func<bool> predicate, Action<object> onFailure)
        {
            Predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
            OnFailure = onFailure ?? throw new ArgumentNullException(nameof(onFailure));
        }

        /// <summary>
        /// Creates a new instance and sets predicate and final action.
        /// </summary>
        /// <param name="predicate">Predicate to check</param>
        /// <param name="onFailure">Action to invoke, if predicate returns <see langword="false"/></param>
        public Contract(Func<bool> predicate, Action onFailure) :
            this(predicate, parameters => onFailure?.Invoke()) { }

        protected Contract() { }

        #endregion

        #region Properties

        public virtual Func<bool> Predicate { get; set; }

        public virtual Action<object> OnFailure { get; set; }

        /// <summary>
        /// Parameters used in OnFailure action.
        /// </summary>
        public virtual object? Parameters { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Checks value in Predicate and invokes OnFailure if needed.
        /// </summary>
        /// <exception cref="InvalidOperationException" />
        public void Check()
        {
            if (Predicate is null || OnFailure is null)
                throw new InvalidOperationException("Check that properties are valid values.");

            if (!Predicate.Invoke())
                OnFailure.Invoke(Parameters);
        }

        #endregion
    }
}
