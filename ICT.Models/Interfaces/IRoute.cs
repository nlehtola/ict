// ---------------------------------------------------------------------------------
// ICT - ICT.Models
// IRoute.cs
// DRNL
// 2016.02.08
// ---------------------------------------------------------------------------------

using System.Collections.Generic;

namespace ICT.Models.Interfaces
{
    /// <summary>
    /// Interface for the route entity. A route entity represents a collection of
    /// city links.
    /// </summary>
    public interface IRoute
    {
        /// <summary>
        /// Get the city link collection.
        /// </summary>
        /// <returns>City link collection</returns>
        IEnumerable<ILink> GetLinks();

        /// <summary>
        /// Add a link at the end of the collection.
        /// </summary>
        /// <param name="link">Given link</param>
        void AddLink(ILink link);

        /// <summary>
        /// Remove all the links from the collection.
        /// </summary>
        void Clear();

        /// <summary>
        /// Get the start city in the route.
        /// </summary>
        /// <returns>Start city in the route</returns>
        ICity GetStartCity();

        /// <summary>
        /// Get the final city in the route.
        /// </summary>
        /// <returns>Final city in the route</returns>
        ICity GetFinalCity();

        /// <summary>
        /// Get city collection.
        /// </summary>
        /// <returns>City collection</returns>
        IEnumerable<ICity> GetCities();

        /// <summary>
        /// Get the final distance of the route.
        /// </summary>
        /// <returns>Final distance of the route</returns>
        int GetDistance();
    }
}
