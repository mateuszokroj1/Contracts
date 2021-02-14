using Contracts.Strategies;

using System;

namespace Contracts
{
    /// <summary>
    /// Contract that uses strategies to work
    /// </summary>
    public class StrategyContract : Contract, IStrategyContract
    {
        #region Constructors

        /// <summary>
        /// Creates new instance with selected strategy object
        /// </summary>
        /// <param name="strategy"></param>
        public StrategyContract(IStrategy strategy) : base()
        {
            Strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
            base.OnFailure = arguments => Strategy.Do();
        }

        #endregion

        #region Properties

        public IStrategy Strategy { get; internal set; }

        [Obsolete]
        private new Action<object> OnFailure => base.OnFailure;

        [Obsolete]
        private new object Parameters => base.Parameters;

        #endregion
    }
}
