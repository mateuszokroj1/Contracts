namespace Contracts.Strategies
{
    /// <summary>
    /// Strategy interface
    /// </summary>
    public interface IStrategy
    {
        /// <summary>
        /// Parameters used during work
        /// </summary>
        object Parameters { get; set; }

        /// <summary>
        /// Basic operation to invoke when failure.
        /// </summary>
        void Do();
    }
}
