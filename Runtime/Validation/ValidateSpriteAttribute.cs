using System;
using System.Diagnostics.CodeAnalysis;

namespace DGP.UnityExtensions.Validation
{
    /// <summary>
    /// Validates that a sprite has a specific aspect ratio and minimum dimensions.
    /// </summary>
    [ExcludeFromCodeCoverage]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class ValidateSpriteAttribute : Attribute
    {
        public float AspectRatio { get; }
        public int MinWidth { get; }
        public int MinHeight { get; }
        public float AspectRatioTolerance { get; }

        /// <summary>
        /// Validates sprite dimensions and aspect ratio.
        /// </summary>
        /// <param name="aspectRatio">Expected aspect ratio (width / height). Use 1.0f for square. Use -1 to skip aspect ratio validation.</param>
        /// <param name="minWidth">Minimum width in pixels. Use -1 to skip width validation.</param>
        /// <param name="minHeight">Minimum height in pixels. Use -1 to skip height validation.</param>
        /// <param name="aspectRatioTolerance">Tolerance for aspect ratio comparison (default 0.01f).</param>
        public ValidateSpriteAttribute(float aspectRatio = -1f, int minWidth = -1, int minHeight = -1, float aspectRatioTolerance = 0.01f)
        {
            AspectRatio = aspectRatio;
            MinWidth = minWidth;
            MinHeight = minHeight;
            AspectRatioTolerance = aspectRatioTolerance;
        }
    }
}