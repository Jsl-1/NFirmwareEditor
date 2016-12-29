using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;

namespace System.Windows.Forms
{
    /// <summary>Provides a type converter to convert <see cref="T:System.Windows.Forms.Padding" /> values to and from various other representations.</summary>
    public class PaddingConverter : TypeConverter
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.PaddingConverter" /> class. </summary>
        public PaddingConverter()
        {
        }

        /// <summary>Returns whether this converter can convert an object of one type to the type of this converter.</summary>
        /// <returns>true if this object can perform the conversion; otherwise, false.</returns>
        /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
        /// <param name="sourceType">A <see cref="T:System.Type" /> that represents the type you wish to convert from.</param>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }
            return base.CanConvertFrom(context, sourceType);
        }

        /// <summary>Returns whether this converter can convert the object to the specified type, using the specified context.</summary>
        /// <returns>true if this converter can perform the conversion; otherwise, false.</returns>
        /// <param name="destinationType">A T:System.Type that represents the type you want to convert to. </param>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(InstanceDescriptor))
            {
                return true;
            }
            return base.CanConvertTo(context, destinationType);
        }

        /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
        /// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> to use as the current culture. </param>
        /// <param name="value">The <see cref="T:System.Object" /> to convert. </param>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            string str = value as string;
            if (str == null)
            {
                return base.ConvertFrom(context, culture, value);
            }
            str = str.Trim();
            if (str.Length == 0)
            {
                return null;
            }
            if (culture == null)
            {
                culture = CultureInfo.CurrentCulture;
            }
            char listSeparator = culture.TextInfo.ListSeparator[0];
            string[] strArrays = str.Split(new char[] { listSeparator });
            int[] numArray = new int[(int)strArrays.Length];
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(int));
            for (int i = 0; i < (int)numArray.Length; i++)
            {
                numArray[i] = (int)converter.ConvertFromString(context, culture, strArrays[i]);
            }
            if ((int)numArray.Length != 4)
            {
                throw new ArgumentException("TextParseFailedFormat");
            }
            return new Padding(numArray[0], numArray[1], numArray[2], numArray[3]);
        }

        /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
        /// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" />. If null is passed, the current culture is assumed. </param>
        /// <param name="value">The <see cref="T:System.Object" /> to convert. </param>
        /// <param name="destinationType">The <see cref="T:System.Type" /> to convert the <paramref name="value" /> parameter to. </param>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == null)
            {
                throw new ArgumentNullException("destinationType");
            }
            if (value is Padding)
            {
                if (destinationType == typeof(string))
                {
                    Padding padding = (Padding)value;
                    if (culture == null)
                    {
                        culture = CultureInfo.CurrentCulture;
                    }
                    string str = string.Concat(culture.TextInfo.ListSeparator, " ");
                    TypeConverter converter = TypeDescriptor.GetConverter(typeof(int));
                    string[] strArrays = new string[4];
                    int num = 0;
                    int num1 = num + 1;
                    strArrays[num] = converter.ConvertToString(context, culture, padding.Left);
                    int num2 = num1;
                    num1 = num2 + 1;
                    strArrays[num2] = converter.ConvertToString(context, culture, padding.Top);
                    int num3 = num1;
                    num1 = num3 + 1;
                    strArrays[num3] = converter.ConvertToString(context, culture, padding.Right);
                    int num4 = num1;
                    num1 = num4 + 1;
                    strArrays[num4] = converter.ConvertToString(context, culture, padding.Bottom);
                    return string.Join(str, strArrays);
                }
                if (destinationType == typeof(InstanceDescriptor))
                {
                    Padding padding1 = (Padding)value;
                    if (padding1.ShouldSerializeAll())
                    {
                        return new InstanceDescriptor(typeof(Padding).GetConstructor(new Type[] { typeof(int) }), new object[] { padding1.All });
                    }
                    return new InstanceDescriptor(typeof(Padding).GetConstructor(new Type[] { typeof(int), typeof(int), typeof(int), typeof(int) }), new object[] { padding1.Left, padding1.Top, padding1.Right, padding1.Bottom });
                }
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

        /// <summary>Creates an instance of the type that this <see cref="T:System.ComponentModel.TypeConverter" /> is associated with, using the specified context, given a set of property values for the object.</summary>
        /// <returns>An <see cref="T:System.Object" /> representing the given <see cref="T:System.Collections.IDictionary" />, or null if the object cannot be created. This method always returns null.</returns>
        /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
        /// <param name="propertyValues">An <see cref="T:System.Collections.IDictionary" /> of new property values. </param>
        public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            if (propertyValues == null)
            {
                throw new ArgumentNullException("propertyValues");
            }
            Padding value = (Padding)context.PropertyDescriptor.GetValue(context.Instance);
            int item = (int)propertyValues["All"];
            if (value.All != item)
            {
                return new Padding(item);
            }
            return new Padding((int)propertyValues["Left"], (int)propertyValues["Top"], (int)propertyValues["Right"], (int)propertyValues["Bottom"]);
        }

        /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
        public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
        /// <param name="value">An <see cref="T:System.Object" /> that specifies the type of array for which to get properties. </param>
        /// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that is used as a filter. </param>
        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(Padding), attributes);
            return properties.Sort(new string[] { "All", "Left", "Top", "Right", "Bottom" });
        }

        /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
    }
}