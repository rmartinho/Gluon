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

using Gluon.Utils;

namespace Gluon.Validation
{
    public class NumberRangeValidator : Component, IControlValidator
    {
        private int minimum = int.MinValue;

        [DefaultValue(int.MinValue)]
        [Category("Behavior")]
        [Description("The minimum value.")]
        public int Minimum
        {
            get { return this.minimum; }
            set
            {
                Ensure.ArgumentInRange(this.minimum <= this.maximum, "value");
                this.minimum = value;
            }
        }

        private int maximum = int.MaxValue;

        [DefaultValue(int.MaxValue)]
        [Category("Behavior")]
        [Description("The maximum value.")]
        public int Maximum
        {
            get { return this.maximum; }
            set
            {
                Ensure.ArgumentInRange(this.maximum >= this.minimum, "value");
                this.maximum = value;
            }
        }

        private const string DefaultOutOfRangeErrorMessage = "This number is out of range.";
        private string outOfRangeErrorMessage = DefaultOutOfRangeErrorMessage;

        [DefaultValue(DefaultOutOfRangeErrorMessage)]
        [Category("Appearance")]
        [Description("The error message shown when a control has a value out of range.")]
        public string OutOfRangeErrorMessage
        {
            get { return this.outOfRangeErrorMessage; }
            set
            {
                Ensure.ArgumentNotNull(value, "value");
                this.outOfRangeErrorMessage = value;
            }
        }

        private const string DefaultInvalidFormatErrorMessage = "This value is not a number.";
        private string invalidFormatErrorMessage = DefaultInvalidFormatErrorMessage;

        [DefaultValue(DefaultInvalidFormatErrorMessage)]
        [Category("Appearance")]
        [Description("The error message shown when a control does not have a numeric value.")]
        public string InvalidFormatErrorMessage
        {
            get { return this.invalidFormatErrorMessage; }
            set
            {
                Ensure.ArgumentNotNull(value, "value");
                this.invalidFormatErrorMessage = value;
            }
        }

        public ValidationResult Validate(Control control)
        {
            Ensure.ArgumentNotNull(control, "control");
            int value;
            if (!int.TryParse(control.Text, out value))
            {
                return ValidationResult.Invalid(InvalidFormatErrorMessage);
            }
            if (value < Minimum || value > Maximum)
            {
                return ValidationResult.Invalid(OutOfRangeErrorMessage);
            }
            return ValidationResult.Valid;
        }
    }
}
