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
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Forms;

using Gluon.Annotations;
using Gluon.Utils;

namespace Gluon.Validation
{
    public class RegexValidator : Component, IControlValidator
    {
        private const string DefaultMessage = "This value is not in the correct format.";
        private string message = DefaultMessage;

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

        private const string DefaultExpression = ".*";
        [NotNull] private string expression = DefaultExpression;

        [NotNull, DefaultValue(DefaultExpression)]
        [Category("Behavior")]
        [Description("The regular expression used for validation.")]
        public string Expression
        {
            get { return this.expression; }
            set
            {
                Ensure.ArgumentNotNull(value, "value");
                this.expression = value;
            }
        }

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
            Ensure.ArgumentNotNull(control, "control");

            Debug.Assert(control.Text != null);
            if (!Regex.IsMatch(control.Text, Expression, RegexOptions))
            {
                return ValidationResult.Invalid(Message);
            }
            return ValidationResult.Valid;
        }

        private RegexOptions RegexOptions
        {
            get
            {
                var options = RegexOptions.ExplicitCapture;
                if (IgnoreCase)
                {
                    options |= RegexOptions.IgnoreCase;
                }
                if (Multiline)
                {
                    options |= RegexOptions.Multiline;
                }
                if (Singleline)
                {
                    options |= RegexOptions.Singleline;
                }
                return options;
            }
        }
    }
}
