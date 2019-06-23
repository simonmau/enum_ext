using AutoFixture;
using System;
using System.Collections.Generic;
using System.Text;

namespace Enum.Ext.AutoFixture
{
    public static class Extensions
    {
        /// <summary>
        /// Configures AutoFixture to work with properties derived from <see cref="TypeSafeEnum{TValue, TKey}"/>.
        /// </summary>
        /// <param name="fixture"></param>
        public static void WithEnumExt(this IFixture fixture)
        {
            fixture.Customize(new TypeSafeEnumCustomization());
        }
    }
}