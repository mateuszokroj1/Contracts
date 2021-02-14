namespace Contracts
{
    /// <summary>
    /// Global settings for contracts
    /// </summary>
    public static class ContractsGlobalSettings
    {
        /// <summary>
        /// Forces fail attached debugger when possible after contract failure.
        /// </summary>
        public static bool UseDebugModeWhenThrowException { get; set; } = false;
    }
}
