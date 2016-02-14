// ---------------------------------------------------------------------------------
// ICT - ICT.Collections
// Planner.cs
// DRNL
// 2016.02.08
// ---------------------------------------------------------------------------------

using ICT.Collections;
using ICT.Collections.Algorithms;
using ICT.Collections.Interfaces;
using ICT.Core.DBC;
using ICT.Core.Exceptions;
using ICT.Models.Connectors;
using ICT.Models.Interfaces;
using ICT.Models.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;

namespace ICT.Models
{
    /// <summary>
    /// Implementation of the planner class.
    /// </summary>
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Export(typeof(IPlanner))]
    public class Planner : IPlanner
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public Planner()
        {
            // Initialize instance variables
            Cities = new List<ICity>();
            Links = new List<ILink>();
            Network = new Graph<string>();
        }

        /// <summary>
        /// Connector
        /// </summary>
        private IConnector Connector { get; set; }

        /// <summary>
        /// City collection.
        /// </summary>
        private List<ICity> Cities { get; set; }

        /// <summary>
        /// Link collection.
        /// </summary>
        private List<ILink> Links { get; set; }

       /// <summary>
       /// City inter-communication network.
       /// </summary>
        private IGraph<string> Network { get; set; }

        /// <summary>
        /// Setup the planner configuration. It uses a connector to get information
        /// about the cities and the links.
        /// </summary>
        /// <param name="connector"></param>
        public void Setup(IConnector connector)
        {
            // Precondition
            Contract.Requires(connector != null, "Valid connector");

            // Setup the connector
            Connector = connector;
            Connector.PropertyChanged += ConnectorPropertyChanged;
        }

        /// <summary>
        /// Check whether a route exists or not.
        /// </summary>
        /// <returns>True if the route exists, false otherwise</returns>
        public bool CheckRoute(IEnumerable<string> cityNames)
        {
            try
            {
                // Check the route
                var route = GetRoute(cityNames);

                return route != null;
            }
            catch 
            {
                return false;
            }
        }

        /// <summary>
        /// Get the sequential route of a collection of cities. The route might 
        /// not exist and an exception will be thrown.
        /// </summary>
        /// <returns>Sequential route of a collection of cities</returns>
        public IRoute GetRoute(IEnumerable<string> cityNames)
        {
            try
            {
                // Get the path
                var path = Network.GetPath(cityNames);

                // Create the route
                var route = new Route();

                // Set the route
                SetupRoute(route, path);

                return route;
            }
            catch
            {
                // Route doesn't exist
                throw new CoreException(Resources.ICTRouteDoesNotExist);
            }
        }

        /// <summary>
        /// Get the distance of a collection of cities. The route might 
        /// not exist and an exception will be thrown.
        /// </summary>
        /// <returns>Sequential route of a collection of cities</returns>
        public int GetDistance(IEnumerable<string> cityNames)
        {
            try
            {
                // Get the distance
                var distance = Network.GetWeight(cityNames);

                return distance;
            }
            catch
            {
                // Route doesn't exist
                throw new CoreException(Resources.ICTRouteDoesNotExist);
            }
        }

        /// <summary>
        /// Get the shortest distance between two cities. The route might 
        /// not exist and an exception will be thrown.
        /// </summary>
        /// <param name="startCityName">Name of the start city</param>
        /// <param name="finalCityName">Name of the final city</param>
        /// <returns>Shortest distance between two cities</returns>
        public int GetDistance(string startCityName, string finalCityName)
        {
            try
            {
                // Get the shortest distance
                var distance = Network.GetWeight(startCityName, finalCityName);

                return distance;
            }
            catch
            {
                // Route doesn't exist
                throw new CoreException(Resources.ICTRouteDoesNotExist);
            }
        }

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
        public IEnumerable<IRoute> GetRoutes(string startCityName, string finalCityName, int maxNumStops, bool enforceExactStopCriteria)
        {
            try
            {
                // Get the paths with imposed limitation
                var paths = !enforceExactStopCriteria ? 
                    Network.GetPaths(startCityName, finalCityName, maxNumStops) :
                    Network.GetPaths(startCityName, finalCityName, maxNumStops, true);

                // Create the routes
                var routes = new List<IRoute>();

                foreach (var path in paths)
                {
                    // Create the route
                    var route = new Route();

                    // Set the route
                    SetupRoute(route, path);

                    routes.Add(route);
                }

                return routes;
            }
            catch
            {
                // Routes don't exist
                throw new CoreException(Resources.ICTRoutesDoNotExist);
            }
        }

        /// <summary>
        /// Get the all the routes between two cities, which the distance is less
        /// than or equal to the target distance. The routes might not exist and 
        /// an exception will be thrown.
        /// </summary>
        /// <param name="startCityName">Name of the start city</param>
        /// <param name="finalCityName">Name of the final city</param>
        /// <param name="targetDistance">Name of the final city</param>
        /// <returns>All the routes between two cities within range</returns>
        public IEnumerable<IRoute> GetRoutesWithinRange(string startCityName, string finalCityName, int targetDistance)
        {
            try
            {
                // Get the paths with imposed limitation
                var paths = Network.GetPathsWithinRange(startCityName, finalCityName, targetDistance);

                // Create the routes
                var routes = new List<IRoute>();

                foreach (var path in paths)
                {
                    // Create the route
                    var route = new Route();

                    // Set the route
                    SetupRoute(route, path);

                    routes.Add(route);
                }

                return routes;
            }
            catch
            {
                // Routes don't exist
                throw new CoreException(Resources.ICTRoutesDoNotExist);
            }
        }

        /// <summary>
        /// Event handler
        /// </summary>
        /// <param name="sender">Connector entity</param>
        /// <param name="e">Property event</param>
        private void ConnectorPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var connector = sender as IConnector;

            if (connector == null)
            {
                return;
            }

            // Check the property and invoke the callback
            var propertyName = e.PropertyName;

            if (ConnectorMessage.Equal(propertyName, ConnectorMessage.UpdateData))
            {
                UpdateDataCallback();
            }
            else if (ConnectorMessage.Equal(propertyName, ConnectorMessage.ResetData))
            {
                ResetDataCallback();
            }
        }

        #region PrivateMemmbers

        /// <summary>
        /// Callback for the update data event (connector)
        /// </summary>
        private void UpdateDataCallback()
        {
            // Precondition
            var cities = Connector.GetCities();
            var links = Connector.GetLinks();

            Contract.Requires(cities != null, "Valid city collection");
            Contract.Requires(links != null, "Valid city collection");

            // Clear containers and network
            Cities.Clear();
            Links.Clear();
            Network.Clear();

            // Check containers...
            if (cities.Count() == 0 || links.Count() == 0)
            {
                return;
            }

            // Copy values
            Cities.AddRange(cities);
            Links.AddRange(links);

            // Checks...
            try
            {
                // Validate the topology
                ValidateTopology();

                // Setup the graph data structure for the algorithmic solutions
                SetupNetwork();
            }
            catch (Exception ex)
            {
                // Clear containers
                Cities.Clear();
                Links.Clear();

                // Re-throw it...
                throw new CoreException(ex.Message);
            }
        }

        /// <summary>
        /// Callback for the reset data event (connector)
        /// </summary>
        private void ResetDataCallback()
        {
            // Clear containers and network
            Cities.Clear();
            Links.Clear();
            Network.Clear();
        }

        /// <summary>
        /// Validate the topology of the city inter-communication system
        /// </summary>
        private void ValidateTopology()
        {
            // No city is repeated
            CheckRepeatedCities();

            // No link is repeated
            CheckRepeatedLinks();

            // No city is isolated
            CheckIsolatedCities();

            // No city links to itself
            CheckSelfReferencedCities();
        }

        /// <summary>
        // Setup the graph data structure for the algorithmic solutions
        /// </summary>
        private void SetupNetwork()
        {
            // Clear network
            Network.Clear();

            // Add vertices
            Cities.ForEach(c => Network.AddVertex(c.Name));

            // Add edges
            Links.ForEach(l =>
            {
                var startCityName = l.StartCity.Name;
                var finalCityName = l.FinalCity.Name;
                var distance = l.Distance;
                var label = string.Format("{0}-{1}", startCityName, finalCityName);

                Network.AddEdge(label, startCityName, finalCityName, distance);
            });
        }

        /// <summary>
        /// Check for repeated cities
        /// </summary>
        private void CheckRepeatedCities()
        {
            var cities = Cities.ToList();

            // No city is repeated
            Cities.ForEach(c =>
            {
                var citySet = cities.Where(x => x.CompareTo(c) == 0);

                // There will be at least one...
                if (citySet.Count() > 1)
                {
                    throw new CoreException("Invalid model: There are repeated cities");
                }
            });
        }

        /// <summary>
        /// Check for repeated links
        /// </summary>
        private void CheckRepeatedLinks()
        {
            var links = Links.ToList();

            // No city is repeated
            Links.ForEach(c =>
            {
                var linkSet = links.Where(x => x.CompareTo(c) == 0);

                // There will be at least one...
                if (linkSet.Count() > 1)
                {
                    throw new CoreException("Invalid model: There are repeated links");
                }
            });
        }

        /// <summary>
        /// Check for isolated cities
        /// </summary>
        private void CheckIsolatedCities()
        {
            // No city is isolated
            Cities.ForEach(c =>
            {
                var link = Links.FirstOrDefault(l => l.StartCity.Name.Equals(c.Name) || 
                                                     l.FinalCity.Name.Equals(c.Name));

                if (link == null)
                {
                    throw new CoreException("Invalid model: There are isolated cities");
                }
            });
        }

        /// <summary>
        /// Check for self-referenced cities
        /// </summary>
        private void CheckSelfReferencedCities()
        {
            // No city is self-referenced
            Links.ForEach(l =>
            {
                if (l.StartCity.CompareTo(l.FinalCity) == 0)
                {
                    throw new CoreException("Invalid model: There are self-referenced cities");
                }
            });
        }

        /// <summary>
        /// Get the link associated to the given cities.
        /// </summary>
        /// <param name="startCity">Start city</param>
        /// <param name="finalCity">Final city</param>
        /// <returns>Link associated to the given cities</returns>
        private ILink GetLink(string startCity, string finalCity)
        {
            try
            {
                // Get the link
                var link = Links.FirstOrDefault(l =>
                    l.StartCity.Name.CompareTo(startCity) == 0 &&
                    l.FinalCity.Name.CompareTo(finalCity) == 0);

                return link;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Get the city associated to the given name.
        /// </summary>
        /// <param name="cityName">City name</param>
        /// <returns>City associated to the given name</returns>
        private ICity GetCIty(string cityName)
        {
            try
            {
                // Get the city
                var city = Cities.FirstOrDefault(c => c.Name.CompareTo(cityName) == 0);

                return city;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Set the configuration of the route based on a graph path.
        /// </summary>
        /// <param name="route">Target route</param>
        /// <param name="path">Graph path</param>
        private void SetupRoute(IRoute route, IPath<string> path)
        {
            // Precondition
            Contract.Requires(path != null, "Valid path (!= null)");
            var edges = path.GetEdges();
            Contract.Requires(edges.Count() > 0, "Valid path (not empty)");

            // Set the route
            foreach (var edge in edges)
            {
                // Get link
                var link = GetLink(edge.StartVertex.Label, edge.FinalVertex.Label);

                // Check link...
                Contract.Assert(link != null, "Valid link");

                // Setup route
                route.AddLink(link);
            }
        }

        #endregion
    }
}
