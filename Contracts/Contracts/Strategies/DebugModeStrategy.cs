using Contracts.Models;
using System.Diagnostics;

namespace Contracts.Strategies
{
    public class DebugModeStrategy : IStrategy
    {
        public object Parameters { get; set; }

        public void Do()
        {
            if(Parameters is StrategyParameters parameters && !string.IsNullOrWhiteSpace(parameters.Message))
                Debug.Fail(parameters.Message);

            Debugger.Break();
        }
    }
}