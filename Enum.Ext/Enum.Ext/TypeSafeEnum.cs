using System.Collections.Generic;
using System.Linq;
using System;
using Newtonsoft.Json;
using Enum.Ext.Converter;
using System.Reflection;

namespace Enum.Ext
{
    [JsonConverter(typeof(JsonTypeSafeEnumConverter))]
    public abstract class TypeSafeEnum<TValue, TKey> : IEquatable<TypeSafeEnum<TValue, TKey>>, IComparable<TypeSafeEnum<TValue, TKey>>
        where TKey : struct, IEquatable<TKey>, IComparable<TKey>
        where TValue : TypeSafeEnum<TValue, TKey>
    {
        protected static readonly Lazy<Dictionary<TKey, TValue>> Dictionary = new Lazy<Dictionary<TKey, TValue>>(() => GetAllOptions().ToDictionary(e => e.Id));

        private static IReadOnlyCollection<TValue> _list;

        protected TypeSafeEnum(TKey id)
        {
            Id = id;
        }

        public TKey Id { get; private set; }

        /// <summary>
        /// Holds all declared values of the specific enum.
        /// </summary>
        public static IReadOnlyCollection<TValue> List => _list ?? (_list = Dictionary.Value.Values.Cast<TValue>().ToList());

        public static TypeSafeEnum<TValue, TKey> GetById(TKey value)
        {
            return Dictionary.Value[value];
        }

        #region overrides

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj is TypeSafeEnum<TValue, TKey> other && Equals(other);
        }

        public bool Equals(TypeSafeEnum<TValue, TKey> other)
        {
            if (object.ReferenceEquals(this, other))
                return true;

            if (other is null)
                return false;

            return Id.Equals(other.Id);
        }

        public int CompareTo(TypeSafeEnum<TValue, TKey> other)
        {
            return Id.CompareTo(other.Id);
        }

        public static bool operator ==(TypeSafeEnum<TValue, TKey> left, TypeSafeEnum<TValue, TKey> right)
        {
            if (left is null)
                return right is null;

            return left.Equals(right);
        }

        public static bool operator !=(TypeSafeEnum<TValue, TKey> left, TypeSafeEnum<TValue, TKey> right)
        {
            return !(left == right);
        }

        public static implicit operator TKey(TypeSafeEnum<TValue, TKey> value)
        {
            return value.Id;
        }

        public static implicit operator TypeSafeEnum<TValue, TKey>(TKey value)
        {
            return Dictionary.Value[value];
        }

        #endregion overrides

        #region static methods

        private static IEnumerable<TValue> GetAllOptions()
        {
            Type baseType = typeof(TValue);
            IEnumerable<Type> enumTypes = Assembly.GetAssembly(baseType).GetTypes().Where(t => baseType.IsAssignableFrom(t));

            List<TValue> options = new List<TValue>();
            foreach (Type enumType in enumTypes)
            {
                List<TValue> typeEnumOptions = enumType.GetFieldsOfType<TValue>();
                options.AddRange(typeEnumOptions);
            }

            return options.ToList();
        }

        #endregion static methods
    }
}