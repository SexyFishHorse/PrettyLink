namespace PrettyLink.Domain.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Fenris.Validation.ArgumentValidation;
    using JetBrains.Annotations;
    using PrettyLink.Domain.DataAccess;
    using PrettyLink.Domain.DataAccess.Model;

    [UsedImplicitly]
    public class LinkService : ILinkService
    {
        private readonly ILinkDataProvider dataProvider;

        public LinkService(ILinkDataProvider dataProvider)
        {
            this.dataProvider = dataProvider;
        }

        public async Task<Link> GetLinkAsync(string prettyLink)
        {
            prettyLink.ShouldNotBeNullOrEmpty(nameof(prettyLink));

            return await dataProvider.GetLinkAsync(prettyLink).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Link>> GetLinksAsync()
        {
            return await dataProvider.GetLinksAsync().ConfigureAwait(false);
        }
    }
}
