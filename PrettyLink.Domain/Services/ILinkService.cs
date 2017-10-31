namespace PrettyLink.Domain.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using JetBrains.Annotations;
    using PrettyLink.Domain.DataAccess.Model;

    public interface ILinkService
    {
        [NotNull]
        [ItemCanBeNull]
        Task<Link> GetLinkAsync([NotNull] string prettyLink);

        [NotNull]
        [ItemNotNull]
        Task<IEnumerable<Link>> GetLinksAsync();
    }
}
