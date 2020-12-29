using Contracts.Models;
using System;

namespace Contracts.Strategies
{
    public class ThrowExceptionStrategy<TException> : IStrategy
        where TException : Exception
    {
        #region Constructors

        public ThrowExceptionStrategy(string message = "")
        {
            Parameters = new StrategyParameters { Message = message };
        }

        #endregion

        #region Properties

        public object Parameters { get; set; }

        public string Message
        {
            get => (Parameters as StrategyParameters)?.Message;
            set
            {
                if (Parameters is StrategyParameters parameters)
                    parameters.Message = value;
                else
                    Parameters = new StrategyParameters { Message = value };
            }
        }

        #endregion

        #region Methods

        public void Do()
        {
            if(string.IsNullOrEmpty(Message))
                throw Activator.CreateInstance(typeof(TException)) as Exception;
            else
                throw Activator.CreateInstance(typeof(TException), Message) as Exception;
        }

        #endregion
    }
}
