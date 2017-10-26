namespace PrettyLink.Api.ComplexTypes
{
    using System.ComponentModel.DataAnnotations;
    using JetBrains.Annotations;

    [PublicAPI]
    public class Link
    {
        /// <summary>
        /// The link that the pretty link redirects to
        /// </summary>
        [Required]
        public string OriginalLink { get; set; }

        /// <summary>
        /// The pretty link which redirects to the original link
        /// </summary>
        [Required]
        public string PrettyLink { get; set; }
    }
}
