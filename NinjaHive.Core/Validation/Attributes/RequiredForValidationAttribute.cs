using System;

namespace NinjaHive.Core.Validation.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class RequiredForValidationAttribute : Attribute
    {
    }
}
