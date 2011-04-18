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

using System.Windows.Forms;

using Wheels;
using Wheels.Annotations;

namespace Gluon.Validation.Validators
{
    internal sealed class AlwaysTrueValidator : IControlValidator
    {
        private AlwaysTrueValidator() {}

        private static class Singleton
        {
            [NotNull] public static readonly IControlValidator Value = new AlwaysTrueValidator();
        }

        [NotNull]
        public static IControlValidator Instance
        {
            get { return Singleton.Value; }
        }

        public ValidationResult Validate(Control control)
        {
            Ensure.ArgumentNotNull(control, "control");
            return ValidationResult.Valid;
        }
    }
}
