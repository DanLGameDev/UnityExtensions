using System.Collections;
using System.Collections.Generic;
using DGP.UnityExtensions.Editor.OdinValidators;
using DGP.UnityExtensions.Validation;
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector.Editor.Validation;
using UnityEngine;

[assembly: RegisterValidator(typeof(ValidateNoNullElementsArrayValidator))]
[assembly: RegisterValidator(typeof(ValidateNoNullElementsListValidator))]


namespace DGP.UnityExtensions.Editor.OdinValidators
{
    // Validator for arrays
    public class ValidateNoNullElementsArrayValidator : AttributeValidator<ValidateNoNullElementsAttribute>
    {
        public override bool CanValidateProperty(InspectorProperty property)
        {
            return property.ValueEntry?.TypeOfValue.IsArray ?? false;
        }

        protected override void Validate(ValidationResult result)
        {
            if (!(Property.ValueEntry.WeakSmartValue is IEnumerable enumerable))
                return;

            var nullIndices = new List<int>();
            int index = 0;
            foreach (var element in enumerable)
            {
                bool isNull = element == null || (element is Object unityObj && !unityObj);
                
                if (isNull)
                    nullIndices.Add(index);
                
                index++;
            }

            if (nullIndices.Count > 0)
            {
                result.AddError($"Found {nullIndices.Count} null element(s) at indices: {string.Join(", ", nullIndices)}")
                    .WithFix("Remove null elements", () =>
                    {
                        var array = Property.ValueEntry.WeakSmartValue as System.Array;
                        if (array == null) return;

                        var elementType = array.GetType().GetElementType();
                        var nonNullList = new List<object>();
                        
                        foreach (var element in array)
                        {
                            bool isNull = element == null || (element is Object unityObj && !unityObj);
                            if (!isNull)
                                nonNullList.Add(element);
                        }

                        var newArray = System.Array.CreateInstance(elementType, nonNullList.Count);
                        for (int i = 0; i < nonNullList.Count; i++)
                        {
                            newArray.SetValue(nonNullList[i], i);
                        }

                        Property.ValueEntry.WeakSmartValue = newArray;
                    });
            }
        }
    }

    // Validator for List<T>
    public class ValidateNoNullElementsListValidator : AttributeValidator<ValidateNoNullElementsAttribute>
    {
        public override bool CanValidateProperty(InspectorProperty property)
        {
            var type = property.ValueEntry?.TypeOfValue;
            bool isList = type != null && type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>);
            return isList;
        }

        protected override void Validate(ValidationResult result)
        {
            if (!(Property.ValueEntry.WeakSmartValue is IEnumerable enumerable))
                return;

            var nullIndices = new List<int>();
            int index = 0;
            foreach (var element in enumerable)
            {
                bool isNull = element == null || (element is Object unityObj && !unityObj);
                
                if (isNull)
                    nullIndices.Add(index);
                
                index++;
            }

            if (nullIndices.Count > 0)
            {
                result.AddError($"Found {nullIndices.Count} null element(s) at indices: {string.Join(", ", nullIndices)}")
                    .WithFix("Remove null elements", () =>
                    {
                        var list = Property.ValueEntry.WeakSmartValue as IList;
                        if (list == null) return;

                        // Remove in reverse order to maintain indices
                        for (int i = nullIndices.Count - 1; i >= 0; i--)
                        {
                            list.RemoveAt(nullIndices[i]);
                        }
                    });
            }
        }
    }
}