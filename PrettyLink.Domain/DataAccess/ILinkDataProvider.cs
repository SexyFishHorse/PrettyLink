namespace PrettyLink.Domain.DataAccess
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using JetBrains.Annotations;
    using PrettyLink.Domain.DataAccess.Model;

    public interface ILinkDataProvider
    {
        [NotNull]
        [ItemNotNull]
        Task<IEnumerable<Link>> GetLinksAsync();

        [NotNull]
        [ItemCanBeNull]
        Task<Link> GetLinkAsync([NotNull] string prettyLink);
    }
}
