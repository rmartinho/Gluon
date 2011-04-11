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

        private const string DefaultOutOfRangeMessage = "This number is out of range.";
        private string outOfRangeMessage = DefaultOutOfRangeMessage;

        [DefaultValue(DefaultOutOfRangeMessage)]
        [Category("Appearance")]
        [Description("The error message shown when a control has a value out of range.")]
        public string OutOfRangeMessage
        {
            get { return this.outOfRangeMessage; }
            set
            {
                Ensure.ArgumentNotNull(value, "value");
                this.outOfRangeMessage = value;
            }
        }

        private const string DefaultInvalidFormatMessage = "This value is not a number.";
        private string invalidFormatMessage = DefaultInvalidFormatMessage;

        [DefaultValue(DefaultInvalidFormatMessage)]
        [Category("Appearance")]
        [Description("The error message shown when a control does not have a numeric value.")]
        public string InvalidFormatMessage
        {
            get { return this.invalidFormatMessage; }
            set
            {
                Ensure.ArgumentNotNull(value, "value");
                this.invalidFormatMessage = value;
            }
        }

        public ValidationResult Validate(Control control)
        {
            Ensure.ArgumentNotNull(control, "control");
            int value;
            if (!int.TryParse(control.Text, out value))
            {
                return ValidationResult.Invalid(InvalidFormatMessage);
            }
            if (value < Minimum || value > Maximum)
            {
                return ValidationResult.Invalid(OutOfRangeMessage);
            }
            return ValidationResult.Valid;
        }
    }
}
