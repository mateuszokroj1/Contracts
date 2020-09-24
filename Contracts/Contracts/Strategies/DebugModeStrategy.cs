using Contracts.Models;
using System.Diagnostics;

namespace Contracts.Strategies
{
    public class DebugModeStrategy : IStrategy
    {
        public object Parameters { get; set; }

        public void Do()
        {
            if(Parameters is DebugModeStrategyParameters parameters && parameters.Message != null)
                Debug.Fail(parameters.Message);

            Debugger.Break();
        }
    }
}
