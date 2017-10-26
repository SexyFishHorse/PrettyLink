namespace PrettyLink.Domain.DataAccess.Model
{
    using System;
    using Amazon.DynamoDBv2.DataModel;
    using JetBrains.Annotations;

    [UsedImplicitly]
    [DynamoDBTable("dev_links")]
    public class Link
    {
        public LinkMetadata Metadata { get; set; }

        public string OriginalLink { get; set; }

        [DynamoDBHashKey("PrettyLink")]
        public string PrettyLink { get; set; }

        [UsedImplicitly]
        public class LinkMetadata
        {
            public DateTime Created { get; set; }
        }
    }
}
