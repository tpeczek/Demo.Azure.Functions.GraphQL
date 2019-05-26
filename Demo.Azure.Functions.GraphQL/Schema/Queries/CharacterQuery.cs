using System;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using GraphQL.Types;
using Demo.Azure.Functions.GraphQL.Documents;
using Demo.Azure.Functions.GraphQL.Schema.Types;

namespace Demo.Azure.Functions.GraphQL.Schema.Queries
{
    internal class CharacterQuery: ObjectGraphType
    {
        private static readonly Uri _charactersCollectionUri = UriFactory.CreateDocumentCollectionUri("Demo.Azure.Functions.GraphQL", "CharactersCollection");
        private static readonly FeedOptions _feedOptions = new FeedOptions { MaxItemCount = -1, };

        public CharacterQuery(IDocumentClient documentClient)
        {
            Field<ListGraphType<CharacterType>>(
                "characters",
                resolve: context =>
                {
                    return documentClient.CreateDocumentQuery<Character>(_charactersCollectionUri, _feedOptions);
                });
        }
    }
}
