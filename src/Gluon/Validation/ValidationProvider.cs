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
using System.Diagnostics;
using System.Windows.Forms;

using Gluon.Utils;

namespace Gluon.Validation
{
    [ToolboxItemFilter("System.Windows.Forms")]
    [ProvideProperty("Validator", typeof (Control))]
    public class ValidationProvider : ErrorProvider, IExtenderProvider
    {
        private bool allowChangingFocus = true;

        [DefaultValue(true)]
        [Category("Behavior")]
        [Description("Whether invalid controls can change focus.")]
        public bool AllowChangingFocus
        {
            get { return this.allowChangingFocus; }
            set { this.allowChangingFocus = value; }
        }

        private readonly IDictionary<Control, IControlValidator> validators =
            new Dictionary<Control, IControlValidator>();

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
                    control.Validating -= this.Validate;
                }
            }
            else
            {
                this.validators[control] = validator;
                control.Validating += this.Validate;
            }
        }

        private void Validate(object sender, CancelEventArgs e)
        {
            Ensure.ArgumentNotNull(e, "e");
            if (sender == null)
            {
                return;
            }

            var control = (Control) sender;
            var result = this.validators[control].Validate(control);
            if (!result.IsValid)
            {
                this.SetError(control, result.ErrorMessage);
                if (!this.AllowChangingFocus)
                {
                    e.Cancel = true;
                }
            }
            else
            {
                this.SetError(control, null);
            }
        }

        public bool ValidateAll()
        {
            var valid = true;
            var e = new CancelEventArgs();
            foreach (var control in this.validators.Keys)
            {
                Debug.Assert(control != null);
                this.Validate(control, e);
                valid = valid && string.IsNullOrEmpty(this.GetError(control));
            }
            return valid;
        }

        bool IExtenderProvider.CanExtend(object extendee)
        {
            return extendee is Control;
        }
    }
}