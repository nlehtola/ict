// ---------------------------------------------------------------------------------
// ICT - ICT.Models
// IConnectorDataParser.cs
// DRNL
// 2016.02.08
// ---------------------------------------------------------------------------------

using System;

namespace ICT.Models.Interfaces
{
    /// <summary>
    /// Interface for the data connector parser.
    /// </summary>
    public interface IConnectorDataParser
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
        bool Parse(string data, Func<string, ICity> getCity, Func<string, ICity> addCity,
                                Func<string, ILink> getLink, Func<string, string, string, int, ILink> addLink);
    }
}
