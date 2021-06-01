namespace Contracts.Validators
{
    /// <summary>
    /// Checks, that string value is <see langword="null"/>, empty or whitespace.
    /// </summary>
    public class IsNullValidator : IValidator<string?>
    {
        #region Constructor

        public IsNullValidator(bool isNegated = false, StringNullKind kind = StringNullKind.Null)
        {
            IsNegated = isNegated;
            Kind = kind;
        }

        #endregion

        #region Properties

        public bool IsNegated { get; set; }

        public StringNullKind Kind { get; set; }

        #endregion

        public bool? Validate(string? value)
        {
            var result = Kind switch
            {
                StringNullKind.Empty => string.IsNullOrEmpty(value),
                StringNullKind.WhiteSpace => string.IsNullOrWhiteSpace(value),
                _ => value is null,
            };

            return IsNegated ? !result : result;
        }
    }
}
