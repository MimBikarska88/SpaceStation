using System.Collections.Generic;
using System.Linq;
using SpaceStation.Models.Planets.Contracts;
using SpaceStation.Repositories.Contracts;

namespace SpaceStation.Repositories
{
    public class PlanetRepository : IRepository<IPlanet>
    {
        private List<IPlanet> Planets;

        public PlanetRepository()
        {
            this.Planets = new List<IPlanet>();
        }

        public IReadOnlyCollection<IPlanet> Models
        {
            get => Planets;
        }
        public void Add(IPlanet model)
        {
            this.Planets.Add(model);
        }

        public bool Remove(IPlanet model)
        {
            return this.Planets.Remove(model);
        }

        public IPlanet FindByName(string name)
        {
            return this.Models.FirstOrDefault(planet => planet.Name == name);
        }
    }
}