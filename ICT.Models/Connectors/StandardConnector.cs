// ---------------------------------------------------------------------------------
// ICT - ICT.Models
// StandardConnector.cs
// DRNL
// 2016.02.08
// ---------------------------------------------------------------------------------

using ICT.Core.Exceptions;
using ICT.Models.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;

namespace ICT.Models.Connectors
{
    /// <summary>
    /// Standard connector class.
    /// </summary>
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Export(typeof(IStandardConnector))]
    class StandardConnector : IStandardConnector
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public StandardConnector()
        {
            // Initialize instance variables
            Cities = new List<ICity>();
            Links = new List<ILink>();
        }

        /// <summary>
        /// City collection.
        /// </summary>
        private List<ICity> Cities { get; set; }

        /// <summary>
        /// Link collection.
        /// </summary>
        private List<ILink> Links { get; set; }

        /// <summary>
        /// Event handler
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Get city collection.
        /// </summary>
        /// <returns>City collection</returns>
        public IEnumerable<ICity> GetCities()
        {
            // Return the collection
            var cities = new List<ICity>(Cities);

            return cities;
        }

        /// <summary>
        /// Get link collection.
        /// </summary>
        /// <returns>Link collection</returns>
        public IEnumerable<ILink> GetLinks()
        {
            // Return the collection
            var links = new List<ILink>(Links);

            return links;
        }

        /// <summary>
        /// Update the connector data.
        /// </summary>
        /// <param name="data">Data source for the connector</param>
        /// <param name="parser">Data connector parser</param>
        public void Update(string data, IConnectorDataParser parser)
        {
            // Clear containers
            Cities.Clear();
            Links.Clear();

            // Parse the data
            if (parser == null)
            {
                throw new CoreException("Invalid data connector parser");
            }

            try
            {
                if (!parser.Parse(data, x => GetCity(x), x => AddCity(x), x => GetLink(x), (x, y, z, w) => AddLink(x, y, z, w)))
                {
                    // Clear containers
                    Cities.Clear();
                    Links.Clear();
                }
            }
            catch
            {
                throw new CoreException("Invalid data source");
            }

            // Notify changes
            NotifyPropertyChanged(ConnectorMessage.UpdateData);
        }

        /// <summary>
        /// Reset the connector data.
        /// </summary>
        public void Reset()
        {
            // Clear containers
            Cities.Clear();
            Links.Clear();

            // Notify changes
            NotifyPropertyChanged(ConnectorMessage.ResetData);
        }

        /// <summary>
        /// Notify property changess
        /// </summary>
        /// <param name="info"></param>
        protected void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged == null)
            {
                return;
            }

            var args = new PropertyChangedEventArgs(info);

            PropertyChanged(this, args);
        }





        private ILink AddLink(string name, string startCityName, string finalCityName, int distance)
        {
            var startCity = GetCity(startCityName);
            var finalCity = GetCity(finalCityName);
            var link = new Link(name, startCity, finalCity, distance);

            Links.Add(link);

            return link;
        }

        private ICity AddCity(string name)
        {
            var city  = new City(name);

            Cities.Add(city);

            return city;
        }

        /// <summary>
        /// Get the target city.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Target city</returns>
        private ICity GetCity(string name)
        {
            try
            {
                var city = Cities.FirstOrDefault(c => c.Name.Equals(name));

                return city;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Get the target link.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Target link</returns>
        private ILink GetLink(string name)
        {
            try
            {
                var link = Links.FirstOrDefault(l => l.Name.Equals(name));

                return link;
            }
            catch
            {
                return null;
            }
        }
    }
}

