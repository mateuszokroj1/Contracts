using System.Text.RegularExpressions;
using System;

namespace Contracts.Validators
{
    /// <summary>
    /// Checks, that string value is match in regular expression
    /// </summary>
    public class RegexValidator : IValidator<string?>
    {
        #region Constructor

        /// <param name="pattern"></param>
        /// <exception cref="ArgumentException" />
        /// <exception cref="ArgumentNullException" />
        public RegexValidator(string pattern) =>
            RegularExpression = new Regex(pattern);

        /// <param name="regex"></param>
        /// <exception cref="ArgumentNullException" />
        public RegexValidator(Regex regex) =>
            RegularExpression = regex ?? throw new ArgumentNullException(nameof(regex));

        #endregion

        #region Properties

        public Regex RegularExpression { get; set; }

        #endregion

        public bool? Validate(string? value) =>
            value is not null ? RegularExpression.IsMatch(value) : null;
    }
}
