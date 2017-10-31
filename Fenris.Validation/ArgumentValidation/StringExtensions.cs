namespace Fenris.Validation.ArgumentValidation
{
    using System;
    using JetBrains.Annotations;

    [PublicAPI]
    public static class StringExtensions
    {
        [AssertionMethod]
        public static void ShouldNotBeNullOrEmpty(
            [AssertionCondition(AssertionConditionType.IS_NOT_NULL)] this string stringToValidate,
            [InvokerParameterName] string paramName,
            string errorMessage = null)
        {
            errorMessage = errorMessage ?? "String cannot be null or empty.";

            if (stringToValidate == null)
            {
                throw new ArgumentNullException(paramName, errorMessage);
            }

            if (string.IsNullOrEmpty(stringToValidate))
            {
                throw new ArgumentException(errorMessage, paramName);
            }
        }

        [AssertionMethod]
        public static void ShouldNotBeNullOrWhitespace(
            [AssertionCondition(AssertionConditionType.IS_NOT_NULL)] this string stringToValidate,
            [InvokerParameterName] string paramName,
            string errorMessage = null)
        {
            errorMessage = errorMessage ?? "String cannot be null or whitespace.";

            if (stringToValidate == null)
            {
                throw new ArgumentNullException(paramName, errorMessage);
            }

            if (string.IsNullOrWhiteSpace(stringToValidate))
            {
                throw new ArgumentException(errorMessage, paramName);
            }
        }
    }
}
