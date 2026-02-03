#if ODIN_VALIDATOR && ENABLE_LOCALIZATION
using System;

namespace DGP.UnityExtensions.Validation
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class RequiredLocalizedStringAttribute : Attribute
    {
    }
}
#endif