namespace PrettyLink.Api.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using PrettyLink.Api.ComplexTypes;
    using PrettyLink.Domain.Services;
    using DomainLink = PrettyLink.Domain.DataAccess.Model.Link;

    public class LinkController : Controller
    {
        private readonly ILinkService service;

        public LinkController(ILinkService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Returns a list of all pretty links and their redirects
        /// </summary>
        [HttpGet("v1/links")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Link), 200)]
        public async Task<IActionResult> GetLinksAsync()
        {
            var links = await service.GetLinksAsync().ConfigureAwait(false);

            return Ok(Mapper.Map<IEnumerable<DomainLink>, IEnumerable<Link>>(links));
        }
    }
}
