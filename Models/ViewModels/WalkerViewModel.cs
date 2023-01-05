using System.Collections.Generic;

namespace DogGo.Models.ViewModels
{
    public class WalkerViewModel
    {
        public Walker Walker { get; set; }
        public List<Neighborhood> Neighborhoods { get; set; }
        public List<Owner> Owners { get; set; }
        public List<Dog> Dogs { get; set; }
    }
}
