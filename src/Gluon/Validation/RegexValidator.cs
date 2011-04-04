using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Gluon.Validation
{
    public class RegexValidator : Component, IControlValidator
    {
        public RegexValidator()
        {
            ErrorMessage = DefaultErrorMessage;
            Expression = DefaultExpression;
        }

        [DefaultValue(DefaultErrorMessage)]
        [Category("Appearance")]
        [Description("The error message shown when a control fails to validate.")]
        public string ErrorMessage { get; set; }
        private const string DefaultErrorMessage = "This value is not in the correct format.";

        [DefaultValue(DefaultExpression)]
        [Category("Behavior")]
        [Description("The regular expression used for validation.")]
        public string Expression { get; set; }
        private const string DefaultExpression = ".*";

        [DefaultValue(false)]
        [Category("Behavior")]
        [Description("Whether the regular expression should ignore case or not.")]
        public bool IgnoreCase { get; set; }

        [DefaultValue(false)]
        [Category("Behavior")]
        [Description("Whether the regular expression is multiline or not.")]
        public bool Multiline { get; set; }

        [DefaultValue(false)]
        [Category("Behavior")]
        [Description("Whether the regular expression is singleline or not.")]
        public bool Singleline { get; set; }

        public ValidationResult Validate(Control control)
        {
            var options = RegexOptions.ExplicitCapture;
            if (IgnoreCase) options |= RegexOptions.IgnoreCase;
            if (Multiline) options |= RegexOptions.Multiline;
            if (Singleline) options |= RegexOptions.Singleline;
            if (!Regex.IsMatch(control.Text, Expression, options))
            {
                return ValidationResult.Invalid(ErrorMessage);
            }
            return ValidationResult.Valid; 
        }
    }
}
