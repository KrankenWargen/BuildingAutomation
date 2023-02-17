using HotChocolate.Data.Neo4J;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationGQL.Models

{ 
    public class Floor { 
  

        public string Name { get; set; }

        [Neo4JRelationship("CONTAIN", RelationshipDirection.Outgoing)]

        public List<Room> Rooms { get; set; } 

    }
}