namespace PrettyLink.Domain.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using JetBrains.Annotations;
    using PrettyLink.Domain.DataAccess.Model;

    public interface ILinkService
    {
        [NotNull]
        [ItemNotNull]
        Task<IEnumerable<Link>> GetLinksAsync();
    }
}
