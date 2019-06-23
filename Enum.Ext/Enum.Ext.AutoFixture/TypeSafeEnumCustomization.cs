using AutoFixture;
using System;

namespace Enum.Ext.AutoFixture
{
    public class TypeSafeEnumCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customizations.Add(new TypeSafeEnumSpecimenBuilder());
        }
    }
}