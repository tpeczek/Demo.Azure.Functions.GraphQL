using Newtonsoft.Json;

namespace Demo.Azure.Functions.GraphQL.Documents
{
    public class Character
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string BirthYear { get; set; }
    }
}
