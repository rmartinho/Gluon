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

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Gluon.Validation
{
    [ToolboxBitmap(typeof(Button))]
    public class ValidationButton : Button
    {
        [Category("Behavior")]
        [Description("The component that provides validation functionality.")]
        public ValidationProvider ValidationProvider { get; set; }

        protected override void OnClick(EventArgs e)
        {
            var oldDialogResult = DialogResult;
            try
            {
                if (ValidationProvider != null && !ValidationProvider.ValidateAll())
                {
                    DialogResult = DialogResult.None;
                }
                base.OnClick(e);
            }
            finally
            {
                DialogResult = oldDialogResult;
            }
        }
    }
}
