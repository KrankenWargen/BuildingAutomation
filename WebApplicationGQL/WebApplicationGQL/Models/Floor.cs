using HotChocolate.Data.Neo4J;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationGQL.Models

{ 
    public class Floor { 
  

        public string Name { get; set; }

        public List<Building> Buildings { get; set; } 

    }
}