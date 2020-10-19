using System;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using GraphQL;
using GraphQL.Types;
using Demo.Azure.Functions.GraphQL.Documents;
using Demo.Azure.Functions.GraphQL.Schema.Types;
using Demo.Azure.Functions.GraphQL.Infrastructure;

namespace Demo.Azure.Functions.GraphQL.Schema.Mutations
{
    internal class StarWarsMutation : ObjectGraphType
    {
        private const string HOMEWORLD_MUTATION_ARGUMENT = "homeworld";
        private const string CHARACTER_MUTATION_ARGUMENT = "character";

        private static readonly Uri _charactersCollectionUri = UriFactory.CreateDocumentCollectionUri(Constants.DATABASE_NAME, Constants.CHARACTERS_COLLECTION_NAME);

        public StarWarsMutation(IDocumentClient documentClient)
        {
            FieldAsync<CharacterType>(
                "createCharacter",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = HOMEWORLD_MUTATION_ARGUMENT },
                    new QueryArgument<NonNullGraphType<CharacterCreationType>> { Name = CHARACTER_MUTATION_ARGUMENT }
                ),
                resolve: async context =>
                {
                    Character character = context.GetArgument<Character>(CHARACTER_MUTATION_ARGUMENT);
                    character.HomeworldId = context.GetArgument<int>(HOMEWORLD_MUTATION_ARGUMENT);
                    character.CharacterId = Guid.NewGuid().ToString("N");

                    await documentClient.CreateDocumentAsync(_charactersCollectionUri, character);

                    return character;
                }
            );
        }
    }
}
