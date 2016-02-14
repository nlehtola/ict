// ---------------------------------------------------------------------------------
// ICT - ICT.Models
// IPlanner.cs
// DRNL
// 2016.02.08
// ---------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ICT.Models.Interfaces
{
    /// <summary>
    /// Interface for the planner entity.
    /// </summary>
    public interface IPlanner
    {
        /// <summary>
        /// Setup the planner configuration. It uses a connector to get information
        /// about the cities and the links.
        /// </summary>
        /// <param name="connector"></param>
        void Setup(IConnector connector);

        /// <summary>
        /// Check whether a route exists or not.
        /// </summary>
        /// <returns>True if the route exists, false otherwise</returns>
        bool CheckRoute(IEnumerable<string> cityNames);

        /// <summary>
        /// Get the sequential route of a collection of cities. The route might 
        /// not exist and an exception will be thrown.
        /// </summary>
        /// <returns>Sequential route of a collection of cities</returns>
        IRoute GetRoute(IEnumerable<string> cityNames);

        /// <summary>
        /// Get the distance of a collection of cities. The route might 
        /// not exist and an exception will be thrown.
        /// </summary>
        /// <returns>Sequential route of a collection of cities</returns>
        int GetDistance(IEnumerable<string> cityNames);

        /// <summary>
        /// Get the shortest distance between two cities. The route might 
        /// not exist and an exception will be thrown.
        /// </summary>
        /// <param name="startCityName">Name of the start city</param>
        /// <param name="finalCityName">Name of the final city</param>
        /// <returns>Shortest distance between two cities</returns>
        int GetDistance(string startCityName, string finalCityName);

        /// <summary>
        /// Get the all the routes between two cities, which the distance is less
        /// than or equal to the target distance. The routes might not exist and 
        /// an exception will be thrown.
        /// </summary>
        /// <param name="startCityName">Name of the start city</param>
        /// <param name="finalCityName">Name of the final city</param>
        /// <param name="maxNumStops">Maximum number of stops</param>
        /// <param name="enforceExactStopCriteria">Enforce exact stop criteria</param>
        /// <returns>All the routes between two cities</returns>
        IEnumerable<IRoute> GetRoutes(string startCityName, string finalCityName, int maxNumStops, bool enforceExactStopCriteria);

        /// <summary>
        /// Get the all the routes between two cities, which the distance is less
        /// than or equal to the target distance. The routes might not exist and 
        /// an exception will be thrown.
        /// </summary>
        /// <param name="startCityName">Name of the start city</param>
        /// <param name="finalCityName">Name of the final city</param>
        /// <param name="targetDistance">Name of the final city</param>
        /// <returns>All the routes between two cities within range</returns>
        IEnumerable<IRoute> GetRoutesWithinRange(string startCityName, string finalCityName, int targetDistance);
    }
}
