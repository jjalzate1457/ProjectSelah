using ProjectSelah.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectSelah.API
{
    public static class SongModelExtensions
    {
        public static bool Export(this Song data)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return false;
        }
    }

    public static class DataClassExtenstions
    {
        /// <summary>
        /// Object extension of List of Lyrics that returns true if the Lyrics are the same
        /// </summary>
        /// <param name="collection1"></param>
        /// <param name="collection2"></param>
        /// <returns></returns>
        public static bool CompareTo(this IList<Lyrics> collection1, IList<Lyrics> collection2)
        {
            string stanza1, stanza2;

            if (collection2 != null && collection1.Count() == collection2.Count())
            {
                collection1 = new List<Lyrics>(collection1.OrderBy(i => i.Order));
                collection2 = new List<Lyrics>(collection2.OrderBy(i => i.Order));

                for (int a = 0; a < collection1.Count(); a++)
                {
                    stanza1 = collection1[a].Stanza.Replace("\\r\\n", "$");
                    stanza2 = collection2[a].Stanza.Replace("\\r\\n", "$");

                    if (collection1[a].Stanza != collection2[a].Stanza)
                        return false;
                }
            }

            return true;
        }

        public static string AsText(this IList<Lyrics> lyricsCollection)
        {
            StringBuilder lyricsText = new StringBuilder();

            if (lyricsCollection.Count(i => i.Stanza != "") > 0)
            {
                Header header = null;

                foreach (Lyrics item in lyricsCollection)
                {
                    if (item.Header != null && (header == null || item.Header.Id != header.Id))
                    {
                        header = item.Header;
                        lyricsText.Append(header.Name + Environment.NewLine);
                    }

                    lyricsText.Append(item.Stanza + Environment.NewLine);
                }
            }

            lyricsText.Append(Constants.EOFMarker);

            return lyricsText.ToString();
        }
    }

    public class ModelFunctions
    {
        public static IEnumerable<Lyrics> ConsolidateLyrics(string text)
        {
            Dictionary<string, Header> headers = new Dictionary<string, Header>();

            using (var c = new Data_Access.DatabaseContext())
            {
                foreach (var header in c.Headers)
                {
                    headers.Add(header.Name.ToLower(), header);
                }
            }

            List<Lyrics> lyricsCollection = new List<Lyrics>();

            Header currentHeader = headers.FirstOrDefault(i => i.Value.IsDefault).Value;

            StringBuilder stanza = new StringBuilder();

            if (text.Replace(Constants.EOFMarker, "") == "") return lyricsCollection;

            foreach (string line in text.Trim().Replace("\r\n", "%").Split('%'))
            {
                if (line.Trim() == "" || line.Trim() == Constants.EOFMarker)
                {
                    lyricsCollection.Add(new Lyrics()
                    {
                        Name = "",
                        Stanza = stanza.ToString(),
                        Header = currentHeader,
                        HeaderId = currentHeader.Id.ToString()
                    });

                    stanza.Clear();
                }
                else
                {
                    if (headers.ContainsKey(line.Trim().ToLower()))
                    {
                        currentHeader = headers[line.Trim().ToLower()];
                    }
                    else
                    {
                        stanza.Append(line);
                        stanza.Append(Environment.NewLine);
                    }
                }
            }

            return lyricsCollection;
        }
    }
}
