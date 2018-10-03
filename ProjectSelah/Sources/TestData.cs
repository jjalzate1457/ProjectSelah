using ProjectSelah.API;
using ProjectSelah.Data_Access;
using ProjectSelah.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace ProjectSelah
{
    public class DefaultData
    {
        static int alpha = 100;

        public static void GenerateHeaders(bool force = false)
        {
            using (var c = new DatabaseContext())
            {
                if (force || c.Headers.Count() == 0)
                {
                    List<Header> headers = new List<Header>
                    {
                        new Header() { Name = "Verse", Highlight = Color(255, 0, 0), IsDefault = true },
                        new Header() { Name = "Verse 1", Highlight = Color(255, 0, 0) },
                        new Header() { Name = "Verse 2", Highlight = Color(255, 0, 0) },
                        new Header() { Name = "Verse 3", Highlight = Color(255, 0, 0) },
                        new Header() { Name = "Verse 4", Highlight = Color(255, 0, 0) },
                        new Header() { Name = "Verse 5", Highlight = Color(255, 0, 0) },
                        new Header() { Name = "Verse 6", Highlight = Color(255, 0, 0) },
                        new Header() { Name = "Verse 7", Highlight = Color(255, 0, 0) },
                        new Header() { Name = "Verse 8", Highlight = Color(255, 0, 0) },
                        new Header() { Name = "Verse 9", Highlight = Color(255, 0, 0) },
                        new Header() { Name = "Verse 10", Highlight = Color(255, 0, 0) },
                        new Header() { Name = "Verse 11", Highlight = Color(255, 0, 0) },
                        new Header() { Name = "Verse 12", Highlight = Color(255, 0, 0) },
                        new Header() { Name = "Verse 13", Highlight = Color(255, 0, 0) },
                        new Header() { Name = "Verse 14", Highlight = Color(255, 0, 0) },
                        new Header() { Name = "Verse 15", Highlight = Color(255, 0, 0) },
                        new Header() { Name = "Bridge", Highlight = Color(255, 128, 0) },
                        new Header() { Name = "Bridge 1", Highlight = Color(255, 128, 0) },
                        new Header() { Name = "Bridge 2", Highlight = Color(255, 128, 0) },
                        new Header() { Name = "Pre-Chorus", Highlight = Color(255, 255, 0) },
                        new Header() { Name = "Chorus", Highlight = Color(255, 255, 0) },
                        new Header() { Name = "Chorus 1", Highlight = Color(255, 255, 0) },
                        new Header() { Name = "Chorus 2", Highlight = Color(255, 255, 0) },
                        new Header() { Name = "Chorus 3", Highlight = Color(255, 255, 0) },
                        new Header() { Name = "Chorus 4", Highlight = Color(255, 255, 0) },
                        new Header() { Name = "Chorus 5", Highlight = Color(255, 255, 0) },
                        new Header() { Name = "Refrain", Highlight = Color(0, 255, 0) },
                        new Header() { Name = "Coda", Highlight = Color(0, 0, 255) },
                        new Header() { Name = "End", Highlight = Color(255, 0, 128) },
                        new Header() { Name = "Last", Highlight = Color(255, 0, 128) }
                    };

                    if (force)
                    {
                        foreach (Header header in headers)
                        {
                            Header item = (from h in c.Headers
                                           where h.Name == header.Name
                                           select h).FirstOrDefault();

                            if (item != default(Header))
                            {
                                string id = item.Id;
                                item.Update(header);
                                item.Id = id;

                            }
                        }

                        c.SaveChanges();
                    }
                    else if (c.Headers.Count() == 0)
                    {
                        foreach (Header header in headers)
                        {
                            c.Headers.Add(header);
                        }

                        c.SaveChanges();
                    }
                }
            }
        }

        static Color Color(object Red, object Green, object Blue)
        {
            return Color(alpha, Red, Green, Blue);
        }

        static Color Color(object Alpha, object Red, object Green, object Blue)
        {
            if (
                (Alpha.GetType() == typeof(long) || Alpha.GetType() == typeof(int)) &&
                (Red.GetType() == typeof(long) || Red.GetType() == typeof(int)) &&
                (Green.GetType() == typeof(long) || Green.GetType() == typeof(int)) &&
                (Blue.GetType() == typeof(long) || Blue.GetType() == typeof(int))
            )
                return (Color)ColorConverter.ConvertFromString("#" + Int(Alpha).ToString("X2") + Int(Red).ToString("X2") + Int(Green).ToString("X2") + Int(Blue).ToString("X2"));
            return new Color();
        }

        static int Int(object Value)
        {
            return Convert.ToInt32(Value);
        }

        public static Song Song1
        {
            get
            {
                List<string> lines = new List<string>
                {
                    "I love You Lord",
                    "And I lift my voice",
                    "To worship You",
                    "All my soul, rejoice",
                    Environment.NewLine,
                    "Take joy my King",
                    "In what You hear",
                    "Let it be a sweet, sweet sound",
                    "In Your ears"
                };

                return new Song
                {
                    Name = "I Love You Lord",
                    Lyrics = new System.Collections.ObjectModel.ObservableCollection<Lyrics>(CreateLyricsCollection(lines))
                };
            }
        }

        public static Song Song2
        {
            get
            {


                List<string> lines = new List<string>
                {
                    "Verse 1",
                    "The splendor of the King",
                    "Clothed in majesty",
                    "Let all the earth rejoice",
                    "All the earth rejoice",
                    Environment.NewLine,
                    "He wraps himself in Light",
                    "And darkness tries to hide",
                    "And trembles at His voice",
                    "Trembles at His voice",
                    Environment.NewLine,
                    "Chorus",
                    "How great is our God, sing with me",
                    "How great is our God, and all will see",
                    "How great, how great is our God",
                    Environment.NewLine,
                    "Verse 2",
                    "Age to age He stands",
                    "And time is in His hands",
                    "Beginning and the end",
                    "Beginning and the end",
                    Environment.NewLine,
                    "The Godhead Three in One",
                    "Father Spirit Son",
                    "The Lion and the Lamb",
                    "The Lion and the Lamb",
                    Environment.NewLine,
                    "Bridge",
                    "Name above all names",
                    "Worthy of our praise",
                    "My heart will sing",
                    "How great is our God"
                };

                return new Song
                {
                    Name = "How great is our God",
                    Lyrics = new System.Collections.ObjectModel.ObservableCollection<Lyrics>(CreateLyricsCollection(lines))
                };
            }
        }

        static IEnumerable<Lyrics> CreateLyricsCollection(List<string> lines)
        {
            var lyricsCollection = new List<Lyrics>();
            using (var c = new DatabaseContext())
            {
                var currentHeader = ((IEnumerable<Header>)from header in c.Headers
                                                          where header.IsDefault
                                                          select header).FirstOrDefault();
                var stanza = new StringBuilder();

                lines.Add("");

                foreach (var line in lines)
                {
                    if (line.Trim() == "")
                    {
                        lyricsCollection.Add(new Lyrics()
                        {
                            Name = "",
                            Stanza = stanza.ToString(),
                            Header = currentHeader,
                            HeaderId = currentHeader.Id.ToString()
                        });
                    }
                    else
                    {
                        {
                            IEnumerable<Header> q = from header in c.Headers
                                                    where header.Name.ToLower() == line.Trim().ToLower()
                                                    select header;
                            if (q.FirstOrDefault() == default(Header))
                            {
                                stanza.Append(line);
                                stanza.Append(Environment.NewLine);
                            }
                            else
                            {
                                currentHeader = q.FirstOrDefault();
                            }
                        }
                    }
                }

                return lyricsCollection;
            }
        }
    }
}
