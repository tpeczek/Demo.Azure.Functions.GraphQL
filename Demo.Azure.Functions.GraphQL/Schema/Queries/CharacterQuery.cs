using System;
using System.Collections.Generic;
using GraphQL.Types;
using Demo.Azure.Functions.GraphQL.Documents;
using Demo.Azure.Functions.GraphQL.Schema.Types;

namespace Demo.Azure.Functions.GraphQL.Schema.Queries
{
    internal class CharacterQuery: ObjectGraphType
    {
        private static readonly IList<Character> _characters = new List<Character>
        {
            new Character { Id = Guid.NewGuid().ToString("N"), Name = "Luke Skywalker",         BirthYear = "19BBY"     },
            new Character { Id = Guid.NewGuid().ToString("N"), Name = "C-3PO",                  BirthYear = "112BBY"    },
            new Character { Id = Guid.NewGuid().ToString("N"), Name = "R2-D2",                  BirthYear = "33BBY"     },
            new Character { Id = Guid.NewGuid().ToString("N"), Name = "Darth Vader",            BirthYear = "41.9BBY"   },
            new Character { Id = Guid.NewGuid().ToString("N"), Name = "Leia Organa",            BirthYear = "19BBY"     },
            new Character { Id = Guid.NewGuid().ToString("N"), Name = "Owen Lars",              BirthYear = "52BBY"     },
            new Character { Id = Guid.NewGuid().ToString("N"), Name = "Beru Whitesun Lars",     BirthYear = "47BBY"     },
            new Character { Id = Guid.NewGuid().ToString("N"), Name = "R5-D4",                  BirthYear = "Unknown"   },
            new Character { Id = Guid.NewGuid().ToString("N"), Name = "Biggs Darklighter",      BirthYear = "24BBY"     },
            new Character { Id = Guid.NewGuid().ToString("N"), Name = "Obi-Wan Kenobi",         BirthYear = "57BBY"     },
            new Character { Id = Guid.NewGuid().ToString("N"), Name = "Anakin Skywalker",       BirthYear = "41.9BBY"   },
            new Character { Id = Guid.NewGuid().ToString("N"), Name = "Wilhuff Tarkin",         BirthYear = "64BBY"     },
            new Character { Id = Guid.NewGuid().ToString("N"), Name = "Chewbacca",              BirthYear = "200BBY"    },
            new Character { Id = Guid.NewGuid().ToString("N"), Name = "Han Solo",               BirthYear = "29BBY"     },
            new Character { Id = Guid.NewGuid().ToString("N"), Name = "Greedo",                 BirthYear = "44BBY"     },
            new Character { Id = Guid.NewGuid().ToString("N"), Name = "Jabba Desilijic Tiure",  BirthYear = "600BBY"    },
            new Character { Id = Guid.NewGuid().ToString("N"), Name = "Wedge Antilles",         BirthYear = "21BBY"     },
            new Character { Id = Guid.NewGuid().ToString("N"), Name = "Jek Tono Porkins",       BirthYear = "Unknown"   },
            new Character { Id = Guid.NewGuid().ToString("N"), Name = "Yoda",                   BirthYear = "896BBY"    },
            new Character { Id = Guid.NewGuid().ToString("N"), Name = "Palpatine",              BirthYear = "82BBY"     },
            new Character { Id = Guid.NewGuid().ToString("N"), Name = "Boba Fett",              BirthYear = "31.5BBY"   }
        };

        public CharacterQuery()
        {
            Field<ListGraphType<CharacterType>>(
                "characters",
                resolve: context =>
                {
                    return _characters;
                });
        }
    }
}
