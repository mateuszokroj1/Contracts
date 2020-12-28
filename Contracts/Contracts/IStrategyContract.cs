using Contracts.Strategies;

namespace Contracts
{
    public interface IStrategyContract
    {
        IStrategy Strategy { get; }
    }
}