using System;

namespace Contracts
{
    /// <summary>
    /// Range type used in range selection
    /// </summary>
    [Flags]
    public enum RangeType
    {
        Exclusive = 0,
        MinInclusive = 1,
        MaxInclusive = 2
    }
}
