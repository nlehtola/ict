// ---------------------------------------------------------------------------------
// ICT - ICT.Models
// ConnectorMessage.cs
// DRNL
// 2016.02.08
// ---------------------------------------------------------------------------------

using System;

namespace ICT.Models.Connectors
{
    /// <summary>
    /// Connector message class
    /// </summary>
    internal static class ConnectorMessage
    {
        /// <summary>
        /// Constructor
        /// </summary>
        static ConnectorMessage()
        {
            // Initialize instance variables
            UpdateData = "UpdateData";
            ResetData = "ResetData";
        }

        /// <summary>
        /// Is the message?
        /// </summary>
        public static bool Equal(string reference, string target)
        {
            return reference.Equals(target, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Update data
        /// </summary>
        public static string UpdateData { get; private set; }

        /// <summary>
        /// Reset data
        /// </summary>
        public static string ResetData { get; private set; }
    }
}
