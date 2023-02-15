using HotChocolate.Data.Neo4J;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebApplicationGQL.Models
{
    /// <summary>
    /// Represents any software or service that has a command line interface.
    /// </summary>
    [Table("Building")]
    public class Building
    {
        /// <summary>
        /// Represents the unique ID for the platform.
        /// </summary>

        /// <summary>
        /// Represents the name for the platform.
        /// </summary>
        [Required]
        public string Name { get; set; } = "";

        /// <summary>
        /// This is the list of available commands for this platform.
        /// </summary>
        /// 
        [Neo4JRelationship("RELTYPE", RelationshipDirection.Outgoing)]
        public List<Floor> Floors { get; set; } = new List<Floor>();    

    }
}