using System;
using System.Data.Common;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GraphQL;
using GraphQL.Server;
using GraphQL.Execution;
using GraphQL.SystemTextJson;
using Demo.Azure.Functions.GraphQL.Schema;
using Demo.Azure.Functions.GraphQL.Infrastructure;

[assembly: FunctionsStartup(typeof(Demo.Azure.Functions.GraphQL.Startup))]

namespace Demo.Azure.Functions.GraphQL
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<IDocumentClient>(serviceProvider => {
                DbConnectionStringBuilder cosmosDBConnectionStringBuilder = new DbConnectionStringBuilder
                {
                    ConnectionString = serviceProvider.GetRequiredService<IConfiguration>()[Constants.CONNECTION_STRING_SETTING]
                };

                if (cosmosDBConnectionStringBuilder.TryGetValue("AccountKey", out object accountKey) && cosmosDBConnectionStringBuilder.TryGetValue("AccountEndpoint", out object accountEndpoint))
                {
                    return new DocumentClient(new Uri(accountEndpoint.ToString()), accountKey.ToString());

                }

                return null;
            });

            builder.Services.AddScoped<StarWarsSchema>();

            builder.Services.AddSingleton<IDocumentExecuter>(new DocumentExecuter());
            builder.Services.AddSingleton<IDocumentWriter>(new DocumentWriter());
            builder.Services.AddSingleton<IErrorInfoProvider>(services =>
            {
                return new ErrorInfoProvider(new ErrorInfoProviderOptions { ExposeExceptionStackTrace = true });
            });
            builder.Services.AddGraphQL()
                .AddGraphTypes(ServiceLifetime.Scoped)
                .AddDataLoader();
        }
    }
}
