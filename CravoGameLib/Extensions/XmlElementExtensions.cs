using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CravoGameLib.Extensions
{
    /// <summary>
    /// Extension methods for XmlElement
    /// </summary>
    public static class XmlElementExtensions
    {
        /// <summary>
        /// If an element has the specified attribute, return it as a string, otherwise return the default
        /// </summary>
        /// <param name="element">The element to check</param>
        /// <param name="attribute">The attribute name</param>
        /// <param name="defaultValue">The default value if the attribute doesn't exist</param>
        /// <returns>The attribute value, or the default if the attribute didn't exist</returns>
        public static string TryGetString(this XmlElement element, string attribute, string defaultValue)
        {
            if(element.HasAttribute(attribute))
            {
                return element.GetAttribute(attribute);
            }
            else
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// If an element has the specified attribute, return it as an int, otherwise return the default
        /// </summary>
        /// <param name="element">The element to check</param>
        /// <param name="attribute">The attribute name</param>
        /// <param name="defaultValue">The default value if the attribute doesn't exist</param>
        /// <returns>The attribute value, or the default if the attribute didn't exist</returns>
        public static int TryGetInt(this XmlElement element, string attribute, int defaultValue)
        {
            if(element.HasAttribute(attribute))
            {
                return Convert.ToInt32(element.GetAttribute(attribute));
            }
            else
            {
                return defaultValue;
            }
        }
    }
}
