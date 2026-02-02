using System;
using System.Diagnostics.CodeAnalysis;

namespace DGP.UnityExtensions.Validation
{
    /// <summary>
    /// Marks an enum value with a key that maps to a ScriptableObject instance
    /// </summary>
    [ExcludeFromCodeCoverage]
    [AttributeUsage(AttributeTargets.Field)]
    public class EnumKeyAttribute : Attribute
    {
        public string Key { get; }

        public EnumKeyAttribute(string key)
        {
            Key = key;
        }
    }
}