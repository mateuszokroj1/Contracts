using System;

namespace Contracts.Validators
{
    public class NotEqualValidator<TNumber> : IValidator<TNumber>
        where TNumber : IEquatable<TNumber>
    {
        public NotEqualValidator(TNumber compareWith = default) => CompareWith = compareWith;

        public TNumber CompareWith { get; set; }

        public bool Validate(TNumber value)
        {
            try
            {
                return !(value?.Equals(CompareWith) ?? false);
            }
            catch(ArgumentNullException)
            {
                return true;
            }
        }
    }
}
