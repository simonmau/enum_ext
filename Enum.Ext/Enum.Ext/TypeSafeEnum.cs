using System.Collections.Generic;
using System.Linq;
using System;
using Newtonsoft.Json;
using Enum.Ext.Converter;

namespace Enum.Ext
{
    [JsonConverter(typeof(JsonTypeSafeEnumConverter))]
    public abstract class TypeSafeEnum<TValue, TKey> where TKey : struct
    {
        protected static readonly Dictionary<TKey, TypeSafeEnum<TValue, TKey>> Dictionary = new Dictionary<TKey, TypeSafeEnum<TValue, TKey>>();

        private static IEnumerable<TValue> _list;

        protected TypeSafeEnum(TKey id)
        {
            if (Dictionary.ContainsKey(id))
            {
                throw new ArgumentException("element with same id cannot be initialized multiple times", nameof(id));
            }

            Id = id;
            Dictionary[id] = this;
        }

        public TKey Id { get; private set; }

        /// <summary>
        /// Holds all declared values of the specific enum.
        /// </summary>
        public static IEnumerable<TValue> List => _list ?? (_list = Dictionary.Values.Cast<TValue>());

        public static TypeSafeEnum<TValue, TKey> GetById(TKey value)
        {
            return Dictionary[value];
        }

        public static implicit operator TKey(TypeSafeEnum<TValue, TKey> value)
        {
            return value.Id;
        }

        public static implicit operator TypeSafeEnum<TValue, TKey>(TKey value)
        {
            return Dictionary[value];
        }
    }
}