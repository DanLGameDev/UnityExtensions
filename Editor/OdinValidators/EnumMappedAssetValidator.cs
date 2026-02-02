using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DGP.UnityExtensions.Editor.OdinValidators;
using DGP.UnityExtensions.Validation;
using Sirenix.OdinInspector.Editor.Validation;
using UnityEditor;

[assembly: RegisterValidator(typeof(EnumMappedAssetValidator))]

namespace DGP.UnityExtensions.Editor.OdinValidators
{
    public class EnumMappedAssetValidator : GlobalValidator
    {
        public override IEnumerable RunValidation(ValidationResult result)
        {
            var enumTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.IsEnum && type.GetCustomAttributes(typeof(EnumMappedAssetAttribute), false).Length > 0);

            foreach (var enumType in enumTypes)
            {
                ValidateEnumMapping(enumType, result);
            }

            yield return result;
        }

        private void ValidateEnumMapping(Type enumType, ValidationResult result)
        {
            var attribute = (EnumMappedAssetAttribute)enumType.GetCustomAttributes(typeof(EnumMappedAssetAttribute), false)[0];
            var soType = attribute.ScriptableObjectType;

            // Check if the SO implements IEnumMappedAsset
            if (!typeof(IEnumMappedAsset).IsAssignableFrom(soType)) {
                result.AddError($"Type {soType.Name} must implement IEnumMappedAsset");
                return;
            }

            // Get all enum values with their keys
            var enumFields = enumType.GetFields(BindingFlags.Public | BindingFlags.Static);
            var enumKeys = new Dictionary<string, string>(); // key -> enum value name
            
            foreach (var field in enumFields) {
                var keyAttr = field.GetCustomAttribute<EnumKeyAttribute>();
                if (keyAttr != null)
                {
                    enumKeys[keyAttr.Key] = field.Name;
                } else {
                    result.AddWarning($"Enum value {enumType.Name}.{field.Name} is missing [EnumKey] attribute");
                }
            }

            // Find all ScriptableObject instances of the specified type
            var guids = AssetDatabase.FindAssets($"t:{soType.Name}");
            var assets = guids
                .Select(guid => AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guid), soType))
                .Where(asset => asset != null)
                .Cast<IEnumMappedAsset>()
                .ToList();

            // Track which keys have corresponding assets
            var mappedKeys = new HashSet<string>();
            var duplicateKeys = new Dictionary<string, List<string>>(); // key -> asset names

            foreach (var asset in assets) {
                var key = asset.EnumKey;
                
                if (string.IsNullOrEmpty(key)) {
                    result.AddError($"Asset {((UnityEngine.Object)asset).name} has empty EnumKey");
                    continue;
                }
                
                if (!mappedKeys.Add(key)) {
                    // Duplicate found
                    if (!duplicateKeys.ContainsKey(key))
                        duplicateKeys[key] = new List<string>();
                    
                    duplicateKeys[key].Add(((UnityEngine.Object)asset).name);
                }
            }

            // Report duplicates
            foreach (var kvp in duplicateKeys) {
                var duplicateAssets = string.Join(", ", kvp.Value);
                var enumValueName = enumKeys.ContainsKey(kvp.Key) ? enumKeys[kvp.Key] : "unknown";
                result.AddError($"Duplicate {soType.Name} instances found for key '{kvp.Key}' (enum: {enumValueName}): {duplicateAssets}");
            }

            // Find missing mappings
            var missingKeys = enumKeys.Keys.Except(mappedKeys).ToList();
            if (missingKeys.Any()) {
                var missingInfo = string.Join(", ", missingKeys.Select(k => $"{enumKeys[k]} (key: '{k}')"));
                result.AddError($"Missing {soType.Name} instances for enum values: {missingInfo}");
            }

            // Find extra assets (assets with keys not in the enum)
            var extraAssets = assets
                .Where(asset => !enumKeys.ContainsKey(asset.EnumKey))
                .ToList();
            
            if (extraAssets.Any()) {
                var extraNames = string.Join(", ", extraAssets.Select(a => $"{((UnityEngine.Object)a).name} (key: '{a.EnumKey}')"));
                result.AddWarning($"Found {soType.Name} instances with keys not in {enumType.Name}: {extraNames}");
            }
        }
    }
}