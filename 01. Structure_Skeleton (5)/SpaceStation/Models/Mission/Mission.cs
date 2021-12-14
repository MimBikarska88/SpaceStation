using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.XPath;
using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Models.Mission.Contracts;
using SpaceStation.Models.Planets.Contracts;

namespace SpaceStation.Models.Mission
{
    public class Mission  : IMission
    {
        public void Explore(IPlanet planet, ICollection<IAstronaut> astronauts)
        {
                for (int i = 0; i < astronauts.Count; ++i)
                {
                    var astronaut = astronauts.ElementAt(i);
                    while (astronaut.Oxygen > 0)
                    {
                        var item = planet.Items.FirstOrDefault();
                        if(item==null) break;
                        astronaut.Bag.Items.Add(item);
                        astronaut.Breath();
                        planet.Items.Remove(item);
                    }
                }
        }
    }
}