namespace PrettyLink.Api.Configuration
{
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using JetBrains.Annotations;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.PlatformAbstractions;
    using PrettyLink.Domain.DataAccess;
    using PrettyLink.Domain.Services;

    [UsedImplicitly]
    [SuppressMessage("ReSharper", "UnusedMethodReturnValue.Global", Justification = "Recommended pattern for Dependency Injection")]
    [SuppressMessage("ReSharper", "UnusedMethodReturnValue.Local", Justification = "Recommended pattern for Dependency Injection")]
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddDataAccess();
            services.AddDomain();

            return services;
        }

        private static IServiceCollection AddDataAccess(this IServiceCollection services)
        {
            services.AddSingleton<ILinkDataProvider, LinkDataProvider>();
            services.AddSingleton<IWordDataProvider>(
                new WordDataProvider(Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "dictionary.json")));

            return services;
        }

        private static IServiceCollection AddDomain(this IServiceCollection services)
        {
            services.AddSingleton<ILinkService, LinkService>();

            return services;
        }
    }
}
