namespace PrettyLink.Api.Middleware
{
    using System;
    using System.Threading.Tasks;
    using JetBrains.Annotations;
    using Microsoft.AspNetCore.Http;
    using Newtonsoft.Json;
    using PrettyLink.Api.ComplexTypes;

    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class ArgumentExceptionHandler
    {
        private readonly RequestDelegate next;

        public ArgumentExceptionHandler(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context).ConfigureAwait(false);
            }
            catch (ArgumentException ex)
            {
                context.Response.Clear();
                context.Response.StatusCode = 400;
                context.Response.ContentType = "application/json";

                var errorMessage = JsonConvert.SerializeObject(new ArgumentErrorResponse(ex.Message, ex.ParamName));

                await context.Response.WriteAsync(errorMessage).ConfigureAwait(false);
            }
        }
    }
}
