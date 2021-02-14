namespace Contracts.Validators
{
    public class IsNullValidator : IValidator<string>
    {
        #region Constructor

        public IsNullValidator(bool isNegation = false, StringNullKind kind = StringNullKind.Null)
        {
            IsNegation = isNegation;
            Kind = kind;
        }

        #endregion

        #region Properties

        public bool IsNegation { get; set; }

        public StringNullKind Kind { get; set; } = StringNullKind.Null;

        #endregion

        public bool Validate(string value)
        {
            bool result;
            switch(Kind)
            {
                case StringNullKind.Null:
                default:
                    result = value == null;
                    break;
                case StringNullKind.Empty:
                    result = string.IsNullOrEmpty(value);
                    break;
                case StringNullKind.WhiteSpace:
                    result = string.IsNullOrWhiteSpace(value);
                    break;
            }

            return IsNegation ? !result : result;
        }
    }
}
