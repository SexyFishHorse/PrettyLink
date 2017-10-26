namespace PrettyLink.Api.Configuration
{
    using System.Diagnostics.CodeAnalysis;
    using AutoMapper;
    using Microsoft.AspNetCore.Builder;
    using ApiLink = PrettyLink.Api.ComplexTypes.Link;
    using DomainLink = PrettyLink.Domain.DataAccess.Model.Link;

    public static class MappingConfiguration
    {
        [SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "Recommended design pattern for app configuration")]
        public static IApplicationBuilder AddAutoMapper(this IApplicationBuilder app)
        {
            Mapper.Initialize(mapper => { mapper.CreateMap<DomainLink, ApiLink>(); });

            return app;
        }
    }
}
