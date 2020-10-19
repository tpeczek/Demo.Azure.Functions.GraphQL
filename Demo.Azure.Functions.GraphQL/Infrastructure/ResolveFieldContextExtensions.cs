using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GraphQL;
using Microsoft.Azure.Documents;

namespace Demo.Azure.Functions.GraphQL.Infrastructure
{
    internal static class ResolveFieldContextExtensions
    {
        public static SqlQuerySpec ToSqlQuerySpec<TSource>(this IResolveFieldContext<TSource> context)
        {
            StringBuilder queryTextStringBuilder = new StringBuilder();

            queryTextStringBuilder.Append("SELECT ");

            queryTextStringBuilder.Append(String.Join(',', context.SubFields.Keys.Select(fieldName => $"c.{Char.ToUpperInvariant(fieldName[0])}{fieldName.Substring(1)}")));

            queryTextStringBuilder.Append(" FROM c");

            SqlParameterCollection parameters = new SqlParameterCollection();
            if (context.Arguments.Count > 0)
            {
                queryTextStringBuilder.Append(" WHERE ");

                foreach (KeyValuePair<string, object> argument in context.Arguments)
                {
                    queryTextStringBuilder.Append($"c.{Char.ToUpperInvariant(argument.Key[0])}{argument.Key.Substring(1)} = @{argument.Key} AND ");
                    parameters.Add(new SqlParameter("@" + argument.Key, argument.Value));
                }

                queryTextStringBuilder.Length -= 5;
            }

            return new SqlQuerySpec(queryTextStringBuilder.ToString(), parameters);
        }
    }
}
