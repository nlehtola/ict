// ---------------------------------------------------------------------------------
// ICT - ICT.Models
// IConnector.cs
// DRNL
// 2016.02.08
// ---------------------------------------------------------------------------------

namespace ICT.Models.Interfaces
{
    /// <summary>
    /// Interface for the connector entity. A connector establishes the data 
    /// communication between the planner and the application
    /// </summary>
    public interface IStandardConnector : IConnector
    {
        /// <summary>
        /// Update the connector data.
        /// </summary>
        /// <param name="data">Data source for the connector</param>
        /// <param name="parser">Data connector parser</param>
        void Update(string data, IConnectorDataParser parser);

        /// <summary>
        /// Reset the connector data.
        /// </summary>
        void Reset();
    }
}
