﻿namespace PrettyLink.Domain.DataAccess
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Amazon.DynamoDBv2;
    using Amazon.DynamoDBv2.DataModel;
    using JetBrains.Annotations;
    using PrettyLink.Domain.DataAccess.Model;

    [UsedImplicitly]
    public class LinkDataProvider : ILinkDataProvider
    {
        public async Task<IEnumerable<Link>> GetLinksAsync()
        {
            using (var context = new DynamoDBContext(new AmazonDynamoDBClient(new AmazonDynamoDBConfig())))
            {
                var asyncSearch = context.ScanAsync<Link>(new ScanCondition[0]);

                return await asyncSearch.GetRemainingAsync().ConfigureAwait(false);
            }
        }
    }
}