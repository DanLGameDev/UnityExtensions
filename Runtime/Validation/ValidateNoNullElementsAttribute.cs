using System;
using System.Diagnostics.CodeAnalysis;

namespace DGP.UnityExtensions.Validation
{
    [ExcludeFromCodeCoverage]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class ValidateNoNullElementsAttribute : Attribute
    {
    }
}