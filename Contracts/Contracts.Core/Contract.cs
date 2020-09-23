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

            Check();
        }

        public Contract(Func<bool> predicate, Action onFailure) :
            this(predicate, parameters => onFailure?.Invoke()) { }

        protected Contract() { }

        #endregion

        #region Properties

        public Func<bool> Predicate { get; set; }

        public Action<object> OnFailure { get; set; }

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
