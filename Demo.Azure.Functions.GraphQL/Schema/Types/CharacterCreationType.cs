using GraphQL.Types;
using Demo.Azure.Functions.GraphQL.Documents;

namespace Demo.Azure.Functions.GraphQL.Schema.Types
{
    internal class CharacterCreationType: InputObjectGraphType
    {
        public CharacterCreationType()
        {
            Field<NonNullGraphType<StringGraphType>>(nameof(Character.Name));
            Field<StringGraphType>(nameof(Character.BirthYear));
        }
    }
}
