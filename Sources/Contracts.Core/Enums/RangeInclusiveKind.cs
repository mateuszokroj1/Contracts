using System;

namespace Contracts
{
    [Flags]
    public enum RangeInclusiveKind
    {
        Exclusive = 0,
        Minimum = 1,
        Maximum = 2
    }
}
