namespace GeometryConverter.Helpers
{
    using System;

    public static class EnumHelper
    {
        public const string Direction = "GeometryConverter.Bases.Direction";

        public static int GetEnumElementsNumber(string typeName)
        {
            var enumType = Type.GetType(typeName);
            return Enum.GetValues(enumType).Length;
        }
    }
}