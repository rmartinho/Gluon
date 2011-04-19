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
using System.Linq;
using System.Windows.Forms;

using Wheels;
using Wheels.Annotations;

namespace Gluon.Validation.ErrorFormatters
{
    public sealed class CompositeErrorFormatter : Component, IErrorFormatter
    {
        [NotNull] private readonly IList<IErrorFormatter> errorFormatters;

        [NotNull]
        public IList<IErrorFormatter> ErrorFormatters
        {
            get { return this.errorFormatters; }
        }

        public CompositeErrorFormatter() : this(Enumerable.Empty<IErrorFormatter>()) {}

        public CompositeErrorFormatter([NotNull] IEnumerable<IErrorFormatter> validators)
        {
            Ensure.ArgumentNotNull(validators, "validators");
            this.errorFormatters = new List<IErrorFormatter>(validators);
        }

        public void ShowError(Control control, string error)
        {
            Ensure.ArgumentNotNull(control, "control");
            Ensure.ArgumentNotNull(error, "error");
            foreach (var errorFormatter in this.errorFormatters)
            {
                errorFormatter.ShowError(control, error);
            }
        }

        public void HideError(Control control)
        {
            Ensure.ArgumentNotNull(control, "control");
            foreach (var errorFormatter in this.errorFormatters.Reverse())
            {
                errorFormatter.HideError(control);
            }
        }
    }
}
