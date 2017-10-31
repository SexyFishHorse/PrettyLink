namespace PrettyLink.Domain.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Fenris.Validation.ArgumentValidation;
    using JetBrains.Annotations;
    using PrettyLink.Domain.DataAccess;
    using PrettyLink.Domain.DataAccess.Model;

    [UsedImplicitly]
    public class LinkService : ILinkService
    {
        private readonly ILinkDataProvider linkDataProvider;

        private readonly IWordDataProvider wordDataProvider;

        public LinkService([NotNull] ILinkDataProvider linkDataProvider, [NotNull] IWordDataProvider wordDataProvider)
        {
            this.linkDataProvider = linkDataProvider ?? throw new ArgumentNullException(nameof(linkDataProvider));
            this.wordDataProvider = wordDataProvider ?? throw new ArgumentNullException(nameof(wordDataProvider));
        }

        public async Task<Link> CreateLinkAsync(string originalLink)
        {
            originalLink.ShouldNotBeNullOrEmpty(nameof(originalLink));

            if (Uri.IsWellFormedUriString(originalLink, UriKind.Absolute) == false)
            {
                throw new ArgumentException("Link is not a valid url.", nameof(originalLink));
            }

            var link = new Link
            {
                OriginalLink = originalLink,
                Metadata =
                {
                    Created = DateTime.UtcNow,
                },
            };

            while (true)
            {
                var adjective1 = wordDataProvider.GetAdjective();
                var adjective2 = wordDataProvider.GetAdjective();
                var noun = wordDataProvider.GetNoun();

                var prettyLinkCandidate = $"{adjective1}{adjective2}{noun}";

                if (await linkDataProvider.GetLinkAsync(prettyLinkCandidate).ConfigureAwait(false) != null)
                {
                    continue;
                }

                link.PrettyLink = prettyLinkCandidate;
                await linkDataProvider.SaveLinkAsync(link).ConfigureAwait(false);

                break;
            }

            return link;
        }

        public async Task<Link> GetLinkAsync(string prettyLink)
        {
            prettyLink.ShouldNotBeNullOrEmpty(nameof(prettyLink));

            return await linkDataProvider.GetLinkAsync(prettyLink).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Link>> GetLinksAsync()
        {
            return await linkDataProvider.GetLinksAsync().ConfigureAwait(false);
        }
    }
}
