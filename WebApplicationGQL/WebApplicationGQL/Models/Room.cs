
using System.ComponentModel.DataAnnotations;

namespace GoldBeckLight.Models

{ 
    public class Room
    { 

        public string Name { get; set; }

        public List<Light> Lights { get; set; }

    }
}