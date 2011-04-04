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

using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using Gluon.Utils;

namespace Gluon.Validation
{
    [ToolboxItemFilter("System.Windows.Forms")]
    [ProvideProperty("Validator", typeof(Control))]
    public class ValidationProvider : Component, IExtenderProvider
    {
        bool IExtenderProvider.CanExtend(object extendee)
        {
            return extendee is Control;
        }

        [DefaultValue(ValidationMode.Automatic)]
        [Category("Behavior")]
        [Description("When to do validation: either automatically or only when manually invoked.")]
        public ValidationMode ValidationMode
        {
            get { return this.validationMode; }
            set
            {
                Ensure.EnumArgument(value, "value");
                if (value == this.validationMode)
                {
                    return;
                }
                if (value == ValidationMode.Automatic)
                {
                    foreach (var control in this.validators.Keys)
                    {
                        AttachValidation(control);
                    }
                }
                if (value == ValidationMode.Manual)
                {
                    foreach (var control in this.validators.Keys)
                    {
                        DetachValidation(control);
                    }
                }
                this.validationMode = value;
            }
        }

        private ValidationMode validationMode;

        [Category("Behavior")]
        [Description("The validator for this control.")]
        public IControlValidator GetValidator(Control control)
        {
            Ensure.ArgumentNotNull(control, "control");
            IControlValidator validator;
            if (!this.validators.TryGetValue(control, out validator))
            {
                return AlwaysTrueValidator.Instance;
            }
            return validator;
        }

        public void SetValidator(Control control, IControlValidator validator)
        {
            Ensure.ArgumentNotNull(control, "control");
            if (validator == null)
            {
                if (this.validators.Remove(control))
                {
                    DetachValidation(control);
                }
            }
            else
            {
                this.validators[control] = validator;
                AttachValidation(control);
            }
        }

        private readonly IDictionary<Control, IControlValidator> validators =
            new Dictionary<Control, IControlValidator>();

        private void AttachValidation(Control control)
        {
            control.Validating += Validate;
        }

        private void DetachValidation(Control control)
        {
            control.Validating -= Validate;
        }

        private void Validate(object sender, CancelEventArgs e)
        {
            if (sender == null)
            {
                return;
            }
            var control = (Control)sender;
            var result = this.validators[control].Validate(control);
            if (!result.IsValid)
            {
                MessageBox.Show(result.ErrorMessage);
                //e.Cancel = true;
            }
        }

        public void ValidateAll()
        {
            foreach (var item in this.validators)
            {
                var control = item.Key;
                var validator = item.Value;
                validator.Validate(control);
            }
        }
    }

    public enum ValidationMode
    {
        Automatic,
        Manual
    }
}
