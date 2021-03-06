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

using Wheels.Annotations;

namespace Gluon.Validation.ErrorFormatters
{
    public sealed class NoErrorFormatter : IErrorFormatter
    {
        private NoErrorFormatter() {}

        private static class Singleton
        {
            [NotNull] public static readonly IErrorFormatter Value = new NoErrorFormatter();
        }

        [NotNull]
        public static IErrorFormatter Instance
        {
            get { return Singleton.Value; }
        }

        public void ShowError(Control control, string error) {}

        public void HideError(Control control) {}
    }
}
