using System;

namespace Contracts.Validators
{
    /// <summary>
    /// Checks, that string length is in range
    /// </summary>
    public class LengthValidator : IValidator<string?>
    {
        #region Constructor

        public LengthValidator(int minimum, int maximum) : this(minimum)
        {
            if(maximum < minimum)
                throw new ArgumentOutOfRangeException("Maximum must be greather than or equal to minimum.");

            Maximum = maximum;
        }

        public LengthValidator(int minimum)
        {
            if(minimum < 0)
                throw new ArgumentOutOfRangeException("Minimum must be greather than or equal to 0.");

            Minimum = minimum;
        }

        #endregion

        #region Properties

        public int Minimum { get; set; }

        public int? Maximum { get; set; }

        #endregion

        public bool? Validate(string? value)
        {
            if(value is null)
                return null;

            return Maximum.HasValue
                ?
                value.Length >= Minimum && value.Length <= Maximum.Value
                :
                value.Length >= Minimum;
        }
    }
}