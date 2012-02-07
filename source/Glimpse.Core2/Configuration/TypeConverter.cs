using System;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;

namespace Glimpse.Core2.Configuration
{
    public class TypeConverter : ConfigurationConverterBase
    {
        public override object ConvertFrom(ITypeDescriptorContext ctx, CultureInfo ci, object data)
        {
            var typeName = data as string;

            if (string.IsNullOrWhiteSpace(typeName))
                throw new ArgumentException(string.Format("Invalid Type name '{0}'.", typeName));

            return Type.GetType(typeName, true, true);
        }

        public override object ConvertTo(ITypeDescriptorContext ctx, CultureInfo ci, object value, Type type)
        {
            var t = value as Type;

            return t.FullName;
        }
    }
}