using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace XamarinBookStoreApp
{
    public static class Permissions
    {
        public const string GroupName = "BookStore";

        public class Books
        {
            public const string Default = GroupName + ".Books";
            public const string Create = Default + ".Create";
            public const string Edit = Default + ".Edit";
            public const string Delete = Default + ".Delete";
        }

        public static List<string> GetPossiblePermissions()
        {
            var constantValues = new List<string>();
            var permissionsNestedTypes = typeof(Permissions).GetNestedTypes().ToList();
            foreach (var nestedType in permissionsNestedTypes)
                constantValues = GetConstantValues<string>(nestedType);
            return constantValues;
        }

        private static List<T> GetConstantValues<T>(Type type)
        {
            FieldInfo[] fields = type.GetFields(BindingFlags.Public
                | BindingFlags.Static
                | BindingFlags.FlattenHierarchy);

            return (fields.Where(fieldInfo => fieldInfo.IsLiteral
                && !fieldInfo.IsInitOnly
                && fieldInfo.FieldType == typeof(T)).Select(fi => (T)fi.GetRawConstantValue())).ToList();
        }
    }
}
