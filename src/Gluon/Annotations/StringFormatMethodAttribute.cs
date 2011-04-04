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

namespace Gluon.Annotations
{
    /// <summary>
    ///   Indicates that marked method builds string by format pattern and (optional) arguments. 
    ///   Parameter, which contains format string, should be given in constructor.
    ///   The format string should be in <see cref = "string.Format(IFormatProvider,string,object[])" /> -like form
    /// </summary>
    [AttributeUsage(
        AttributeTargets.Constructor | AttributeTargets.Method,
        AllowMultiple = false,
        Inherited = true)]
    internal sealed class StringFormatMethodAttribute : Attribute
    {
        private readonly string myFormatParameterName;

        /// <summary>
        ///   Initializes new instance of StringFormatMethodAttribute
        /// </summary>
        /// <param name = "formatParameterName">Specifies which parameter of an annotated method should be treated as format-string</param>
        public StringFormatMethodAttribute(string formatParameterName)
        {
            this.myFormatParameterName = formatParameterName;
        }

        /// <summary>
        ///   Gets format parameter name
        /// </summary>
        public string FormatParameterName
        {
            get { return this.myFormatParameterName; }
        }
    }
}
