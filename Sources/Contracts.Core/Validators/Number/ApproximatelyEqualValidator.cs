using System;

namespace Contracts.Validators.Number
{
    /// <summary>
    /// Checks, that floating point value is equal in range +- Epsilon
    /// </summary>
    public class ApproximatelyEqualValidator : IValidator<float>, IValidator<double>
    {
        #region Constructors

        public ApproximatelyEqualValidator(float compareWith, bool isNegated = false) 
        { 
            this.compareWithFloat = compareWith;
            this.isNegated = isNegated;
        }

        public ApproximatelyEqualValidator(double compareWith, bool isNegated = false)
        { 
            this.compareWithDouble = compareWith;
            this.isNegated = isNegated;
        }

        #endregion

        #region Fields

        private readonly float compareWithFloat;
        private readonly double compareWithDouble;
        private readonly bool isNegated;

        #endregion

        #region Methods

        public bool? Validate(float value)
        {
            var result = Math.Abs(value - compareWithFloat) <= float.Epsilon;
            return isNegated ? !result : result;
        }

        public bool? Validate(double value)
        {
            var result = Math.Abs(value - compareWithDouble) <= double.Epsilon;
            return isNegated ? !result : result;
        }

        #endregion
    }
}
