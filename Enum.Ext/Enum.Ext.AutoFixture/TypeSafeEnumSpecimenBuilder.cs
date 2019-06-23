using AutoFixture.Kernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Enum.Ext;

namespace Enum.Ext.AutoFixture
{
    public class TypeSafeEnumSpecimenBuilder : ISpecimenBuilder
    {
        private static readonly Random _random = new Random();

        public object Create(object request, ISpecimenContext context)
        {
            if (request is Type type)
            {
                var listPropertyInfo = GetBasePropertyInfo(type);

                if (listPropertyInfo == null)
                {
                    return new NoSpecimen();
                }

                var enums = (IEnumerable<object>)listPropertyInfo.GetValue(type, null);

                var numberOfEnums = enums.Count();

                if (numberOfEnums > 1)
                {
                    var times = _random.Next(1, numberOfEnums);

                    using (var enumerator = enums.GetEnumerator())
                    {
                        for (int i = 0; i < times; i++)
                        {
                            enumerator.MoveNext();
                        }

                        return enumerator.Current;
                    }
                }
            }

            return new NoSpecimen();
        }

        private static PropertyInfo GetBasePropertyInfo(Type objectType)
        {
            Type currentType = objectType.BaseType;

            if (currentType == null)
            {
                return null;
            }

            while (currentType != typeof(object))
            {
                if (currentType.IsGenericType && currentType.GetGenericTypeDefinition() == typeof(TypeSafeEnum<,>))
                    return currentType.GetProperty("List", BindingFlags.Public | BindingFlags.Static);

                currentType = currentType.BaseType;
            }

            return null;
        }
    }
}