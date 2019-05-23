using Microsoft.Extensions.DependencyInjection;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using GraphQL;
using GraphQL.Server;
using Demo.Azure.Functions.GraphQL.Schema;

[assembly: FunctionsStartup(typeof(Demo.Azure.Functions.GraphQL.Startup))]

namespace Demo.Azure.Functions.GraphQL
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddScoped<IDependencyResolver>(serviceProvider => new FuncDependencyResolver(serviceProvider.GetRequiredService));
            builder.Services.AddScoped<StarWarsSchema>();

            builder.Services.AddSingleton<IDocumentExecuter>(new DocumentExecuter());
            builder.Services.AddGraphQL(options =>
            {
                options.ExposeExceptions = true;
            })
            .AddGraphTypes(ServiceLifetime.Scoped);
        }
    }
}
