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
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Security;
using System.Security.Permissions;

using Gluon.Annotations;
using Gluon.Properties;

namespace Gluon.Utils
{
    /// <summary>
    ///   The <c>Ensure</c> class contains various preconditions verifiers.
    /// </summary>
    internal static class Ensure
    {
        [DebuggerNonUserCode]
        private static TException Create<TException>()
            where TException : Exception, new()
        {
            return new TException();
        }

        [DebuggerNonUserCode]
        private static TException Create<TException>([LocalizationRequired(true)] string message)
            where TException : Exception
        {
            return (TException)Activator.CreateInstance(typeof(TException), message);
        }

        [DebuggerNonUserCode]
        private static TException Create<TException>(
            [LocalizationRequired(true)] string message, Exception innerException)
            where TException : Exception
        {
            return (TException)Activator.CreateInstance(typeof(TException), message, innerException);
        }

        [StringFormatMethod("format")]
        [DebuggerNonUserCode]
        private static string Format([NotNull, LocalizationRequired(true)] string format,
                                     [NotNull] params object[] args)
        {
            return string.Format(CultureInfo.InvariantCulture, format, args);
        }

        /// <summary>
        ///   Guarantees that an argument verifies a specified condition.
        /// </summary>
        /// <param name = "condition">The condition to check.</param>
        /// <param name = "paramName">The name of the parameter.</param>
        /// <exception cref = "ArgumentException">
        ///   The <paramref name = "condition" /> is <c>false</c>.
        /// </exception>
        [AssertionMethod]
        [DebuggerNonUserCode]
        public static void Argument(
            [AssertionCondition(AssertionConditionType.IsTrue)] bool condition,
            [InvokerParameterName] string paramName)
        {
            if (!condition)
            {
                throw new ArgumentException(paramName);
            }
        }

        /// <summary>
        ///   Guarantees that an argument verifies a specified condition.
        /// </summary>
        /// <param name = "condition">The condition to check.</param>
        /// <param name = "paramName">The name of the parameter.</param>
        /// <param name = "format">Format string for the error message.</param>
        /// <param name = "args">Objects to be formatted.</param>
        /// <exception cref = "ArgumentException">
        ///   The <paramref name = "condition" /> is <c>false</c>.
        /// </exception>
        [AssertionMethod, StringFormatMethod("format")]
        [DebuggerNonUserCode]
        public static void Argument(
            [AssertionCondition(AssertionConditionType.IsTrue)] bool condition,
            [InvokerParameterName] string paramName,
            [NotNull, LocalizationRequired(true)] string format, [NotNull] params object[] args)
        {
            if (!condition)
            {
                throw new ArgumentException(Format(format, args), paramName);
            }
        }

        /// <summary>
        ///   Guarantees that an argument is not null.
        /// </summary>
        /// <param name = "obj">The argument.</param>
        /// <param name = "paramName">The name of the parameter.</param>
        /// <exception cref = "ArgumentNullException">
        ///   <paramref name = "obj" /> is <c>null</c>.
        /// </exception>
        [AssertionMethod]
        [DebuggerNonUserCode]
        public static void ArgumentNotNull(
            [AssertionCondition(AssertionConditionType.IsNotNull)] object obj,
            [InvokerParameterName] string paramName)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }

        /// <summary>
        ///   Guarantees that an argument is not null.
        /// </summary>
        /// <param name = "obj">The argument.</param>
        /// <param name = "paramName">The name of the parameter.</param>
        /// <param name = "format">Format string for the error message.</param>
        /// <param name = "args">Objects to be formatted.</param>
        /// <exception cref = "ArgumentNullException">
        ///   <paramref name = "obj" /> is <c>null</c>.
        /// </exception>
        [AssertionMethod, StringFormatMethod("format")]
        [DebuggerNonUserCode]
        public static void ArgumentNotNull(
            [AssertionCondition(AssertionConditionType.IsNotNull)] object obj,
            [InvokerParameterName] string paramName,
            [NotNull, LocalizationRequired(true)] string format, [NotNull] params object[] args)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(paramName, Format(format, args));
            }
        }

        /// <summary>
        ///   Guarantees that a string argument is not <c>null</c> nor empty.
        /// </summary>
        /// <param name = "value">The string argument.</param>
        /// <param name = "paramName">The name of the parameter.</param>
        /// <exception cref = "ArgumentException">
        ///   The <paramref name = "value" /> is <c>null</c> or <see cref = "string.Empty" />.
        /// </exception>
        [AssertionMethod]
        [DebuggerNonUserCode]
        public static void ArgumentNotNullOrEmpty(
            [AssertionCondition(AssertionConditionType.IsNotNull)] string value,
            [InvokerParameterName] string paramName)
        {
            ArgumentNotNull(value, paramName);
            Debug.Assert(Resources.Exception_StringArgumentIsEmpty != null);
            Argument(!string.IsNullOrEmpty(value), paramName, Resources.Exception_StringArgumentIsEmpty);
        }

        /// <summary>
        ///   Guarantees that a string argument is not <c>null</c> nor empty.
        /// </summary>
        /// <param name = "value">The string argument.</param>
        /// <param name = "paramName">The name of the parameter.</param>
        /// <param name = "format">Format string for the error message.</param>
        /// <param name = "args">Objects to be formatted.</param>
        /// <exception cref = "ArgumentException">
        ///   The <paramref name = "value" /> is <c>null</c> or <see cref = "string.Empty" />.
        /// </exception>
        [AssertionMethod, StringFormatMethod("format")]
        [DebuggerNonUserCode]
        public static void ArgumentNotNullOrEmpty(
            [AssertionCondition(AssertionConditionType.IsNotNull)] string value,
            [InvokerParameterName] string paramName,
            [NotNull, LocalizationRequired(true)] string format, [NotNull] params object[] args)
        {
            ArgumentNotNull(value, paramName, format, args);
            Argument(!string.IsNullOrEmpty(value), paramName, format, args);
        }

        /// <summary>
        ///   Guarantees that a collection argument is not <c>null</c> nor empty.
        /// </summary>
        /// <param name = "collection">The collection argument.</param>
        /// <param name = "paramName">The name of the parameter.</param>
        /// <exception cref = "ArgumentException">
        ///   The <paramref name = "collection" /> is <c>null</c> or empty.
        /// </exception>
        [AssertionMethod]
        [DebuggerNonUserCode]
        public static void ArgumentNotNullOrEmpty(
            [AssertionCondition(AssertionConditionType.IsNotNull)] ICollection collection,
            [InvokerParameterName] string paramName)
        {
            ArgumentNotNull(collection, paramName);
            Debug.Assert(Resources.Exception_CollectionArgumentIsEmpty != null);
            Argument(collection.Count > 0, paramName, Resources.Exception_CollectionArgumentIsEmpty);
        }

        /// <summary>
        ///   Guarantees that a collection argument is not <c>null</c> nor empty.
        /// </summary>
        /// <param name = "collection">The collection argument.</param>
        /// <param name = "paramName">The name of the parameter.</param>
        /// <param name = "format">Format string for the error message.</param>
        /// <param name = "args">Objects to be formatted.</param>
        /// <exception cref = "ArgumentException">
        ///   The <paramref name = "collection" /> is <c>null</c> or empty.
        /// </exception>
        [AssertionMethod, StringFormatMethod("format")]
        [DebuggerNonUserCode]
        public static void ArgumentNotNullOrEmpty(
            [AssertionCondition(AssertionConditionType.IsNotNull)] ICollection collection,
            [InvokerParameterName] string paramName,
            [NotNull, LocalizationRequired(true)] string format, [NotNull] params object[] args)
        {
            ArgumentNotNull(collection, paramName, format, args);
            Argument(collection.Count > 0, paramName, format, args);
        }

        /// <summary>
        ///   Guarantees that an argument is within a given range.
        /// </summary>
        /// <param name = "condition">The condition that checks the range.</param>
        /// <param name = "paramName">The name of the parameter.</param>
        /// <exception cref = "ArgumentException">
        ///   The <paramref name = "condition" /> is <c>false</c>.
        /// </exception>
        [AssertionMethod]
        [DebuggerNonUserCode]
        public static void ArgumentInRange(
            [AssertionCondition(AssertionConditionType.IsTrue)] bool condition,
            [InvokerParameterName] string paramName)
        {
            if (!condition)
            {
                throw new ArgumentOutOfRangeException(paramName);
            }
        }

        /// <summary>
        ///   Guarantees that an argument is within a given range.
        /// </summary>
        /// <param name = "condition">The condition that checks the range.</param>
        /// <param name = "paramName">The name of the parameter.</param>
        /// <param name = "format">Format string for the error message.</param>
        /// <param name = "args">Objects to be formatted.</param>
        /// <exception cref = "ArgumentException">
        ///   The <paramref name = "condition" /> is <c>false</c>.
        /// </exception>
        [AssertionMethod, StringFormatMethod("format")]
        [DebuggerNonUserCode]
        public static void ArgumentInRange(
            [AssertionCondition(AssertionConditionType.IsTrue)] bool condition,
            [InvokerParameterName] string paramName,
            [NotNull, LocalizationRequired(true)] string format, [NotNull] params object[] args)
        {
            if (!condition)
            {
                throw new ArgumentOutOfRangeException(paramName, Format(format, args));
            }
        }

        /// <summary>
        ///   Guarantees that the object is in the expected state.
        /// </summary>
        /// <param name = "condition">The condition to check.</param>
        /// <exception cref = "InvalidOperationException">
        ///   The <paramref name = "condition" /> is <c>false</c>.
        /// </exception>
        [AssertionMethod]
        [DebuggerNonUserCode]
        public static void State([AssertionCondition(AssertionConditionType.IsTrue)] bool condition)
        {
            if (!condition)
            {
                throw new InvalidOperationException();
            }
        }

        /// <summary>
        ///   Guarantees that the object is in the expected state.
        /// </summary>
        /// <param name = "condition">The condition to check.</param>
        /// <param name = "format">Format string for the error message.</param>
        /// <param name = "args">Objects to be formatted.</param>
        /// <exception cref = "InvalidOperationException">
        ///   The <paramref name = "condition" /> is <c>false</c>.
        /// </exception>
        [AssertionMethod, StringFormatMethod("format")]
        [DebuggerNonUserCode]
        public static void State(
            [AssertionCondition(AssertionConditionType.IsTrue)] bool condition,
            [NotNull, LocalizationRequired(true)] string format, [NotNull] params object[] args)
        {
            if (!condition)
            {
                throw new InvalidOperationException(Format(format, args));
            }
        }

        /// <summary>
        ///   Guarantees that the object is in the expected state,
        ///   throwing a <typeparamref name = "TException" /> otherwise.
        /// </summary>
        /// <typeparam name = "TException">The type of the exception.</typeparam>
        /// <param name = "condition">The condition to check.</param>
        [AssertionMethod]
        [DebuggerNonUserCode]
        public static void State<TException>(
            [AssertionCondition(AssertionConditionType.IsTrue)] bool condition)
            where TException : Exception, new()
        {
            if (!condition)
            {
                throw Create<TException>();
            }
        }

        /// <summary>
        ///   Guarantees that the object is in the expected state,
        ///   throwing a <typeparamref name = "TException" /> otherwise.
        /// </summary>
        /// <typeparam name = "TException">The type of the exception.</typeparam>
        /// <param name = "condition">The condition to check.</param>
        /// <param name = "format">Format string for the error message.</param>
        /// <param name = "args">Objects to be formatted.</param>
        [AssertionMethod, StringFormatMethod("format")]
        [DebuggerNonUserCode]
        public static void State<TException>(
            [AssertionCondition(AssertionConditionType.IsTrue)] bool condition,
            [NotNull, LocalizationRequired(true)] string format, [NotNull] params object[] args)
            where TException : Exception, new()
        {
            if (!condition)
            {
                throw Create<TException>(Format(format, args));
            }
        }

        /// <summary>
        ///   Demands permission to read a file.
        /// </summary>
        /// <param name = "path">The path of the file.</param>
        /// <exception cref = "SecurityException" />
        [AssertionMethod]
        [DebuggerNonUserCode]
        public static void FileIsReadable(string path)
        {
            new FileIOPermission(FileIOPermissionAccess.Read, path).Demand();
        }

        /// <summary>
        ///   Demands permission to read a file.
        /// </summary>
        /// <param name = "path">The path of the file.</param>
        /// <exception cref = "SecurityException" />
        [AssertionMethod]
        [DebuggerNonUserCode]
        public static void FileIsWritable(string path)
        {
            new FileIOPermission(FileIOPermissionAccess.Write, path).Demand();
        }

        /// <summary>
        ///   Demands permission to read a file.
        /// </summary>
        /// <param name = "path">The path of the file.</param>
        /// <exception cref = "SecurityException" />
        [AssertionMethod]
        [DebuggerNonUserCode]
        public static void FileIsAppendable(string path)
        {
            new FileIOPermission(FileIOPermissionAccess.Append, path).Demand();
        }

        /// <summary>
        ///   Guarantees that an enum argument is defined.
        /// </summary>
        /// <typeparam name = "T">The enum type.</typeparam>
        /// <param name = "value">The enum value.</param>
        /// <param name = "paramName">The name of the parameter.</param>
        /// <exception cref = "ArgumentOutOfRangeException">
        ///   The value is not a defined enum value.
        /// </exception>
        [AssertionMethod]
        [DebuggerNonUserCode]
        public static void EnumArgument<T>([NotNull] T value,
                                           [InvokerParameterName] string paramName)
            // where T : Enum
        {
            if (!Enum.IsDefined(typeof(T), value))
            {
                throw new InvalidEnumArgumentException(paramName, (int)(object)value, typeof(T));
            }
        }
    }
}
