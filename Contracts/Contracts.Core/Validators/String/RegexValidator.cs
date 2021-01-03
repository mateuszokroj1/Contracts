using System.Text.RegularExpressions;
using System;

namespace Contracts.Validators
{
    public class RegexValidator : IValidator<string>
    {
        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pattern"></param>
        /// <exception cref="ArgumentException" />
        /// <exception cref="ArgumentNullException" />
        public RegexValidator(string pattern) =>
            RegularExpression = new Regex(pattern);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="regex"></param>
        /// <exception cref="ArgumentNullException" />
        public RegexValidator(Regex regex) =>
            RegularExpression = regex ?? throw new ArgumentNullException(nameof(regex));

        #endregion

        #region Properties

        public Regex RegularExpression { get; set; }

        #endregion

        public bool Validate(string value) => value != null && RegularExpression.IsMatch(value);
    }
}
