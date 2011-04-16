#region Copyright and license information

// Copyright 2011 Martinho Fernandes
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#endregion

using Gluon.Annotations;
using Gluon.Utils;

namespace Gluon.Validation
{
    public sealed class ValidationResult
    {
        private ValidationResult([CanBeNull] string errorMessage)
        {
            Ensure.ArgumentNotNullOrEmpty(errorMessage, "errorMessage");
            this.errorMessage = errorMessage;
        }

        private static class Singleton
        {
            [NotNull] public static readonly ValidationResult Value = new ValidationResult(null);
        }

        [NotNull]
        public static ValidationResult Valid
        {
            get { return Singleton.Value; }
        }

        [NotNull]
        public static ValidationResult Invalid([NotNull] string errorMessage)
        {
            Ensure.ArgumentNotNull(errorMessage, "errorMessage");
            return new ValidationResult(errorMessage);
        }

        [CanBeNull] private readonly string errorMessage;

        public bool IsValid
        {
            get { return this.errorMessage == null; }
        }

        [NotNull]
        public string ErrorMessage
        {
            get
            {
                Ensure.State(this.errorMessage != null, "Cannot retrieve error message from valid result.");
                return this.errorMessage;
            }
        }

        public static bool operator true([NotNull] ValidationResult result)
        {
            Ensure.ArgumentNotNull(result, "result");
            return result.IsValid;
        }

        public static bool operator false([NotNull] ValidationResult result)
        {
            Ensure.ArgumentNotNull(result, "result");
            return !result.IsValid;
        }
    }
}