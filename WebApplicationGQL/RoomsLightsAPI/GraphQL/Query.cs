﻿using GoldBeckLight.Repositories;
using HotChocolate.Data.Neo4J;
using HotChocolate.Data.Neo4J.Execution;
using HotChocolate.Data.Neo4J.Language;
using Neo4j.Driver;
using ServiceStack;
using System;
using System.Diagnostics;
using WebApplicationGQL.Models;
using static HotChocolate.ErrorCodes;

namespace WebApplicationGQL.GraphQL
{
    [ExtendObjectType(Name = "Query")]
    public class Query
    {
 


        [UseProjection]
        [UseFiltering]
        public async Task<IEnumerable<Light>> GetLights(string roomName,[Service] ILightRepository lightRepository)
        {
            return await lightRepository.GetLightsByFloorName(roomName);
        }
    }
}
