// ---------------------------------------------------------------------------------
// ICT - ICT.Models
// IConnector.cs
// DRNL
// 2016.02.08
// ---------------------------------------------------------------------------------

using System.Collections.Generic;
using System.ComponentModel;

namespace ICT.Models.Interfaces
{
    /// <summary>
    /// Interface for the connector entity. A connector establishes the data 
    /// communication between the planner and the application
    /// </summary>
    public interface IConnector : INotifyPropertyChanged
    {
        /// <summary>
        /// Get city collection.
        /// </summary>
        /// <returns>City collection</returns>
        IEnumerable<ICity> GetCities();

        /// <summary>
        /// Get link collection.
        /// </summary>
        /// <returns>Link collection</returns>
        IEnumerable<ILink> GetLinks();
    }
}
