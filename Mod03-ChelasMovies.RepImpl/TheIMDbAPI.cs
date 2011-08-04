using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mod03_ChelasMovies.DomainModel;
using System.Net;
using System.IO;
using System.Xml.Linq;
using System.Text.RegularExpressions;

namespace Mod03_ChelasMovies.RepImpl
{
    public static class TheIMDbAPI
    {
        private static readonly Regex IMDbTimeRegex = new Regex(@"(\d+)\s*\w*\s*(\d*)\s*\w*", RegexOptions.IgnoreCase);

        private static TimeSpan? IMDbTimeToChelasTime(this string imdbTime)
        {
            if (String.IsNullOrEmpty(imdbTime)) return null;

            Match m = IMDbTimeRegex.Match(imdbTime);

            if (m.Groups.Count != 3 || m.Groups[1].Captures.Count != 1 || m.Groups[1].Captures[0].Length < 1)
                throw new ArgumentException(String.Format("Invalid IMDb time format: \"{0}\"", imdbTime));

            string runtime;
            if (m.Groups[2].Captures[0].Length > 0) {
                runtime = String.Format("{0}:{1}", m.Groups[1].Captures[0], m.Groups[2].Captures[0]);
            } else {
                runtime = String.Format("0:{0}", m.Groups[1].Captures[0]);
            }
            return TimeSpan.Parse(runtime);
        }

        private static string FixNA(this string str)
        {
            return str.ToUpper() == "N/A" ? "" : str;
        }

        public static Movie SearchByTitle(string title)
        {
            String url = String.Format("http://www.imdbapi.com/?i=&t={0}&r=xml", title);
            try
            {
                WebRequest req = WebRequest.Create(url);
                String response = new StreamReader(req.GetResponse().GetResponseStream()).ReadToEnd();
                XElement respXml = XElement.Parse(response).Element("movie");
                return new Movie
                {
                    Title = respXml.Attribute("title").Value,
                    Actors = respXml.Attribute("actors").Value,
                    Director = respXml.Attribute("director").Value,
                    Genre = respXml.Attribute("genre").Value,
                    Image = respXml.Attribute("poster").Value.FixNA(),
                    Year = Int32.Parse(respXml.Attribute("year").Value.FixNA()),
                    Runtime = respXml.Attribute("runtime").Value.FixNA().IMDbTimeToChelasTime() ?? default(TimeSpan)
                };
            } catch {
                return null;
            }
        }
    }
}
