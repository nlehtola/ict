// ---------------------------------------------------------------------------------
// ICT - ICT.Collections
// IVertex.cs
// DRNL
// 2016.02.08
// ---------------------------------------------------------------------------------

using System;

namespace ICT.Collections.Interfaces
{
    /// <summary>
    /// Interface for the edge entity (for the graph data structure).
    /// </summary>
    public interface IEdge<T>
        where T : IComparable<T>
    {
        /// <summary>
        /// Label associated with the edge.
        /// </summary>
        T Label { get; }

        /// <summary>
        /// Start vertex.
        /// </summary>
        IVertex<T> StartVertex { get; }

        /// <summary>
        /// End vertex.
        /// </summary>
        IVertex<T> FinalVertex { get; }

        /// <summary>
        /// Weight associated to the edge.
        /// </summary>
        int Weight { get; }
    }
}
