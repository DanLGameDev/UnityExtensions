namespace DGP.UnityExtensions.Validation
{
    /// <summary>
    /// Interface for ScriptableObjects that map to enum values via string keys
    /// </summary>
    public interface IEnumMappedAsset
    {
        /// <summary>
        /// The unique key that matches an enum value
        /// </summary>
        string EnumKey { get; }
    }
}