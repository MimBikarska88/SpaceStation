using System;
using System.Collections.Generic;
using SpaceStation.Models.Planets.Contracts;
using SpaceStation.Utilities.Messages;

namespace SpaceStation.Models.Planets
{
    public class Planet : IPlanet
    {
        private string name;
        public ICollection<string> Items { get; }

        public Planet(string name)
        {
            this.Name = name;
            this.Items = new List<string>();
        }
        public string Name
        {
            get => name;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(ExceptionMessages.InvalidPlanetName);
                }

                name = value;
            }
        }
    }
}