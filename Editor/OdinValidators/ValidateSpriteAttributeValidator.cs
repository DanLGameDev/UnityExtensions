#if ODIN_INSPECTOR
using DGP.UnityExtensions.Editor.OdinValidators;
using DGP.UnityExtensions.Validation;
using Sirenix.OdinInspector.Editor.Validation;
using UnityEditor;
using UnityEngine;

[assembly: RegisterValidator(typeof(ValidateSpriteAttributeValidator))]

namespace DGP.UnityExtensions.Editor.OdinValidators
{
    public class ValidateSpriteAttributeValidator : AttributeValidator<ValidateSpriteAttribute, Sprite>
    {
        protected override void Validate(ValidationResult result)
        {
            if (Value == null)
                return;

            var sprite = Value;
            var texture = sprite.texture;
            
            if (texture == null)
            {
                result.AddError("Sprite has no texture");
                return;
            }
            
            var path = AssetDatabase.GetAssetPath(texture);
            var importer = AssetImporter.GetAtPath(path) as TextureImporter;
            
            int width = texture.width;
            int height = texture.height;
            
            if (importer != null)
                importer.GetSourceTextureWidthAndHeight(out width, out height);
            
            if (Attribute.MinWidth >= 0 && width < Attribute.MinWidth)
                result.AddError($"Sprite width ({width}px) is less than minimum required ({Attribute.MinWidth}px)");
            
            if (Attribute.MinHeight >= 0 && height < Attribute.MinHeight)
                result.AddError($"Sprite height ({height}px) is less than minimum required ({Attribute.MinHeight}px)");
            
            if (Attribute.AspectRatio >= 0)
            {
                float actualAspectRatio = (float)width / height;
                float expectedAspectRatio = Attribute.AspectRatio;
                float difference = Mathf.Abs(actualAspectRatio - expectedAspectRatio);
                
                if (difference > Attribute.AspectRatioTolerance)
                    result.AddError($"Sprite aspect ratio ({actualAspectRatio:F2}) does not match expected ratio ({expectedAspectRatio:F2}). Dimensions: {width}x{height}");
            }
        }
    }
}
#endif