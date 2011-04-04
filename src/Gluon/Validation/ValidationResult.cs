using System;

namespace Gluon.Validation
{
    public sealed class ValidationResult
    {
        private ValidationResult(string errorMessage)
        {
            this.errorMessage = errorMessage;
        }

        private static class Singleton
        {
            public static readonly ValidationResult Valid = new ValidationResult(null);
        }
        public static ValidationResult Valid { get { return Singleton.Valid; } }
        public static ValidationResult Invalid(string errorMessage)
        {
            if (errorMessage == null) throw new ArgumentNullException("errorMessage");
            return new ValidationResult(errorMessage);
        }

        private readonly string errorMessage;
        public bool IsValid { get { return errorMessage == null; } }
        public string ErrorMessage { get { return errorMessage; } }

        public static bool operator true(ValidationResult result)
        {
            return result.IsValid;
        }
        public static bool operator false(ValidationResult result)
        {
            return !result.IsValid;
        }
    }
}
