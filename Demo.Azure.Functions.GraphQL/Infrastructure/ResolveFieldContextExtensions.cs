using System;
using System.Linq;
using System.Text;
using GraphQL.Types;
using Microsoft.Azure.Documents;

namespace Demo.Azure.Functions.GraphQL.Infrastructure
{
    internal static class ResolveFieldContextExtensions
    {
        public static SqlQuerySpec ToSqlQuerySpec<TSource>(this ResolveFieldContext<TSource> context)
        {
            StringBuilder queryTextStringBuilder = new StringBuilder();

            queryTextStringBuilder.Append("SELECT ");

            queryTextStringBuilder.Append(String.Join(',', context.SubFields.Keys.Select(fieldName => $"c.{Char.ToUpperInvariant(fieldName[0])}{fieldName.Substring(1)}")));

            queryTextStringBuilder.Append(" FROM c");

            return new SqlQuerySpec(queryTextStringBuilder.ToString());
        }
    }
}
