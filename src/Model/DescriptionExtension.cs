using System;
using System.ComponentModel;
using System.Reflection;

namespace MonsterCounty.Model
{
    public static class DescriptionExtension
    {
        public static string GetStringValue(this Enum value)
        {
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
            if (fieldInfo == null) return null;
            var attribute = (DescriptionAttribute)fieldInfo.GetCustomAttribute(typeof(DescriptionAttribute));
            return attribute?.Description;
        }
    }
}