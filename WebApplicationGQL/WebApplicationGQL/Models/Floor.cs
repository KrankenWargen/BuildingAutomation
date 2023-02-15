using HotChocolate.Data.Neo4J;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationGQL.Models
{
    /// <summary>
    /// Represents any executable command.
    /// </summary>
    public class Floor { 
  

        [Required]
        public string Name { get; set; }

        [Neo4JRelationship("RELTYPE", RelationshipDirection.Incoming)]
        public List<Building> Buildings { get; set; } = new List<Building>();

    }
}