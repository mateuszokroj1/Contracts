using System;

namespace Contracts.Validators
{
    /// <summary>
    /// Checks, that value is in range
    /// </summary>
    /// <typeparam name="TNumber">Type of value</typeparam>
    public class RangeValidator<TNumber> : IValidator<TNumber>
        where TNumber : struct, IComparable<TNumber>
    {
        public RangeValidator(TNumber minimum, TNumber maximum = default, RangeInclusiveKind inclusiveType = 0)
        {
            Minimum = minimum;
            Maximum = maximum;
            RangeInclusive = inclusiveType;
        }

        public RangeValidator() { }

        public TNumber Minimum { get; set; }

        public TNumber Maximum { get; set; }

        public RangeInclusiveKind RangeInclusive { get; set; }

        /// <exception cref="ArgumentOutOfRangeException" />
        public bool? Validate(TNumber value)
        {
            if (Minimum.CompareTo(Maximum) > 0)
                throw new ArgumentOutOfRangeException(nameof(Minimum));

            if(Maximum.CompareTo(Minimum) < 0)
                throw new ArgumentOutOfRangeException(nameof(Maximum));

            if (RangeInclusive.HasFlag(RangeInclusiveKind.Minimum))
            {
                if (value.CompareTo(Minimum) < 0)
                    return false;
            }
            else
            {
                if (value.CompareTo(Minimum) <= 0)
                    return false;
            }

            if (RangeInclusive.HasFlag(RangeInclusiveKind.Maximum))
            {
                if (value.CompareTo(Maximum) > 0)
                    return false;
            }
            else
            {
                if (value.CompareTo(Maximum) >= 0)
                    return false;
            }

            return true;
        }
    }
}
