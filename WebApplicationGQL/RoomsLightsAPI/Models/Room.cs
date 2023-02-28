using HotChocolate.Data.Neo4J;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationGQL.Models

{ 
    public class Room
    { 

        public string Name { get; set; }

      

        public List<Light> Lights { get; set; } = new List<Light>();

    }
}