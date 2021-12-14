using System.Collections.Generic;
using System.Linq;
using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Repositories.Contracts;

namespace SpaceStation.Repositories
{
    public class AstronautRepository : IRepository<IAstronaut>
    {
        private List<IAstronaut> Astronauts;

        public AstronautRepository()
        {
            this.Astronauts = new List<IAstronaut>();
        }
        public IReadOnlyCollection<IAstronaut> Models
        {
            get => Astronauts;
        }
        public void Add(IAstronaut model)
        {
            this.Astronauts.Add(model);
        }

        public bool Remove(IAstronaut model)
        {
            return this.Astronauts.Remove(model);
        }

        public IAstronaut FindByName(string name)
        {
            return this.Models.FirstOrDefault(astronaut => astronaut.Name == name);
        }
    }
}