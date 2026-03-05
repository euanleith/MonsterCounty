using System;

namespace MonsterCounty.Utilities
{
    public static class EnumUtilities
    {
        public static T GetRandomEnumValue<T>()
        {
            Random rand = new Random();
            Array positionOptions = Enum.GetValues(typeof(T));
            return (T)positionOptions.GetValue(rand.Next(positionOptions.Length));
        }
    }
}