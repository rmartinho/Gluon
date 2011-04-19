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
using System.Drawing;
using System.Windows.Forms;

using Wheels;
using Wheels.Annotations;

namespace Gluon.Validation.ErrorFormatters
{
    public class BackgroundFormatter : Component, IErrorFormatter
    {
        private Color errorColor = Color.MistyRose;

        [DefaultValue(typeof(Color), "MistyRose")]
        [Category("Appearance")]
        [Description("The color used to mark errors.")]
        public Color ErrorColor
        {
            get { return this.errorColor; }
            set { this.errorColor = value; }
        }

        [NotNull] private readonly IDictionary<Control, Color> oldColors =
            new Dictionary<Control, Color>();

        public void ShowError(Control control, string error)
        {
            Ensure.ArgumentNotNull(control, "control");
            Ensure.ArgumentNotNull(error, "error");
            if (this.oldColors.ContainsKey(control))
            {
                return;
            }
            this.oldColors[control] = control.BackColor;
            control.BackColor = this.ErrorColor;
        }

        public void HideError(Control control)
        {
            Ensure.ArgumentNotNull(control, "control");
            control.BackColor = this.oldColors[control];
            this.oldColors.Remove(control);
        }
    }
}
