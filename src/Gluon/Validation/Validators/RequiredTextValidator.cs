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

using System.ComponentModel;
using System.Windows.Forms;

using Wheels;
using Wheels.Annotations;

namespace Gluon.Validation.Validators
{
    public class RequiredTextValidator : Component, IControlValidator
    {
        public RequiredTextValidator()
        {
            this.Message = DefaultMessage;
        }

        [NotNull] private string message;

        [NotNull]
        [DefaultValue(DefaultMessage)]
        [Category("Appearance")]
        [Description("The error message shown when a control fails to validate.")]
        public string Message
        {
            get { return this.message; }
            set
            {
                Ensure.ArgumentNotNull(value, "value");
                this.message = value;
            }
        }

        private const string DefaultMessage = "This value is required.";

        public ValidationResult Validate(Control control)
        {
            Ensure.ArgumentNotNull(control, "control");
            if (string.IsNullOrEmpty(control.Text.Trim()))
            {
                return ValidationResult.Invalid(this.Message);
            }
            return ValidationResult.Valid;
        }
    }
}
