﻿#region Copyright and license information
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
            Ensure.ArgumentNotNull(control, "control");
            if (string.IsNullOrEmpty(control.Text.Trim()))
            {
                return ValidationResult.Invalid(ErrorMessage);
            }
            return ValidationResult.Valid;
        }
    }
}
