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
            Parameters = new ThrowExceptionStrategyParameters { Message = message };
        }

        #endregion

        #region Properties

        public object Parameters { get; set; }

        public string Message
        {
            get => (Parameters as ThrowExceptionStrategyParameters)?.Message;
            set
            {
                if (Parameters is ThrowExceptionStrategyParameters parameters)
                    parameters.Message = value;
                else
                    Parameters = new ThrowExceptionStrategyParameters { Message = value };
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
