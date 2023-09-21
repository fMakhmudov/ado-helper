using System;
using System.Globalization;

namespace AdoNetMaster.Infrastructure.AdoHelper.Core
{
    public class DBConvert
    {
        private static bool ToBooleanInternal(object value, IFormatProvider provider)
        {
            string text = value as string;
            if (text != null && bool.TryParse(text, out var result))
            {
                return result;
            }

            if ((text = value.ToString()) != null && int.TryParse(text, out var result2))
            {
                return result2 != 0;
            }

            return Convert.ToBoolean(value, provider);
        }

        public static bool? ToNullableBoolean(object value, IFormatProvider provider)
        {
            return ToNullable(value, provider, ToBooleanInternal);
        }

        public static bool? ToNullableBoolean(object value)
        {
            return ToNullable(value, CultureInfo.CurrentCulture, ToBooleanInternal);
        }

        public static bool ToBoolean(object value, IFormatProvider provider)
        {
            return To(value, provider, ToBooleanInternal);
        }

        public static bool ToBoolean(object value)
        {
            return To(value, CultureInfo.CurrentCulture, ToBooleanInternal);
        }

        private static sbyte ToSByteInternal(object value, IFormatProvider provider)
        {
            return Convert.ToSByte(value, provider);
        }

        public static sbyte? ToNullableSByte(object value, IFormatProvider provider)
        {
            return ToNullable(value, provider, ToSByteInternal);
        }

        public static sbyte? ToNullableSByte(object value)
        {
            return ToNullable(value, CultureInfo.CurrentCulture, ToSByteInternal);
        }

        public static sbyte ToSByte(object value, IFormatProvider provider)
        {
            return To(value, provider, ToSByteInternal);
        }

        public static sbyte ToSByte(object value)
        {
            return To(value, CultureInfo.CurrentCulture, ToSByteInternal);
        }

        private static short ToInt16Internal(object value, IFormatProvider provider)
        {
            return Convert.ToInt16(value, provider);
        }

        public static short? ToNullableInt16(object value, IFormatProvider provider)
        {
            return ToNullable(value, provider, ToInt16Internal);
        }

        public static short? ToNullableInt16(object value)
        {
            return ToNullable(value, CultureInfo.CurrentCulture, ToInt16Internal);
        }

        public static short ToInt16(object value, IFormatProvider provider)
        {
            return To(value, provider, ToInt16Internal);
        }

        public static short ToInt16(object value)
        {
            return To(value, CultureInfo.CurrentCulture, ToInt16Internal);
        }

        private static int ToInt32Internal(object value, IFormatProvider provider)
        {
            return Convert.ToInt32(value, provider);
        }

        public static int? ToNullableInt32(object value, IFormatProvider provider)
        {
            return ToNullable(value, provider, ToInt32Internal);
        }

        public static int? ToNullableInt32(object value)
        {
            return ToNullable(value, CultureInfo.CurrentCulture, ToInt32Internal);
        }

        public static int ToInt32(object value, IFormatProvider provider)
        {
            return To(value, provider, ToInt32Internal);
        }

        public static int ToInt32(object value)
        {
            return To(value, CultureInfo.CurrentCulture, ToInt32Internal);
        }

        private static long ToInt64Internal(object value, IFormatProvider provider)
        {
            return Convert.ToInt64(value, provider);
        }

        public static long? ToNullableInt64(object value, IFormatProvider provider)
        {
            return ToNullable(value, provider, ToInt64Internal);
        }

        public static long? ToNullableInt64(object value)
        {
            return ToNullable(value, CultureInfo.CurrentCulture, ToInt64Internal);
        }

        public static long ToInt64(object value, IFormatProvider provider)
        {
            return To(value, provider, ToInt64Internal);
        }

        public static long ToInt64(object value)
        {
            return To(value, CultureInfo.CurrentCulture, ToInt64Internal);
        }

        private static byte ToByteInternal(object value, IFormatProvider provider)
        {
            return Convert.ToByte(value, provider);
        }

        public static byte? ToNullableByte(object value, IFormatProvider provider)
        {
            return ToNullable(value, provider, ToByteInternal);
        }

        public static byte? ToNullableByte(object value)
        {
            return ToNullable(value, CultureInfo.CurrentCulture, ToByteInternal);
        }

        public static byte ToByte(object value, IFormatProvider provider)
        {
            return To(value, provider, ToByteInternal);
        }

        public static byte ToByte(object value)
        {
            return To(value, CultureInfo.CurrentCulture, ToByteInternal);
        }

        private static ushort ToUInt16Internal(object value, IFormatProvider provider)
        {
            return Convert.ToUInt16(value, provider);
        }

        public static ushort? ToNullableUInt16(object value, IFormatProvider provider)
        {
            return ToNullable(value, provider, ToUInt16Internal);
        }

        public static ushort? ToNullableUInt16(object value)
        {
            return ToNullable(value, CultureInfo.CurrentCulture, ToUInt16Internal);
        }

        public static ushort ToUInt16(object value, IFormatProvider provider)
        {
            return To(value, provider, ToUInt16Internal);
        }

        public static ushort ToUInt16(object value)
        {
            return To(value, CultureInfo.CurrentCulture, ToUInt16Internal);
        }

        private static uint ToUInt32Internal(object value, IFormatProvider provider)
        {
            return Convert.ToUInt32(value, provider);
        }

        public static uint? ToNullableUInt32(object value, IFormatProvider provider)
        {
            return ToNullable(value, provider, ToUInt32Internal);
        }

        public static uint? ToNullableUInt32(object value)
        {
            return ToNullable(value, CultureInfo.CurrentCulture, ToUInt32Internal);
        }

        public static uint ToUInt32(object value, IFormatProvider provider)
        {
            return To(value, provider, ToUInt32Internal);
        }

        public static uint ToUInt32(object value)
        {
            return To(value, CultureInfo.CurrentCulture, ToUInt32Internal);
        }

        private static ulong ToUInt64Internal(object value, IFormatProvider provider)
        {
            return Convert.ToUInt64(value, provider);
        }

        public static ulong? ToNullableUInt64(object value, IFormatProvider provider)
        {
            return ToNullable(value, provider, ToUInt64Internal);
        }

        public static ulong? ToNullableUInt64(object value)
        {
            return ToNullable(value, CultureInfo.CurrentCulture, ToUInt64Internal);
        }

        public static ulong ToUInt64(object value, IFormatProvider provider)
        {
            return To(value, provider, ToUInt64Internal);
        }

        public static ulong ToUInt64(object value)
        {
            return To(value, CultureInfo.CurrentCulture, ToUInt64Internal);
        }

        private static decimal ToDecimalInternal(object value, IFormatProvider provider)
        {
            string decimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            value = ("" + value).Replace(".", decimalSeparator).Replace(",", decimalSeparator);

            return Convert.ToDecimal(value, provider);
        }

        public static decimal? ToNullableDecimal(object value, IFormatProvider provider)
        {
            return ToNullable(value, provider, ToDecimalInternal);
        }

        public static decimal? ToNullableDecimal(object value)
        {
            return ToNullable(value, CultureInfo.CurrentCulture, ToDecimalInternal);
        }

        public static decimal ToDecimal(object value, IFormatProvider provider)
        {
            return To(value, provider, ToDecimalInternal);
        }

        public static decimal ToDecimal(object value)
        {
            return To(value, CultureInfo.CurrentCulture, ToDecimalInternal);
        }

        private static float ToSingleInternal(object value, IFormatProvider provider)
        {
            return Convert.ToSingle(value, provider);
        }

        public static float? ToNullableSingle(object value, IFormatProvider provider)
        {
            return ToNullable(value, provider, ToSingleInternal);
        }

        public static float? ToNullableSingle(object value)
        {
            return ToNullable(value, CultureInfo.CurrentCulture, ToSingleInternal);
        }

        public static float ToSingle(object value, IFormatProvider provider)
        {
            return To(value, provider, ToSingleInternal);
        }

        public static float ToSingle(object value)
        {
            return To(value, CultureInfo.CurrentCulture, ToSingleInternal);
        }

        private static double ToDoubleInternal(object value, IFormatProvider provider)
        {
            return Convert.ToDouble(value, provider);
        }

        public static double? ToNullableDouble(object value, IFormatProvider provider)
        {
            return ToNullable(value, provider, ToDoubleInternal);
        }

        public static double? ToNullableDouble(object value)
        {
            return ToNullable(value, CultureInfo.CurrentCulture, ToDoubleInternal);
        }

        public static double ToDouble(object value, IFormatProvider provider)
        {
            return To(value, provider, ToDoubleInternal);
        }

        public static double ToDouble(object value)
        {
            return To(value, CultureInfo.CurrentCulture, ToDoubleInternal);
        }

        private static char ToCharInternal(object value, IFormatProvider provider)
        {
            string text = value as string;
            if (!string.IsNullOrEmpty(text))
            {
                return text[0];
            }

            return Convert.ToChar(value, provider);
        }

        public static char? ToNullableChar(object value, IFormatProvider provider)
        {
            return ToNullable(value, provider, ToCharInternal);
        }

        public static char? ToNullableChar(object value)
        {
            return ToNullable(value, CultureInfo.CurrentCulture, ToCharInternal);
        }

        public static char ToChar(object value, IFormatProvider provider)
        {
            return To(value, provider, ToCharInternal);
        }

        public static char ToChar(object value)
        {
            return To(value, CultureInfo.CurrentCulture, ToCharInternal);
        }

        private static DateTime ToDateTimeInternal(object value, IFormatProvider provider)
        {
            return Convert.ToDateTime(value, provider);
        }

        public static DateTime? ToNullableDateTime(object value, IFormatProvider provider)
        {
            return ToNullable(value, provider, ToDateTimeInternal);
        }

        public static DateTime? ToNullableDateTime(object value)
        {
            return ToNullable(value, CultureInfo.CurrentCulture, ToDateTimeInternal);
        }

        public static DateTime ToDateTime(object value, IFormatProvider provider)
        {
            return To(value, provider, ToDateTimeInternal);
        }

        public static DateTime ToDateTime(object value)
        {
            return To(value, CultureInfo.CurrentCulture, ToDateTimeInternal);
        }

        private static DateTimeOffset ToDateTimeOffsetInternal(object value, IFormatProvider provider, DateTimeStyles styles)
        {
            if (value is DateTime)
            {
                return (DateTimeOffset)value;
            }

            return DateTimeOffset.Parse(value.ToString(), provider, styles);
        }

        private static DateTimeOffset ToDateTimeOffsetInternal(object value, IFormatProvider provider)
        {
            return ToDateTimeOffsetInternal(value, provider, DateTimeStyles.None);
        }

        public static DateTimeOffset? ToNullableDateTimeOffset(object value, IFormatProvider provider)
        {
            return ToNullable(value, provider, ToDateTimeOffsetInternal);
        }

        public static DateTimeOffset? ToNullableDateTimeOffset(object value)
        {
            return ToNullable(value, CultureInfo.CurrentCulture, ToDateTimeOffsetInternal);
        }

        public static DateTimeOffset ToDateTimeOffset(object value, IFormatProvider provider)
        {
            return To(value, provider, ToDateTimeOffsetInternal);
        }

        public static DateTimeOffset ToDateTimeOffset(object value)
        {
            return To(value, CultureInfo.CurrentCulture, ToDateTimeOffsetInternal);
        }

        private static string ToStringInternal(object value, IFormatProvider provider)
        {
            return Convert.ToString(value, provider);
        }

        public static string ToString(object value, IFormatProvider provider)
        {
            return To(value, provider, ToStringInternal);
        }

        public static string ToString(object value)
        {
            return To(value, CultureInfo.CurrentCulture, ToStringInternal);
        }

        private static Guid ToGuidInternal(object value, IFormatProvider provider)
        {
            byte[] array = value as byte[];
            if (array != null)
            {
                return new Guid(array);
            }

            return new Guid(value.ToString());
        }

        public static Guid? ToNullableGuid(object value)
        {
            return ToNullable(value, null, ToGuidInternal);
        }

        public static Guid ToGuid(object value)
        {
            return To(value, null, ToGuidInternal);
        }

        public static byte[] ToByteArray(object value)
        {
            if (value is byte[])
            {
                return (byte[])value;
            }

            if (value == null || value == DBNull.Value)
            {
                return null;
            }

            throw new FormatException("Cannot cast value to byte[].");
        }

        public static T To<T>(object value)
        {
            return To<T>(value, CultureInfo.CurrentCulture);
        }

        public static T To<T>(object value, IFormatProvider provider)
        {
            if (value is T)
            {
                return (T)value;
            }

            if (value == null || value == DBNull.Value)
            {
                return default(T);
            }

            return (T)To(typeof(T), value, provider);
        }

        public static object To(Type type, object value)
        {
            return To(type, value, CultureInfo.CurrentCulture);
        }

        public static object To(Type type, object value, IFormatProvider provider)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            if (value != null && value.GetType() == type)
            {
                return value;
            }

            bool flag = IsNullable(type);
            if (flag)
            {
                type = Nullable.GetUnderlyingType(type);
            }

            if (value == null || value == DBNull.Value)
            {
                if (flag || !type.IsValueType)
                {
                    return null;
                }

                return Activator.CreateInstance(type);
            }

            TypeCode typeCode = Type.GetTypeCode(type);
            switch (typeCode)
            {
                case TypeCode.Boolean:
                    return ToBooleanInternal(value, provider);
                case TypeCode.SByte:
                    return ToSByteInternal(value, provider);
                case TypeCode.Int16:
                    return ToInt16Internal(value, provider);
                case TypeCode.Int32:
                    return ToInt32Internal(value, provider);
                case TypeCode.Int64:
                    return ToInt64Internal(value, provider);
                case TypeCode.Byte:
                    return ToByteInternal(value, provider);
                case TypeCode.UInt16:
                    return ToUInt16Internal(value, provider);
                case TypeCode.UInt32:
                    return ToUInt32Internal(value, provider);
                case TypeCode.UInt64:
                    return ToUInt64Internal(value, provider);
                case TypeCode.Decimal:
                    return ToDecimalInternal(value, provider);
                case TypeCode.Single:
                    return ToSingleInternal(value, provider);
                case TypeCode.Double:
                    return ToDoubleInternal(value, provider);
                case TypeCode.Char:
                    return ToCharInternal(value, provider);
                case TypeCode.DateTime:
                    return ToDateTimeInternal(value, provider);
                case TypeCode.String:
                    return ToStringInternal(value, provider);
                case TypeCode.Object:
                    if (type == typeof(Guid))
                    {
                        return ToGuidInternal(value, null);
                    }

                    if (type == typeof(byte[]))
                    {
                        return ToByteArray(value);
                    }

                    if (type == typeof(DateTimeOffset))
                    {
                        return ToDateTimeOffsetInternal(value, null);
                    }

                    try { return Convert.ChangeType(value, type, provider); }
                    catch { return System.Text.Json.JsonSerializer.Deserialize("" + value, type); }

                    //break;
            }

            return Convert.ChangeType(value, typeCode, provider);
        }

        public static object ToDBValue(object value)
        {
            if (value == null)
            {
                return DBNull.Value;
            }

            return value;
        }

        private static T? ToNullable<T>(object value, IFormatProvider provider, Func<object, IFormatProvider, T> converter) where T : struct
        {
            if (value is T)
            {
                return (T)value;
            }

            if (value == null || value == DBNull.Value)
            {
                return null;
            }

            return converter(value, provider);
        }

        private static T To<T>(object value, IFormatProvider provider, Func<object, IFormatProvider, T> converter)
        {
            if (value is T)
            {
                return (T)value;
            }

            if (value == null || value == DBNull.Value)
            {
                return default(T);
            }

            return converter(value, provider);
        }

        private static bool IsNullable(Type type)
        {
            if (type.IsGenericType)
            {
                return type.GetGenericTypeDefinition() == typeof(Nullable<>);
            }

            return false;
        }
    }
}
