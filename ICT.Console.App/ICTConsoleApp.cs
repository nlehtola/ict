// ---------------------------------------------------------------------------------
// ICT - ICT.Console.App
// ICTConsoleApp.cs
// DRNL
// 2016.02.08
// ---------------------------------------------------------------------------------

using ICT.Console.App.Interfaces;
using ICT.Console.App.Properties;
using ICT.Core.Exceptions;
using ICT.Core.Extensions;
using ICT.Core.Interfaces;
using ICT.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ICT.Console.App
{
    /// <summary>
    /// ICT Console application class
    /// </summary>
    internal class ICTConsoleApp : IExecutable, IComposite
    {
        /// <summary>
        /// Static constructor
        /// </summary>
        static ICTConsoleApp()
        {
            // Initialize static members
            DefaultConnectorData = Resources.ICTDefaultConnectorData;
        }

        public ICTConsoleApp()
        {
            // Initialize instance variables
            ConnectorData = DefaultConnectorData;

            Commands = new List<Action>();
            Commands.Add(() => DummyCommand());
            Commands.Add(() => PrintCityInfoCommand());
            Commands.Add(() => PrintLinkInfoCommand());
            Commands.Add(() => DefaultConnectorDataCommand());
            Commands.Add(() => ResetConnectorDataCommand());
            Commands.Add(() => AddNewLinkCommand());
            Commands.Add(() => CheckRouteCommand());
            Commands.Add(() => CalculateRouteDistanceCommand());
            Commands.Add(() => GetMinimumDistanceCommand());
            Commands.Add(() => GetRoutesMaximumNoStopsCommand());
            Commands.Add(() => GetRoutesExactNoStopsCommand());
            Commands.Add(() => GetRoutesWithinMaximumDistanceCommand());

            MenuOptions = new List<string>()
            {
                "  [1 ] List the current cities",
                "  [2 ] List the current links",
                "  [3 ] Reset to the default data set",
                "  [4 ] Clear data set",
                "  [5 ] Add new link",
                "  [6 ] Check route",
                "  [7 ] Calculate route distance",
                "  [8 ] Get minimum distance between two cities",
                "  [9 ] Get all routes (maximum no. of stops)",
                "  [10] Get all routes (exact no. of stops)",
                "  [11] Get all routes (within maximum distance)",
                "  [0 ] Quit"
            };
        }
        
        /// <summary>
        /// Standard IO component
        /// </summary>
        [Import(typeof(IStandardIO))]
        private IStandardIO StandardIO { get; set; }

        /// <summary>
        /// Connector component
        /// </summary>
        [Import(typeof(IStandardConnector))]
        private IStandardConnector Connector { get; set; }

        /// <summary>
        /// Planner component
        /// </summary>
        [Import(typeof(IPlanner))]
        private IPlanner Planner { get; set; }

        /// <summary>
        /// Menu options
        /// </summary>
        private List<string> MenuOptions { get; set; }

        /// <summary>
        /// Default connector data.
        /// </summary>
        private static string DefaultConnectorData { get; set; }

        /// <summary>
        /// Current connector data.
        /// </summary>
        private string ConnectorData { get; set; }

        /// <summary>
        /// Commands
        /// </summary>
        private List<Action> Commands { get; set; }

        /// <summary>
        /// Run the console application
        /// </summary>
        public void Execute()
        {
            // Open
            Open();

            // Start the application
            Start();

            // Run the application cycle(s)
            Run();

            // Stop the application
            Stop();

            // Close
            Close();
        }

        #region PrivateMembers

        /// <summary>
        /// Open/initialize the application environment/context.
        /// </summary>
        private void Open()
        {
            // Check for the parts
            if (Planner == null)
            {
                this.ResolveParts();
            }

            try
            {
                // Planner setup
                Planner.Setup(Connector);

                // Connector update
                Connector.Update(ConnectorData, new ICTDataConnectorParser());
            }
            catch (Exception ex)
            {
                // Report the problem...
                StandardIO.WriteLine(ex.Message);

                // Force to terminate...
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// Start the application.
        /// </summary>
        private void Start()
        {
            // Welcome message
            StandardIO.WriteLine(Resources.ICTApplicationWelcomeMessage);
            StandardIO.WriteLine(Resources.ICTApplicationUnderWelcomeMessage);
            StandardIO.WriteLine();
        }

        /// <summary>
        /// Run the application cycle(s).
        /// </summary>
        private void Run()
        {
            var option = 1;

            do
            {
                // Print menu
                PrintMenu();

                // Get option
                option = GetOption();

                // Select an action...
                Commands[option]();

            } while (option != 0);

            // Run...
            StandardIO.WriteLine();
        }

        /// <summary>
        /// Stop the application.
        /// </summary>
        private void Stop()
        {
            // Farewell message
            StandardIO.WriteLine();
            StandardIO.WriteLine(Resources.ICTPressAnyKeyMessage);
            StandardIO.ReadLine();
            StandardIO.WriteLine();
        }

        /// <summary>
        /// Close/reset the application environment/context.
        /// </summary>
        private void Close()
        {

        }

        /// <summary>
        /// Return the list of all cities in a string.
        /// </summary>
        /// <returns>List of all cities in a string</returns>
        private string CityInfo()
        {
            var cities = Connector.GetCities();

            if (cities.Count() == 0)
            {
                return Resources.ICTEmptyCollectionMessage;
            }

            var sb = new StringBuilder();

            foreach (var city in cities)
            {
                sb.Append(string.Format("{0} ", city.Name));
            }

            return sb.ToString();
        }

        /// <summary>
        /// Return the list of all links in a string.
        /// </summary>
        /// <returns>List of all links in a string</returns>
        private string LinkInfo()
        {
            var links = Connector.GetLinks();

            if (links.Count() == 0)
            {
                return Resources.ICTEmptyCollectionMessage;
            }

            var sb = new StringBuilder();

            foreach (var link in links)
            {
                sb.Append(string.Format("{0} ", link.Name));
            }

            return sb.ToString();
        }

        /// <summary>
        /// Print menu options
        /// </summary>
        private void PrintMenu()
        {
            // Menu
            StandardIO.WriteLine(Resources.ICTSelectOptionMessage);
            StandardIO.WriteLine();
            MenuOptions.ForEach(o => StandardIO.WriteLine(o));
            StandardIO.WriteLine();
        }

        /// <summary>
        /// Get option
        /// </summary>
        private int GetOption()
        {
            var option = -1;

            do
            {
                StandardIO.Write(SpaceIt(Resources.ICTEnterOptionMessage));

                var line = StandardIO.ReadLine();

                if (!int.TryParse(line, out option))
                {
                    option = -1;
                }

            } while (option < 0 || option > MenuOptions.Count);

            return option;
        }

        /// <summary>
        /// Check new link definition
        /// </summary>
        /// <param name="definition">Link definition</param>
        private void CheckLinkDefinition(string definition)
        {
            // Valid string?
            if (string.IsNullOrEmpty(definition))
            {
                throw new CoreException(Resources.ICTInvalidLinkDefinitionMessage);
            }

            // Match it with the XX# format
            var regex = @"^[A-Z]{1}[A-Z]{1}\d*$";

            if (!Regex.IsMatch(definition, regex, RegexOptions.IgnoreCase))
            {
                throw new CoreException(Resources.ICTInvalidLinkDefinitionMessage);
            }

            // Parse data
            var linkName = definition;
            var startCityName = definition.Substring(0, 1);
            var finalCityName = definition.Substring(1, 1);
            var strDistance = definition.Substring(2);
            var distance = 0;

            if (!int.TryParse(strDistance, out distance))
            {
                distance = 0;
            }

            // Adjust it...
            definition = string.Format("{0}{1}{2}", startCityName.ToUpper(), finalCityName.ToUpper(), distance);

            // Verify it
            var cityLink = string.Format("{0}{1}", startCityName.ToUpper(), finalCityName.ToUpper());

            // Self-reference?
            if (startCityName.Equals(finalCityName, StringComparison.OrdinalIgnoreCase))
            {
                throw new CoreException(string.Format(Resources.ICTSelfReferencingLinksMessage, cityLink));
            }

            // Do we have it?
            if (ConnectorData.Contains(cityLink))
            {
                throw new CoreException(string.Format(Resources.ICTLinkAlreadyExistsMessage, cityLink));
            }
        }

        /// <summary>
        /// Get new link definition
        /// </summary>
        private string GetNewLinkDefinition()
        {
            // Get link definition
            StandardIO.Write(SpaceIt(Resources.ICTEnterLinkDefinitionMessage));

            var definition = StandardIO.ReadLine();

            CheckLinkDefinition(definition);

            return definition;
        }

        /// <summary>
        /// Get route definition as collection.
        /// </summary>
        /// <param name="definition">Route definition</param>
        /// <param name="maxCities">Maximum number of cities to be used</param>
        private IEnumerable<string> GetRouteDefinitionAsCollection(string definition, int maxCities = -1)
        {
            // Valid string?
            if (string.IsNullOrEmpty(definition))
            {
                throw new CoreException(Resources.ICTInvalidRouteDefinitionMessage);
            }

            // Check the city names...
            var tokens = definition.Split();

            if (tokens.Length == 0)
            {
                throw new CoreException(Resources.ICTInvalidRouteDefinitionMessage);
            }

            // Check the maximum number of cities to be used
            if (maxCities > 0)
            {
                if (tokens.Length > maxCities)
                {
                    throw new CoreException(Resources.ICTInvalidRouteDefinitionMessage);
                }
            }

            // Do it...
            foreach (var token in tokens)
            {
                // Match it with the X format
                var regex = @"^[A-Z]{1}$";

                if (!Regex.IsMatch(token, regex, RegexOptions.IgnoreCase))
                {
                    throw new CoreException(Resources.ICTInvalidRouteDefinitionMessage);
                }
            }

            return new List<string>(tokens);
        }

        /// <summary>
        /// Get route definition. Space separated city names.
        /// </summary>
        /// <param name="message">Message to be displayed to the user</param>
        /// <param name="maxCities">Maximum number of cities to be used</param>
        /// <returns>Route definition</returns>
        private IEnumerable<string> GetRouteDefinition(string message, int maxCities = -1)
        {
            // Get link definition
            StandardIO.Write(SpaceIt(message));

            var basicDefinition = StandardIO.ReadLine();

            var definition = GetRouteDefinitionAsCollection(basicDefinition, maxCities);

            return definition;
        }

        /// <summary>
        /// Get route limitation.
        /// </summary>
        /// <returns>Route limitation</returns>
        private int GetRouteLimitDefinition(string message)
        {
            // Get route limitation definition
            StandardIO.Write(SpaceIt(message));

            var definition = StandardIO.ReadLine();

            // Match it with the XX# format
            var regex = @"^\d*$";

            if (!Regex.IsMatch(definition, regex, RegexOptions.IgnoreCase))
            {
                throw new CoreException(Resources.ICTInvalidRouteLimitationDefinitionMessage);
            }

            var limit = 0;

            if (!int.TryParse(definition, out limit))
            {
                throw new CoreException(Resources.ICTInvalidRouteLimitationDefinitionMessage);
            }

            if (limit <= 0)
            {
                throw new CoreException(Resources.ICTInvalidRouteLimitationDefinitionMessage);
            }

            return limit;
        }
        
        /// <summary>
        /// Dummy command
        /// </summary>
        private void DummyCommand()
        {
            // ...
        }

        /// <summary>
        /// Print city info command
        /// </summary>
        private void PrintCityInfoCommand()
        {
            // Print current cities
            PrintResult(string.Format(Resources.ICTCurrentCitiesMessage, CityInfo()));
        }

        /// <summary>
        /// Print link info command
        /// </summary>
        private void PrintLinkInfoCommand()
        {
            // Print current links
            PrintResult(string.Format(Resources.ICTCurrentLinksMessage, LinkInfo()));
        }

        /// <summary>
        /// Reset connector data command
        /// </summary>
        private void ResetConnectorDataCommand()
        {
            // Clear connector data
            ConnectorData = string.Empty;

            // Connector reset 
            Connector.Reset();

            // Print current links
            PrintResult(string.Format(Resources.ICTCurrentLinksMessage, LinkInfo()));
        }

        /// <summary>
        /// Default connector data command
        /// </summary>
        private void DefaultConnectorDataCommand()
        {
            // Reset connector data
            ConnectorData = DefaultConnectorData;

            // Connector update
            Connector.Update(ConnectorData, new ICTDataConnectorParser());

            // Print current links
            PrintResult(string.Format(Resources.ICTCurrentLinksMessage, LinkInfo()));
        }

        /// <summary>
        /// Add new link command
        /// </summary>
        private void AddNewLinkCommand()
        {
            try 
            {
                // Get new link definition
                var linkSpecs = GetNewLinkDefinition();

                // Connector update
                var connectorData = ConnectorData + " " + linkSpecs;

                Connector.Update(connectorData, new ICTDataConnectorParser());
                ConnectorData = connectorData;

                // Print current links
                PrintResult(string.Format(Resources.ICTCurrentLinksMessage, LinkInfo()));
            }
            catch (Exception ex)
            {
                // Report the problem...
                PrintResult(ex.Message);
            }
        }

        /// <summary>
        /// Check route command
        /// </summary>
        private void CheckRouteCommand()
        {
            try 
            {
                // Get route
                var definition = GetRouteDefinition(Resources.ICTEnterRouteDefinitionMessage);

                // Check route with the planner
                var result = Planner.CheckRoute(definition) ?
                    string.Format(
                        Resources.ICTRouteIsAvailableMessage, 
                        definition.Aggregate((i, j) => i + "-" + j)) :
                        Resources.ICTRouteIsNotAvailableMessage;

                PrintResult(result);
            }
            catch
            {
                // Report the problem...
                PrintResult(Resources.ICTRouteIsNotAvailableMessage);
            }
        }

        /// <summary>
        /// Calculate route distance command
        /// </summary>
        private void CalculateRouteDistanceCommand()
        {
            try
            {
                // Get route
                var definition = GetRouteDefinition(Resources.ICTEnterRouteDefinitionMessage);

                // Calculate the route distance with the planner
                var distance = Planner.GetDistance(definition);
                var result = string.Format(Resources.ICTRouteDistanceMessage, distance);

                PrintResult(result);
            }
            catch
            {
                // Report the problem...
                PrintResult(Resources.ICTRouteIsNotAvailableMessage);
            }
        }

        /// <summary>
        /// Get minimum distance command
        /// </summary>
        private void GetMinimumDistanceCommand()
        {
            try 
            {
                // Get route end points
                var definition = GetRouteDefinition(Resources.ICTEnterTargetCitiesMessage, 2);

                // Check route with the planner
                var distance = Planner.GetDistance(definition.ElementAt(0), definition.ElementAt(1));
                var result = string.Format(Resources.ICTMinimumDistanceMessage, distance);

                PrintResult(result);
            }
            catch
            {
                // Report the problem...
                PrintResult(Resources.ICTRouteIsNotAvailableMessage);
            }
        }

        /// <summary>
        /// Get all routes (maximum no. of stops) command.
        /// </summary>
        private void GetRoutesMaximumNoStopsCommand()
        {
            try 
            {
                // Get route end points and number of stops
                var definition = GetRouteDefinition(Resources.ICTEnterTargetCitiesMessage, 2);
                var maxNumStops = GetRouteLimitDefinition(Resources.ICTEnterMaxNumStopsMessage);

                // Check routes with the planner
                var routes = Planner.GetRoutes(definition.ElementAt(0), definition.ElementAt(1), maxNumStops, false);
                var routeInfo = string.Empty;

                foreach (var route in routes)
                {
                    var names = route.GetCities().Select(c => c.Name);

                    routeInfo += names.Aggregate((i, j) => i + "-" + j) + " ";
                }

                var result = string.Format(Resources.ICTAvailableRoutesMessage, routeInfo);

                PrintResult(result);
            }
            catch
            {
                // Report the problem...
                PrintResult(Resources.ICTRoutesAreNotAvailableMessage);
            }
        }

        /// <summary>
        /// Get all routes (exact no. of stops) command.
        /// </summary>
        private void GetRoutesExactNoStopsCommand()
        {
            try
            {
                // Get route end points and number of stops
                var definition = GetRouteDefinition(Resources.ICTEnterTargetCitiesMessage, 2);
                var maxNumStops = GetRouteLimitDefinition(Resources.ICTEnterMaxNumStopsMessage);

                // Check route with the planner
                var routes = Planner.GetRoutes(definition.ElementAt(0), definition.ElementAt(1), maxNumStops, true);
                var routeInfo = string.Empty;

                foreach (var route in routes)
                {
                    var names = route.GetCities().Select(c => c.Name);

                    routeInfo += names.Aggregate((i, j) => i + "-" + j) + " ";
                }

                var result = string.Format(Resources.ICTAvailableRoutesMessage, routeInfo);

                PrintResult(result);
            }
            catch
            {
                // Report the problem...
                PrintResult(Resources.ICTRoutesAreNotAvailableMessage);
            }
        }

        /// <summary>
        /// Get all routes (within maximum distance) command.
        /// </summary>
        private void GetRoutesWithinMaximumDistanceCommand()
        {
            try
            {
                // Get route end points and number of stops
                var definition = GetRouteDefinition(Resources.ICTEnterTargetCitiesMessage, 2);
                var maxDistance = GetRouteLimitDefinition(Resources.ICTEnterMaxDistanceMessage);

                // Check routes with the planner
                var routes = Planner.GetRoutesWithinRange(definition.ElementAt(0), definition.ElementAt(1), maxDistance);
                var routeInfo = string.Empty;

                foreach (var route in routes)
                {
                    var names = route.GetCities().Select(c => c.Name);

                    routeInfo += names.Aggregate((i, j) => i + "-" + j) + " ";
                }

                var result = string.Format(Resources.ICTAvailableRoutesMessage, routeInfo);

                PrintResult(result);
            }
            catch
            {
                // Report the problem...
                PrintResult(Resources.ICTRoutesAreNotAvailableMessage);
            }
        }

        /// <summary>
        /// Print result on the screen
        /// </summary>
        /// <param name="result">Result to be printed</param>
        private void PrintResult(string result)
        {
            // Print result
            StandardIO.WriteLine();
            StandardIO.WriteLine("  => " + result);
            StandardIO.WriteLine();
        }

        /// <summary>
        /// Space it!
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        private string SpaceIt(string source)
        {
            return source + " ";
        }

        #endregion
    }
}
