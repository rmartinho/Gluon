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
using System.Text;
using System.Windows.Forms;

using Wheels;
using Wheels.Annotations;

namespace Gluon.Validation.Validators
{
    public sealed class CompositeValidator : Component, IControlValidator
    {
        [NotNull] private readonly List<IControlValidator> validators;

        [NotNull]
        public IList<IControlValidator> Validators
        {
            get { return this.validators; }
        }

        public CompositeValidator() : this(Enumerable.Empty<IControlValidator>()) {}

        public CompositeValidator([NotNull] IEnumerable<IControlValidator> validators)
        {
            Ensure.ArgumentNotNull(validators, "validators");
            this.validators = new List<IControlValidator>(validators);
        }

        public ValidationResult Validate(Control control)
        {
            Ensure.ArgumentNotNull(control, "control");

            var fullMessage = new StringBuilder();
            foreach (var validator in this.validators)
            {
                var result = validator.Validate(control);
                if (!result.IsValid)
                {
                    fullMessage.AppendLine(result.ErrorMessage);
                }
            }
            if (fullMessage.Length > 0)
            {
                return ValidationResult.Invalid(fullMessage.ToString());
            }
            return ValidationResult.Valid;
        }
    }
}
