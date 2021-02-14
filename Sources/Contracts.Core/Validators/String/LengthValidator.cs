using System;

namespace Contracts.Validators
{
    public class LengthValidator : IValidator<string>
    {
        #region Constructor

        public LengthValidator(int minimum, int maximum)
        {
            if (minimum < 0)
                throw new ArgumentOutOfRangeException("Minimum must be greather than or equal to 0.");

            if (maximum < minimum)
                throw new ArgumentOutOfRangeException("Maximum must be greather than or equal to minimum.");

            Minimum = minimum;
            Maximum = maximum;
        }

        public LengthValidator(int minimum)
        {
            if (minimum < 0)
                throw new ArgumentOutOfRangeException("Minimum must be greather than or equal to 0.");

            Minimum = minimum;
        }

        #endregion

        #region Properties

        public int Minimum { get; set; }

        public int Maximum { get; set; }

        #endregion

        public bool Validate(string value) =>
            (value?.Length ?? 0) >= Minimum
            && Maximum > 0
            && (value?.Length ?? 0) <= Maximum;
    }
}
