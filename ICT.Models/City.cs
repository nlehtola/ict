// ---------------------------------------------------------------------------------
// ICT - ICT.Collections
// City.cs
// DRNL
// 2016.02.08
// ---------------------------------------------------------------------------------

using ICT.Core.DBC;
using ICT.Models.Interfaces;

namespace ICT.Models
{
    /// <summary>
    /// Implementation of the city class.
    /// </summary>
    public class City : ICity
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name">Name of the city</param>
        public City(string name)
        {
            // Precondition
            Contract.Requires(!string.IsNullOrEmpty(name), "City name is valid");

            // Initialize instance variables
            Name = name;
        }

        /// <summary>
        /// Name of the city.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Compare to another object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            var other = obj as City;

            if (other == null)
            {
                return -1;
            }

            return Name.CompareTo(other.Name);
        }

        /// <summary>
        /// Generate/return the string-based information about the city.
        /// </summary>
        /// <returns>String-based information about the city</returns>
        public override string ToString()
        {
            return Name;
        }
    }
}
