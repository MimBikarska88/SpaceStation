using System;
using System.Linq;
using System.Text;
using SpaceStation.Core.Contracts;
using SpaceStation.Models.Astronauts;
using SpaceStation.Models.Mission;
using SpaceStation.Models.Planets;
using SpaceStation.Repositories;
using SpaceStation.Utilities.Messages;

namespace SpaceStation.Core
{
    public class Controller : IController
    {
        private AstronautRepository AstronautRepository;
        private PlanetRepository PlanetRepository;
        private PlanetRepository ExploredPlanetsRepository;

        public Controller()
        {
            this.AstronautRepository = new AstronautRepository();
            this.PlanetRepository = new PlanetRepository();
            this.ExploredPlanetsRepository = new PlanetRepository();
        }
        public string AddAstronaut(string type, string astronautName)
        {
            switch (type)
            {
                case "Biologist": AstronautRepository.Add(new Biologist(astronautName));
                    return string.Format(OutputMessages.AstronautAdded, type, astronautName);
                case "Geodesist": AstronautRepository.Add(new Geodesist(astronautName));
                    return string.Format(OutputMessages.AstronautAdded, type, astronautName);
                case "Meteorologist": AstronautRepository.Add(new Meteorologist(astronautName));
                    return string.Format(OutputMessages.AstronautAdded, type, astronautName);
                default:
                    throw new InvalidOperationException(ExceptionMessages.InvalidAstronautType);
            }
        }

        public string AddPlanet(string planetName, params string[] items)
        { 
            var planet = new Planet(planetName);
           foreach (var item in items)
           {
               planet.Items.Add(item);
           }
           PlanetRepository.Add(planet);
           return string.Format(OutputMessages.PlanetAdded, planetName);
        }

        public string RetireAstronaut(string astronautName)
        {
            var astronaut = AstronautRepository.FindByName(astronautName);
            if (astronaut == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InvalidRetiredAstronaut,astronautName));
            }
            else
            {
                AstronautRepository.Remove(astronaut);
                return string.Format(OutputMessages.AstronautRetired, astronautName);
            }
        }

        public string ExplorePlanet(string planetName)
        { 
            
                var team = AstronautRepository.Models.Where(astr => astr.Oxygen >= 60).ToList();
                if (!team.Any()) throw new InvalidOperationException(ExceptionMessages.InvalidAstronautCount);
                var planet = PlanetRepository.FindByName(planetName);

                var mission = new Mission();
                mission.Explore(planet, team);
                int count = team.Count(astronaut => !astronaut.CanBreath);
                ExploredPlanetsRepository.Add(planet);
                PlanetRepository.Remove(planet);
                return string.Format(OutputMessages.PlanetExplored,planetName, count);

        }

        public string Report()
        {
            var stringbuilder = new StringBuilder();
            int planetsCount = ExploredPlanetsRepository.Models.Where(model => model.Items.Count == 0).Count();
            
            stringbuilder.Append($"{planetsCount} planets were explored!"+Environment.NewLine);
            stringbuilder.Append("Astronauts info:" + Environment.NewLine);
            foreach (var astro in AstronautRepository.Models)
            {
                stringbuilder.Append($"Name: {astro.Name}"+Environment.NewLine);
                stringbuilder.Append($"Oxygen: {astro.Oxygen}" + Environment.NewLine);
                if (astro.Bag.Items.Count() == 0)
                {
                    stringbuilder.Append("Bag items: none" + Environment.NewLine);
                }
                else
                {
                    stringbuilder.Append($"Bag items: {string.Join(", ",astro.Bag.Items)}"+Environment.NewLine);
                }
            }

            return stringbuilder.ToString();
        }
    }
}