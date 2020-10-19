using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using GraphQL;
using GraphQL.Server.Internal;
using GraphQL.Server.Common;

namespace Demo.Azure.Functions.GraphQL.Infrastructure
{
    internal static class GraphQLExecuterExtensions
    {
        private const string OPERATION_NAME_KEY = "operationName";
        private const string QUERY_KEY = "query";
        private const string VARIABLES_KEY = "variables";

        private const string JSON_MEDIA_TYPE = "application/json";
        private const string GRAPHQL_MEDIA_TYPE = "application/graphql";
        private const string FORM_URLENCODED_MEDIA_TYPE = "application/x-www-form-urlencoded";

        public async static Task<ExecutionResult> ExecuteAsync(this IGraphQLExecuter graphQLExecuter, IServiceProvider serviceProvider, HttpRequest request)
        {
            string operationName = null;
            string query = null;
            Inputs variables = null;

            if (HttpMethods.IsGet(request.Method) || (HttpMethods.IsPost(request.Method) && request.Query.ContainsKey(QUERY_KEY)))
            {
                (operationName, query, variables) = ExtractGraphQLAttributesFromQueryString(request);
            }
            else if (HttpMethods.IsPost(request.Method))
            {
                if (!MediaTypeHeaderValue.TryParse(request.ContentType, out var mediaTypeHeader))
                {
                    throw new GraphQLBadRequestException($"Could not parse 'Content-Type' header value '{request.ContentType}'.");
                }

                switch (mediaTypeHeader.MediaType)
                {
                    case JSON_MEDIA_TYPE:
                        (operationName, query, variables) = await ExtractGraphQLAttributesFromJsonBodyAsync(request);
                        break;
                    case GRAPHQL_MEDIA_TYPE:
                        query = await ExtractGraphQLQueryFromGraphQLBodyAsync(request.Body);
                        break;
                    case FORM_URLENCODED_MEDIA_TYPE:
                        (operationName, query, variables) = await ExtractGraphQLAttributesFromFormCollectionAsync(request);
                        break;
                    default:
                        throw new GraphQLBadRequestException($"Not supported 'Content-Type' header value '{request.ContentType}'.");
                }
            }
            
            return await graphQLExecuter.ExecuteAsync(operationName, query, variables, null, serviceProvider, request.HttpContext.RequestAborted);
        }

        private static (string operationName, string query, Inputs variables) ExtractGraphQLAttributesFromQueryString(HttpRequest request)
        {
            return (
                request.Query.TryGetValue(OPERATION_NAME_KEY, out var operationNameValues) ? operationNameValues[0] : null,
                request.Query.TryGetValue(QUERY_KEY, out var queryValues) ? queryValues[0] : null,
                request.Query.TryGetValue(VARIABLES_KEY, out var variablesValues) ? JsonSerializer.Deserialize<Dictionary<string, object>>(variablesValues[0]).ToInputs() : null
            );
        }

        private async static Task<(string operationName, string query, Inputs variables)> ExtractGraphQLAttributesFromJsonBodyAsync(HttpRequest request)
        {
            GraphQLRequest graphQLRequest = await JsonSerializer.DeserializeAsync<GraphQLRequest>
            (
                request.Body,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            return (
                graphQLRequest.OperationName,
                graphQLRequest.Query,
                graphQLRequest.Inputs
            );
        }

        private static Task<string> ExtractGraphQLQueryFromGraphQLBodyAsync(Stream body)
        {
            using (StreamReader bodyReader = new StreamReader(body))
            {
                return bodyReader.ReadToEndAsync();
            }
        }

        private async static Task<(string operationName, string query, Inputs variables)> ExtractGraphQLAttributesFromFormCollectionAsync(HttpRequest request)
        {
            IFormCollection requestFormCollection = await request.ReadFormAsync();

            return (
                requestFormCollection.TryGetValue(OPERATION_NAME_KEY, out var operationNameValues) ? operationNameValues[0] : null,
                requestFormCollection.TryGetValue(QUERY_KEY, out var queryValues) ? queryValues[0] : null,
                requestFormCollection.TryGetValue(VARIABLES_KEY, out var variablesValue) ? JsonSerializer.Deserialize<Dictionary<string, object>>(variablesValue[0]).ToInputs() : null
                );
        }
    }
}
