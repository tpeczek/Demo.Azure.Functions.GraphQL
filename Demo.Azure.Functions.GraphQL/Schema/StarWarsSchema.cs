using System;
using GraphQL.Utilities;
using GraphQLSchema = GraphQL.Types.Schema;
using Demo.Azure.Functions.GraphQL.Schema.Queries;
using Demo.Azure.Functions.GraphQL.Schema.Mutations;

namespace Demo.Azure.Functions.GraphQL.Schema
{
    public class StarWarsSchema: GraphQLSchema
    {
        public StarWarsSchema(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            Query = serviceProvider.GetRequiredService<StarWarsQuery>();
            Mutation = serviceProvider.GetRequiredService<StarWarsMutation>();
        }
    }
}
