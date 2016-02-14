// ---------------------------------------------------------------------------------
// ICT - ICT.Collections
// Link.cs
// DRNL
// 2016.02.08
// ---------------------------------------------------------------------------------

using ICT.Core.DBC;
using ICT.Models.Interfaces;
using System.Text;

namespace ICT.Models
{
    /// <summary>
    /// Implementation of the link class. A link entity represents a driving route
    /// between two cities.
    /// </summary>
    public class Link : ILink
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name">Name of the route</param>
        /// <param name="startCity">Start city</param>
        /// <param name="finalCity">Final city</param>
        /// <param name="distance">Distance</param>
        public Link(string name, ICity startCity, ICity finalCity, int distance)
        {
            // Precondition
            Contract.Requires(!string.IsNullOrEmpty(name), "Link name is valid");
            Contract.Requires(startCity != null, "Start city is valid");
            Contract.Requires(finalCity != null, "Final city is valid");
            Contract.Requires(distance > 0, "Distance is valid");

            // Initialize instance variables
            Name = name;
            StartCity = startCity;
            FinalCity = finalCity;
            Distance = distance;
        }

        /// <summary>
        /// Name of the route.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Start city.
        /// </summary>
        public ICity StartCity { get; set; }

        /// <summary>
        /// Final city.
        /// </summary>
        public ICity FinalCity { get; set; }

        /// <summary>
        /// Distance.
        /// </summary>
        public int Distance { get; set; }

        /// <summary>
        /// Compare to another object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            var other = obj as Link;

            if (other == null)
            {
                return -1;
            }

            var code = Name.CompareTo(other.Name);

            if (code != 0)
            {
                return code;
            }

            code = StartCity.CompareTo(other.StartCity);

            if (code != 0)
            {
                return code;
            }

            code = FinalCity.CompareTo(other.FinalCity);

            return code;
        }

        /// <summary>
        /// Generate/return the string-based information about the link.
        /// </summary>
        /// <returns>String-based information about the link</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append(string.Format("{0}{1}{2}", StartCity.Name, FinalCity.Name, Distance));

            return sb.ToString();
        }
    }
}
