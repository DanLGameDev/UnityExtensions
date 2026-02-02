using System;

namespace DGP.UnityExtensions.Validation
{
    /// <summary>
    /// Interface for ScriptableObjects that map to enum values
    /// </summary>
    public interface IEnumMappedAsset<T> where T : Enum
    {
        /// <summary>
        /// The enum value that this asset maps to
        /// </summary>
        T EnumValue { get; }
    }
}