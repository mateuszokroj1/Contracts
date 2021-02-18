using System;

namespace Contracts.Validators.Number
{
    public class ApproximatelyEqualValidator : IValidator<float>, IValidator<double>
    {
        public ApproximatelyEqualValidator(float compareWith) => this.compareWith = compareWith;

        public ApproximatelyEqualValidator(double compareWith) => this.compareWith = compareWith;

        public ApproximatelyEqualValidator(decimal compareWith) => this.compareWith = compareWith;

        private object compareWith;

        public bool Validate(float value)
        {
            if (this.compareWith is float f)
            {
                var diff = value - f;

                return diff <= float.Epsilon && diff >= -float.Epsilon;
            }
            else
                throw new ArgumentException("CompareWith is not float.");
        }

        public bool Validate(double value)
        {
            if (this.compareWith is double d)
            {
                var diff = value - d;

                return diff <= double.Epsilon && diff >= -double.Epsilon;
            }
            else
                throw new ArgumentException("CompareWith is not double.");
        }
    }
}
