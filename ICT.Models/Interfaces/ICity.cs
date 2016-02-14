// ---------------------------------------------------------------------------------
// ICT - ICT.Models
// ICity.cs
// DRNL
// 2016.02.08
// ---------------------------------------------------------------------------------

using System;

namespace ICT.Models.Interfaces
{
    /// <summary>
    /// Interface for the city entity.
    /// </summary>
    public interface ICity : IComparable
    {
        /// <summary>
        /// Name of the city.
        /// </summary>
        string Name { get; set; }
    }
}
