// ---------------------------------------------------------------------------------
// ICT - ICT.Console.App
// ICTDataConnectorParser.cs
// DRNL
// 2016.02.08
// ---------------------------------------------------------------------------------

using ICT.Core.Exceptions;
using ICT.Models;
using ICT.Models.Interfaces;
using System;

namespace ICT.Console.App
{
    /// <summary>
    /// ICT data connector parser class.
    /// </summary>
    internal class ICTDataConnectorParser : IConnectorDataParser
    {
        /// <summary>
        /// Parse the connector data.
        /// </summary>
        /// <param name="data">Connector data</param>
        /// <param name="getCity">Get city delegate</param>
        /// <param name="addCity">Add city delegate</param>
        /// <param name="getLink">Get link delegate</param>
        /// <param name="addLink">Add city delegate</param>
        /// <returns>True in case of success, otherwise false</returns>
        public bool Parse(string data, Func<string, ICity> getCity, Func<string, ICity> addCity,
                                       Func<string, ILink> getLink, Func<string, string, string, int, ILink> addLink)
        {
            try
            {
                // Parse the data
                var tokens = data.Split();

                if (tokens == null || tokens.Length == 0)
                {
                    return false;
                }

                // Traverse the collection
                foreach (var token in tokens)
                {
                    // Format CC#
                    var linkName = token;
                    var startCityName = token.Substring(0, 1);
                    var finalCityName = token.Substring(1, 1);
                    var strDistance = token.Substring(2);
                    var distance = 0;

                    if (!int.TryParse(strDistance, out distance))
                    {
                        distance = 0;
                    }

                    // Add a city
                    var startCity = getCity(startCityName);

                    if (startCity == null)
                    {
                        startCity = addCity(startCityName);
                    }

                    // ..., and again!
                    var finalCity = getCity(finalCityName);

                    if (finalCity == null)
                    {
                        finalCity = addCity(finalCityName);
                    }

                    var link = getLink(linkName);

                    if (link == null)
                    {
                        addLink(linkName, startCityName, finalCityName, distance);
                    }
                }

                return true;
            }
            catch
            {
                throw new CoreException("Invalid data source");
            }
        }
    }
}
