namespace PrettyLink.Api.ComplexTypes
{
    using JetBrains.Annotations;

    [PublicAPI]
    public class ArgumentErrorResponse
    {
        public ArgumentErrorResponse(string message, string parameterName)
        {
            Message = message;
            ParameterName = parameterName;
        }

        /// <summary>
        /// The error message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The parameter which failed validation
        /// </summary>
        public string ParameterName { get; set; }
    }
}
