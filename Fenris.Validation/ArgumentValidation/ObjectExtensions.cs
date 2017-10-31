namespace Fenris.Validation.ArgumentValidation
{
    using System;
    using JetBrains.Annotations;

    public static class ObjectExtensions
    {
        [AssertionMethod]
        public static void ShouldNotBeNull(
            [AssertionCondition(AssertionConditionType.IS_NOT_NULL)] this object valueToValidate,
            [InvokerParameterName] string paramName,
            string errorMessage = null)
        {
            errorMessage = errorMessage ?? "Value can not be null.";

            if (valueToValidate == null)
            {
                throw new ArgumentNullException(paramName, errorMessage);
            }
        }
    }
}
