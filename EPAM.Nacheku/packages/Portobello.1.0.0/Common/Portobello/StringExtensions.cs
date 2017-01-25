using System;
using System.Linq;
using System.Text;

namespace Expoware.Portobello.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Parse a given string to a number.
        /// </summary>
        /// <param name="theString">String to parse</param>
        /// <param name="defaultNumber">Value to return in case of failure</param>
        /// <returns>Integer</returns>
        public static Int32 ToInt(this String theString, Int32 defaultNumber = 0)
        {
            Int32 number;
            var success = Int32.TryParse(theString, out number);
            return success ? number : defaultNumber;
        }

        /// <summary>
        /// Parse a given string to a Boolean.
        /// </summary>
        /// <param name="theString">String to parse</param>
        /// <param name="defaultValue">Value to return in case of failure</param>
        /// <returns>True/False</returns>
        public static Boolean ToBool(this String theString, Boolean defaultValue = false)
        {
            Boolean response;
            var success = Boolean.TryParse(theString, out response);
            if (success)
                return response;

            // Make a few more attempts (yes/no, 1/0)
            if (theString.EqualsAny("yes", "1", "y"))
                return true;
            if (theString.EqualsAny("no", "0", "n"))
                return false;

            return defaultValue;
        }

        /// <summary>
        /// Parse a given string to a date.
        /// </summary>
        /// <param name="theString">String to parse</param>
        /// <param name="defaultDate">Date to return in case of failure</param>
        /// <returns>Date</returns>
        public static DateTime ToDate(this String theString, DateTime defaultDate = default(DateTime))
        {
            DateTime date;
            var success = DateTime.TryParse(theString, out date);
            return success ? date : defaultDate;  //DateTime.MinValue;
        }

        /// <summary>
        /// Indicate whether a given string contains any of the specified substrings. 
        /// </summary>
        /// <param name="theString">String to process</param>
        /// <param name="args">List of substrings</param>
        /// <returns>True/False</returns>
        public static Boolean ContainsAny(this String theString, params String[] args)
        {
            var temp = theString.ToLower();
            return args.Any(s => temp.Contains(s.ToLower()));
        }

        /// <summary>
        /// Indicate whether a given string equals any of the specified substrings. 
        /// </summary>
        /// <param name="theString">String to process</param>
        /// <param name="args">List of possible matches</param>
        /// <returns>True/False</returns>
        public static Boolean EqualsAny(this String theString, params String[] args)
        {
            return args.Any(token => theString.Equals(token, StringComparison.InvariantCultureIgnoreCase));
        }

        /// <summary>
        /// Get a slice of the provided string that begins at specified substring.
        /// </summary>
        /// <param name="theString">String to process</param>
        /// <param name="marker">Substring to locate</param>
        /// <param name="shouldIncludeMarker">Whether substring should be skipped or included</param>
        /// <returns>Substring</returns>
        public static String SubstringFrom(this String theString, String marker, Boolean shouldIncludeMarker = false)
        {
            var index = theString.IndexOf(marker, StringComparison.InvariantCultureIgnoreCase);
            if (index < 0)
                return theString;

            var startIndex = shouldIncludeMarker ? index : index + marker.Length;
            return theString.Substring(startIndex);
        }

        /// <summary>
        /// Get a slice of the provided string that ends at the specified substring.
        /// </summary>
        /// <param name="theString">String to process</param>
        /// <param name="marker">Substring to locate</param>
        /// <param name="shouldIncludeMarker">Whether substring should be skipped or included</param>
        /// <returns>Substring</returns>
        public static String SubstringTo(this String theString, String marker, Boolean shouldIncludeMarker = false)
        {
            var index = theString.IndexOf(marker, StringComparison.InvariantCultureIgnoreCase);
            if (index < 0)
                return theString;

            var endIndex = shouldIncludeMarker ? index + marker.Length : index;
            return theString.Substring(0, endIndex);
        }

        /// <summary>
        /// Get a slice of the provided string included between markers (not included)
        /// </summary>
        /// <param name="theString">String to process</param>
        /// <param name="marker1">Initial substring</param>
        /// <param name="marker2">Ending substring</param>
        /// <returns>Substring</returns>
        public static String SubstringBetween(this String theString, String marker1, String marker2)
        {
            var temp = theString.SubstringFrom(marker1);
            return temp.SubstringTo(marker2);
        }

        /// <summary>
        /// Indicate whether the given string is NULL or empty or whitespace.
        /// </summary>
        /// <param name="theString">String to process</param>
        /// <returns>True/False</returns>
        public static Boolean IsNullOrWhitespace(this String theString)
        {
            return String.IsNullOrWhiteSpace(theString);
        }

        /// <summary>
        /// Repeats the specified string for the specified number of times.
        /// </summary>
        /// <param name="theString">String to process</param>
        /// <param name="count">Number of repetitions</param>
        /// <returns>New string</returns>
        public static String Repeat(this String theString, Int32 count = 2)
        {
            if (count <= 0 || String.IsNullOrEmpty(theString))
                return String.Empty;

            var builder = new StringBuilder();
            for (var i = 0; i < count; i++)
            {
                builder.Append(theString);
            }
            return builder.ToString();
        }

        /// <summary>
        /// Counts the number of words in the string
        /// </summary>
        /// <param name="theString">String to process</param>
        /// <returns>Number of words in the string</returns>
        public static Int32 WordCount(this String theString)
        {
            if (String.IsNullOrWhiteSpace(theString))
                return 0;

            var buffer = theString.Trim();
            var count = buffer.Split(' ').Length;
            return count;
        }

        /// <summary>
        /// Counts the number of characters in the string
        /// </summary>
        /// <param name="theString">String to process</param>
        /// <returns>Number of characters in the string</returns>
        public static Int32 CharCount(this String theString)
        {
            if (String.IsNullOrWhiteSpace(theString))
                return 0;
            return theString.Length;
        }
    }
}