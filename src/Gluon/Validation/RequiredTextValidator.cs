using System.ComponentModel;
using System.Windows.Forms;

namespace Gluon.Validation
{
    public class RequiredTextValidator : Component, IControlValidator
    {
        public RequiredTextValidator()
        {
            ErrorMessage = DefaultErrorMessage;
        }

        [DefaultValue(DefaultErrorMessage)]
        [Category("Appearance")]
        [Description("The error message shown when a control fails to validate.")]
        public string ErrorMessage { get; set; }
        private const string DefaultErrorMessage = "This value is required.";

        public ValidationResult Validate(Control control)
        {
            if (string.IsNullOrEmpty(control.Text.Trim()))
            {
                return ValidationResult.Invalid(ErrorMessage);
            }
            return ValidationResult.Valid;
        }
    }
}
