using System;

namespace Contracts
{
    public class Contract : IContract
    {
        #region Constructors

        public Contract(Func<bool> predicate, Action<object> onFailure)
        {
            Predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
            OnFailure = onFailure ?? throw new ArgumentNullException(nameof(onFailure));
        }

        public Contract(Func<bool> predicate, Action onFailure) :
            this(predicate, parameters => onFailure?.Invoke()) { }

        protected Contract() { }

        #endregion

        #region Properties

        public virtual Func<bool> Predicate { get; set; }

        public virtual Action<object> OnFailure { get; set; }

        public virtual object Parameters { get; set; }

        #endregion

        #region Methods

        /// <exception cref="InvalidOperationException" />
        public void Check()
        {
            if (Predicate == null || OnFailure == null)
                throw new InvalidOperationException();

            if (!Predicate.Invoke())
                OnFailure.Invoke(Parameters);
        }

        #endregion
    }
}
