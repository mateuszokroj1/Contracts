using Contracts.Strategies;

using System;

namespace Contracts
{
    public class StrategyContract : Contract, IStrategyContract
    {
        #region Constructors

        public StrategyContract(IStrategy strategy) : base()
        {
            Strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
            base.OnFailure = arguments => Strategy.Do();
        }

        #endregion

        #region Properties

        public IStrategy Strategy { get; internal set; }

        private new Action<object> OnFailure => base.OnFailure;

        private new object Parameters => base.Parameters;

        #endregion
    }
}
