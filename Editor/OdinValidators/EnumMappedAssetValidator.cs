using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

            // Check if the SO implements IEnumMappedAsset<T>
            var expectedInterfaceType = typeof(IEnumMappedAsset<>).MakeGenericType(enumType);
            if (!expectedInterfaceType.IsAssignableFrom(soType))
            {
                result.AddError($"Type {soType.Name} must implement IEnumMappedAsset<{enumType.Name}>");
                return;
            }

            // Get all enum values
            var enumValues = Enum.GetValues(enumType).Cast<Enum>().ToList();

            // Find all ScriptableObject instances of the specified type
            var guids = AssetDatabase.FindAssets($"t:{soType.Name}");
            var assets = guids
                .Select(guid => AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guid), soType))
                .Where(asset => asset != null)
                .ToList();

            // Get the EnumValue property from the interface
            var enumValueProperty = expectedInterfaceType.GetProperty("EnumValue");
            if (enumValueProperty == null)
            {
                result.AddError($"Could not find EnumValue property on {soType.Name}");
                return;
            }

            // Group assets by their enum value
            var assetsByEnumValue = new Dictionary<Enum, List<UnityEngine.Object>>();
            foreach (var asset in assets)
            {
                var enumValue = (Enum)enumValueProperty.GetValue(asset);
                
                if (!assetsByEnumValue.ContainsKey(enumValue))
                    assetsByEnumValue[enumValue] = new List<UnityEngine.Object>();
                
                assetsByEnumValue[enumValue].Add((UnityEngine.Object)asset);
            }

            // Check for duplicates
            foreach (var kvp in assetsByEnumValue.Where(kvp => kvp.Value.Count > 1))
            {
                var duplicateAssets = string.Join(", ", kvp.Value.Select(a => a.name));
                result.AddError($"Duplicate {soType.Name} instances found for {enumType.Name}.{kvp.Key}: {duplicateAssets}");
            }

            // Check for missing mappings
            var mappedValues = assetsByEnumValue.Keys.ToHashSet();
            var missingValues = enumValues.Where(ev => !mappedValues.Contains(ev)).ToList();
            
            if (missingValues.Any())
            {
                var missingInfo = string.Join(", ", missingValues.Select(ev => ev.ToString()));
                result.AddError($"Missing {soType.Name} instances for {enumType.Name} values: {missingInfo}");
            }

            // Check for extra assets (assets with enum values not in the current enum definition)
            var extraAssets = assetsByEnumValue
                .Where(kvp => !enumValues.Contains(kvp.Key))
                .SelectMany(kvp => kvp.Value)
                .ToList();
            
            if (extraAssets.Any())
            {
                var extraNames = string.Join(", ", extraAssets.Select(a => $"{a.name} (value: {enumValueProperty.GetValue(a)})"));
                result.AddWarning($"Found {soType.Name} instances with enum values not in {enumType.Name}: {extraNames}");
            }
        }
    }
}