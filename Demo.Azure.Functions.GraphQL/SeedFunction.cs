using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Demo.Azure.Functions.GraphQL.Documents;

namespace Demo.Azure.Functions.GraphQL
{
    public static class SeedFunction
    {
        [FunctionName("seed")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest request,
            [CosmosDB(databaseName: "Demo.Azure.Functions.GraphQL", collectionName: "CharactersCollection", ConnectionStringSetting = "CosmosDBConnection", CreateIfNotExists = true)] IAsyncCollector<Character> charactersCollector)
        {
            await charactersCollector.AddAsync(new Character { Id = Guid.NewGuid().ToString("N"), Name = "Luke Skywalker", BirthYear = "19BBY" });
            await charactersCollector.AddAsync(new Character { Id = Guid.NewGuid().ToString("N"), Name = "C-3PO", BirthYear = "112BBY" });
            await charactersCollector.AddAsync(new Character { Id = Guid.NewGuid().ToString("N"), Name = "R2-D2", BirthYear = "33BBY" });
            await charactersCollector.AddAsync(new Character { Id = Guid.NewGuid().ToString("N"), Name = "Darth Vader", BirthYear = "41.9BBY" });
            await charactersCollector.AddAsync(new Character { Id = Guid.NewGuid().ToString("N"), Name = "Leia Organa", BirthYear = "19BBY" });
            await charactersCollector.AddAsync(new Character { Id = Guid.NewGuid().ToString("N"), Name = "Owen Lars", BirthYear = "52BBY" });
            await charactersCollector.AddAsync(new Character { Id = Guid.NewGuid().ToString("N"), Name = "Beru Whitesun Lars", BirthYear = "47BBY" });
            await charactersCollector.AddAsync(new Character { Id = Guid.NewGuid().ToString("N"), Name = "R5-D4", BirthYear = "Unknown" });
            await charactersCollector.AddAsync(new Character { Id = Guid.NewGuid().ToString("N"), Name = "Biggs Darklighter", BirthYear = "24BBY" });
            await charactersCollector.AddAsync(new Character { Id = Guid.NewGuid().ToString("N"), Name = "Obi-Wan Kenobi", BirthYear = "57BBY" });
            await charactersCollector.AddAsync(new Character { Id = Guid.NewGuid().ToString("N"), Name = "Anakin Skywalker", BirthYear = "41.9BBY" });
            await charactersCollector.AddAsync(new Character { Id = Guid.NewGuid().ToString("N"), Name = "Wilhuff Tarkin", BirthYear = "64BBY" });
            await charactersCollector.AddAsync(new Character { Id = Guid.NewGuid().ToString("N"), Name = "Chewbacca", BirthYear = "200BBY" });
            await charactersCollector.AddAsync(new Character { Id = Guid.NewGuid().ToString("N"), Name = "Han Solo", BirthYear = "29BBY" });
            await charactersCollector.AddAsync(new Character { Id = Guid.NewGuid().ToString("N"), Name = "Greedo", BirthYear = "44BBY" });
            await charactersCollector.AddAsync(new Character { Id = Guid.NewGuid().ToString("N"), Name = "Jabba Desilijic Tiure", BirthYear = "600BBY" });
            await charactersCollector.AddAsync(new Character { Id = Guid.NewGuid().ToString("N"), Name = "Wedge Antilles", BirthYear = "21BBY" });
            await charactersCollector.AddAsync(new Character { Id = Guid.NewGuid().ToString("N"), Name = "Jek Tono Porkins", BirthYear = "Unknown" });
            await charactersCollector.AddAsync(new Character { Id = Guid.NewGuid().ToString("N"), Name = "Yoda", BirthYear = "896BBY" });
            await charactersCollector.AddAsync(new Character { Id = Guid.NewGuid().ToString("N"), Name = "Palpatine", BirthYear = "82BBY" });
            await charactersCollector.AddAsync(new Character { Id = Guid.NewGuid().ToString("N"), Name = "Boba Fett", BirthYear = "31.5BBY" });

            return new OkResult();
        }
    }
}
