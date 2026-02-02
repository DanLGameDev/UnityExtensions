using System;
using System.Diagnostics.CodeAnalysis;

namespace DGP.UnityExtensions.Validation
{
    /// <summary>
    /// Marks an enum as having a 1:1 mapping with ScriptableObject instances
    /// </summary>
    [AttributeUsage(AttributeTargets.Enum)]
    [ExcludeFromCodeCoverage]
    public class EnumMappedAssetAttribute : Attribute
    {
        public Type ScriptableObjectType { get; }

        public EnumMappedAssetAttribute(Type scriptableObjectType)
        {
            if (!scriptableObjectType.IsSubclassOf(typeof(UnityEngine.ScriptableObject)))
                throw new ArgumentException($"{scriptableObjectType.Name} must derive from ScriptableObject");

            ScriptableObjectType = scriptableObjectType;
        }
    }
}