using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Mod03_ChelasMovies.WebApp.Models.ViewModels
{
    /// <summary>
    /// View Model for the input entered by the user for searching movies
    /// </summary>
    public class SearchViewModel : BaseViewModel
    {
        #region Properties

        public string Title { get; set; }
        public int? Year { get; set; }    
        public string Genre { get; set; }
        public string Director { get; set; }

        #endregion Properties

        #region Public Methods

        /// <summary>
        /// Build a string to be used by the Dynamic Query for filtering
        /// </summary>
        /// <returns>String with the filter expressions builted from the object Properties</returns>
        public string BuildFilter()
        {
            StringBuilder builder = new StringBuilder();
            List<string> buffer = new List<string>();
            PropertyInfo[] propertyInfos = this.GetType().GetProperties();

            foreach (var propertyInfo in propertyInfos)
            {
                var propertyValue = propertyInfo.GetValue(this, null);

                // if the Property is a String then check if the lowercase Title contains the lowercase search criteria
                if (propertyInfo.PropertyType == typeof(string) && propertyValue != null)
                {
                    buffer.Add(string.Format("{0}.ToLower().Contains(\"{1}\".ToLower())", propertyInfo.Name, propertyValue));
                    continue;
                }

                // if the Property type is a Value Type then compare the Property with the search criteria
                if (propertyInfo.PropertyType.IsValueType && propertyValue != null)
                {
                    buffer.Add(string.Format("{0}={1}", propertyInfo.Name, propertyValue));
                    continue;
                }

                // if the Property is a Nullable type then the Property Value with the search criteria
                if (propertyInfo.PropertyType.IsGenericType && propertyInfo.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) && propertyValue != null)
                {
                    buffer.Add(string.Format("{0}={1}", propertyInfo.Name, propertyValue));
                    continue;
                }
            }

            // buil a list of AND expressions
            foreach (var item in buffer)
            {
                builder.Append(builder.Length == 0 ? item : string.Format("&& {0} ", item));
            }

            return builder.ToString();
        }

        #endregion Public Methods
    }
}