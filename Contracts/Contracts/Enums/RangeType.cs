using System;

namespace Contracts
{
    [Flags]
    public enum RangeType
    {
        Exclusive = 0,
        MinInclusive = 1,
        MaxInclusive = 2
    }
}
