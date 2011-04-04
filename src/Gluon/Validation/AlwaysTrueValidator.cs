using System.Windows.Forms;

namespace Gluon.Validation
{
    internal sealed class AlwaysTrueValidator : IControlValidator
    {
        private AlwaysTrueValidator() { }

        private static class Singleton
        {
            public static readonly IControlValidator Instance = new AlwaysTrueValidator();
        }
        public static IControlValidator Instance { get { return Singleton.Instance; } }

        public ValidationResult Validate(Control control)
        {
            return ValidationResult.Valid;
        }
    }
}
