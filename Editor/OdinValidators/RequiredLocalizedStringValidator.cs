#if ENABLE_LOCALIZATION
using Sirenix.OdinInspector.Editor.Validation;
using DGP.UnityExtensions.Validation;
using UnityEngine.Localization;

namespace DGP.UnityExtensions.Editor.OdinValidators
{
    public class RequiredLocalizedStringValidator : AttributeValidator<RequiredLocalizedStringAttribute, LocalizedString>
    {
        protected override void Validate(ValidationResult result)
        {
            if (this.Value == null || this.Value.IsEmpty)
            {
                result.Message = "LocalizedString must have a table assigned";
                result.ResultType = ValidationResultType.Error;
            }
        }
    }
}
#endif