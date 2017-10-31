namespace PrettyLink.Api.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Fenris.Validation.ArgumentValidation;
    using Microsoft.AspNetCore.Mvc;
    using PrettyLink.Api.ComplexTypes;
    using PrettyLink.Api.ComplexTypes.Requests;
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
        /// Creates a new pretty redirect link for an existing url.
        /// </summary>
        /// <param name="request">The link to create a redirect for</param>
        /// <response code="201">Link has been successfully created.</response>
        [HttpPost("/v1/links")]
        [ProducesResponseType(typeof(Link), 201)]
        public async Task<IActionResult> CreateLinkAsync([FromBody] CreateLinkRequest request)
        {
            request.ShouldNotBeNull(nameof(request));
            request.OriginalLink = request.OriginalLink?.Trim();
            request.OriginalLink.ShouldNotBeNullOrEmpty(nameof(request));

            var link = await service.CreateLinkAsync(request.OriginalLink).ConfigureAwait(false);

            return Created($"/v1/links/{link.PrettyLink}", Mapper.Map<DomainLink, Link>(link));
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
