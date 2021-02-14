using Contracts.Strategies;

namespace Contracts
{
    /// <summary>
    /// StrategyContract interface
    /// </summary>
    public interface IStrategyContract
    {
        /// <summary>
        /// Selected strategy object
        /// </summary>
        IStrategy Strategy { get; }
    }
}