// ---------------------------------------------------------------------------------
// ICT - ICT.Models
// Route.cs
// DRNL
// 2016.02.08
// ---------------------------------------------------------------------------------

using ICT.Core.DBC;
using ICT.Models.Interfaces;
using System.Collections.Generic;

namespace ICT.Models
{
    /// <summary>
    /// Implementation of the route class.
    /// </summary>
    public class Route : IRoute
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public Route()
        {
            // Initialize instance variables
            Links = new List<ILink>();
        }

        /// <summary>
        /// City link collection.
        /// </summary>
        private List<ILink> Links { get; set; }

        /// <summary>
        /// Get the city link collection.
        /// </summary>
        /// <returns>City link collection</returns>
        public IEnumerable<ILink> GetLinks()
        {
            // Get the city link collection
            var links = new List<ILink>(Links);

            return links;
        }

        /// <summary>
        /// Add a link at the end of the collection.
        /// </summary>
        /// <param name="link">Given link</param>
        public void AddLink(ILink link)
        {
            // Add link to the collection
            Links.Add(link);
        }

        /// <summary>
        /// Remove all the links from the collection.
        /// </summary>
        public void Clear()
        {
            // Remove all the links from the collection
            Links.Clear();
        }

        /// <summary>
        /// Get the start city in the route.
        /// </summary>
        /// <returns>Start city in the route</returns>
        public ICity GetStartCity()
        {
            // Precondition
            Contract.Requires(Links.Count > 0, "City link collection is not empty");

            return Links[0].StartCity;
        }

        /// <summary>
        /// Get the final city in the route.
        /// </summary>
        /// <returns>Final city in the route</returns>
        public ICity GetFinalCity()
        {
            // Precondition
            Contract.Requires(Links.Count > 0, "City link collection is not empty");

            return Links[Links.Count - 1].FinalCity;
        }

        /// <summary>
        /// Get city collection.
        /// </summary>
        /// <returns>City collection</returns>
        public IEnumerable<ICity> GetCities()
        {
            var cities = new List<ICity>();

            // Check the number of links
            if (Links.Count == 0)
            {
                return cities;
            }

            // Traverse all the links for the start cities
            Links.ForEach(l => cities.Add(l.StartCity));

            // Get the final city
            cities.Add(GetFinalCity());

            return cities;
        }

        /// <summary>
        /// Get the final distance of the route.
        /// </summary>
        /// <returns>Final distance of the route</returns>
        public int GetDistance()
        {
            var distance = 0;

            // Traverse the collection
            Links.ForEach(l => distance += l.Distance);

            return distance;
        }
    }
}
