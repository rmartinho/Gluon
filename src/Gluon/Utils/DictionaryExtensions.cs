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
using System.Diagnostics;

using Wheels;
using Wheels.Annotations;

namespace Gluon.Utils
{
    internal static class DictionaryExtensions
    {
        public static ChangeKind Modify<TKey, TValue>(
            [NotNull] this IDictionary<TKey, TValue> dictionary,
            [NotNull] TKey key,
            [CanBeNull] TValue value)
            where TValue : class
        {
            Ensure.ArgumentNotNull(dictionary, "dictionary");
            Ensure.ArgumentNotNull(key, "key");

            if (value == null)
            {
                if (dictionary.Remove(key))
                {
                    return ChangeKind.Removed;
                }
            }
            else
            {
                var existingValue = dictionary.TryGetValue(key);
                if (!existingValue.HasValue || existingValue.Value != value)
                {
                    dictionary[key] = value;
                    return ChangeKind.Added;
                }
            }
            return ChangeKind.None;
        }

        [NotNull]
        public static TValue GetValueOrDefault<TKey, TValue>(
            [NotNull] this IDictionary<TKey, TValue> dictionary,
            [NotNull] TKey key,
            [NotNull] TValue defaultValue)
            where TValue : class
        {
            Ensure.ArgumentNotNull(dictionary, "dictionary");
            Ensure.ArgumentNotNull(key, "key");
            Ensure.ArgumentNotNull(defaultValue, "defaultValue");

            var value = dictionary.TryGetValue(key) | defaultValue;
            Debug.Assert(value != null, "value != null");
            return value;
        }
    }

    internal enum ChangeKind
    {
        None,
        Added,
        Removed,
    }
}
