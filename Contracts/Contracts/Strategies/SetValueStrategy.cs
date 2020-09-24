using System;

namespace Contracts.Strategies
{
    public class SetValueStrategy<T> : IStrategy
    {
        #region Constructors

        public SetValueStrategy(ref T destination, T source)
        {
            this.destination = destination;
            this.source = source;
        }

        #endregion

        #region Fields

        private object destination;
        private T source;

        #endregion

        #region Properties

        [Obsolete]
        public object Parameters { get; set; }

        #endregion

        #region Methods

        public void Do() => this.destination = this.source;

        #endregion
    }
}
