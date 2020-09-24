﻿using System;

namespace Contracts.Strategies
{
    public class ThrowExceptionStrategy<TException> : IStrategy
        where TException : Exception
    {
        #region Constructors

        public ThrowExceptionStrategy(string message = "")
        {
            Message = message;
        }

        #endregion

        #region Properties

        public string Message { get; set; }

        #endregion

        #region Methods

        public void Do()
        {
            throw Activator.CreateInstanceOfException<TException>(Message ?? string.Empty);
        }

        #endregion
    }
}
