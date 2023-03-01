using GoldBeckLight.Resolvers;
using HotChocolate.Data.Neo4J;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationGQL.Models

{
   
    public class Floor {

        public string Name { get; set; }


        public List<Room> Rooms { get; set; } = new List<Room> { }; 


    }
}