using HotChocolate.Data.Neo4J;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationGQL.Models

{ 
    public class Light
    { 

        public string Name { get; set; }
        public bool IsOn { get; set; }


    }
}