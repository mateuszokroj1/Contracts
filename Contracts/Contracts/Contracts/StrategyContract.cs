using Contracts.Strategies;

using System;

namespace Contracts
{
    public class StrategyContract : Contract
    {
        #region Constructors

        public StrategyContract(IStrategy strategy) : base()
        {
            Strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
            base.OnFailure = arguments => Strategy.Do();
        }

        #endregion

        #region Properties

        internal IStrategy Strategy { get; set; }

        private new Action<object> OnFailure { get => base.OnFailure; }

        private new object Parameters { get => base.Parameters; }

        #endregion
    }
}
