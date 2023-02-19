using HotChocolate.Data.Neo4J;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebApplicationGQL.Models
{

    public class Building
    {
        public Building() { } 

        public string Name { get; set; } = "";

        public List<Floor> Floors { get; set; }    = new List<Floor>();

    }
}