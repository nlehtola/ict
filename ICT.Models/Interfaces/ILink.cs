// ---------------------------------------------------------------------------------
// ICT - ICT.Models
// ILink.cs
// DRNL
// 2016.02.08
// ---------------------------------------------------------------------------------

using System;

namespace ICT.Models.Interfaces
{
    /// <summary>
    /// Interface for the link entity. A link entity represents a driving route
    /// between two cities.
    /// </summary>
    public interface ILink : IComparable
    {
        /// <summary>
        /// Name of the route.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Start city.
        /// </summary>
        ICity StartCity { get; set; }

        /// <summary>
        /// Final city.
        /// </summary>
        ICity FinalCity { get; set; }

        /// <summary>
        /// Distance.
        /// </summary>
        int Distance { get; set; }
    }
}
