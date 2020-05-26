using GraphQL;
using GraphQLSchema = global::GraphQL.Types.Schema;
using Demo.Azure.Functions.GraphQL.Schema.Queries;
using Demo.Azure.Functions.GraphQL.Schema.Mutations;

namespace Demo.Azure.Functions.GraphQL.Schema
{
    public class StarWarsSchema: GraphQLSchema
    {
        public StarWarsSchema(IDependencyResolver dependencyResolver)
            : base(dependencyResolver)
        {
            Query = dependencyResolver.Resolve<StarWarsQuery>();
            Mutation = dependencyResolver.Resolve<StarWarsMutation>();
        }
    }
}
