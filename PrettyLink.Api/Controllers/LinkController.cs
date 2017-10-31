namespace PrettyLink.Api.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Fenris.Validation.ArgumentValidation;
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
        /// <response code="200">Links are returned successfully</response>
        [HttpGet("v1/links")]
        [ProducesResponseType(typeof(IEnumerable<Link>), 200)]
        public async Task<IActionResult> GetLinksAsync()
        {
            var links = await service.GetLinksAsync().ConfigureAwait(false);

            return Ok(Mapper.Map<IEnumerable<DomainLink>, IEnumerable<Link>>(links));
        }

        /// <summary>
        /// Returns a single link.
        /// </summary>
        /// <param name="prettyLink">The link to return</param>
        /// <response code="200">The link is returned successfully</response>
        /// <response code="404">If the link could not be found</response>
        [HttpGet("v1/links/{prettyLink}")]
        [ProducesResponseType(typeof(Link), 200)]
        [ProducesResponseType(typeof(Error), 404)]
        public async Task<IActionResult> GetLinkAsync(string prettyLink)
        {
            prettyLink = prettyLink?.Trim();
            prettyLink.ShouldNotBeNullOrEmpty(nameof(prettyLink));

            var link = await service.GetLinkAsync(prettyLink).ConfigureAwait(false);

            if (link == null)
            {
                return NotFound(new { Message = "Link not found." });
            }

            return Ok(Mapper.Map<DomainLink, Link>(link));
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("/{prettyLink}")]
        public async Task<IActionResult> RedirectToLink(string prettyLink)
        {
            prettyLink = prettyLink?.Trim();
            prettyLink.ShouldNotBeNullOrEmpty(nameof(prettyLink));

            var link = await service.GetLinkAsync(prettyLink).ConfigureAwait(false);

            if (link == null)
            {
                return NotFound(new { Message = "Link not found." });
            }

            return RedirectPermanent(link.OriginalLink);
        }
    }
}
