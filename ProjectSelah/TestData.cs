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

        public static void GenerateHeaders()
        {

            using (var c = new Data_Access.DatabaseContext())
            {
                if (c.Headers.Count() > 0) return;

                c.Headers.Add(new Header() { Name = "Verse", Highlight = Color(alpha, 0, 0), IsDefault = true });
                c.Headers.Add(new Header() { Name = "Verse 1", Highlight = Color(alpha, 0, 0) });
                c.Headers.Add(new Header() { Name = "Verse 2", Highlight = Color(alpha, 0, 0) });
                c.Headers.Add(new Header() { Name = "Verse 3", Highlight = Color(alpha, 0, 0) });
                c.Headers.Add(new Header() { Name = "Verse 4", Highlight = Color(alpha, 0, 0) });
                c.Headers.Add(new Header() { Name = "Verse 5", Highlight = Color(alpha, 0, 0) });
                c.Headers.Add(new Header() { Name = "Verse 6", Highlight = Color(alpha, 0, 0) });
                c.Headers.Add(new Header() { Name = "Verse 7", Highlight = Color(alpha, 0, 0) });
                c.Headers.Add(new Header() { Name = "Verse 8", Highlight = Color(alpha, 0, 0) });
                c.Headers.Add(new Header() { Name = "Verse 9", Highlight = Color(alpha, 0, 0) });
                c.Headers.Add(new Header() { Name = "Verse 10", Highlight = Color(alpha, 0, 0) });
                c.Headers.Add(new Header() { Name = "Verse 11", Highlight = Color(alpha, 0, 0) });
                c.Headers.Add(new Header() { Name = "Verse 12", Highlight = Color(alpha, 0, 0) });
                c.Headers.Add(new Header() { Name = "Verse 13", Highlight = Color(alpha, 0, 0) });
                c.Headers.Add(new Header() { Name = "Verse 14", Highlight = Color(alpha, 0, 0) });
                c.Headers.Add(new Header() { Name = "Verse 15", Highlight = Color(alpha, 0, 0) });
                c.Headers.Add(new Header() { Name = "Bridge", Highlight = Color(alpha, 128, 0) });
                c.Headers.Add(new Header() { Name = "Bridge 1", Highlight = Color(alpha, 128, 0) });
                c.Headers.Add(new Header() { Name = "Bridge 2", Highlight = Color(alpha, 128, 0) });
                c.Headers.Add(new Header() { Name = "Pre-Chorus", Highlight = Color(alpha, alpha, 0) });
                c.Headers.Add(new Header() { Name = "Chorus", Highlight = Color(alpha, alpha, 0) });
                c.Headers.Add(new Header() { Name = "Chorus 1", Highlight = Color(alpha, alpha, 0) });
                c.Headers.Add(new Header() { Name = "Chorus 2", Highlight = Color(alpha, alpha, 0) });
                c.Headers.Add(new Header() { Name = "Chorus 3", Highlight = Color(alpha, alpha, 0) });
                c.Headers.Add(new Header() { Name = "Chorus 4", Highlight = Color(alpha, alpha, 0) });
                c.Headers.Add(new Header() { Name = "Chorus 5", Highlight = Color(alpha, alpha, 0) });
                c.Headers.Add(new Header() { Name = "Refrain", Highlight = Color(0, alpha, 0) });
                c.Headers.Add(new Header() { Name = "Coda", Highlight = Color(0, 0, alpha) });
                c.Headers.Add(new Header() { Name = "End", Highlight = Color(alpha, 0, 128) });
                c.Headers.Add(new Header() { Name = "Last", Highlight = Color(alpha, 0, 128) });

                c.SaveChanges();
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
                var data = new Song();
                data.Name = "I Love You Lord";
                var lines = new List<string>();
                lines.Add("I love You Lord");
                lines.Add("And I lift my voice");
                lines.Add("To worship You");
                lines.Add("All my soul, rejoice");
                lines.Add(Environment.NewLine);
                lines.Add("Take joy my King");
                lines.Add("In what You hear");
                lines.Add("Let it be a sweet, sweet sound");
                lines.Add("In Your ears");
                data.Lyrics = new System.Collections.ObjectModel.ObservableCollection<Lyrics>(CreateLyricsCollection(lines));
                return data;
            }
        }

        public static Song Song2
        {
            get
            {
                var data = new Song();
                data.Name = "How great is our God";
                var lines = new List<string>();
                lines.Add("Verse 1");
                lines.Add("The splendor of the King");
                lines.Add("Clothed in majesty");
                lines.Add("Let all the earth rejoice");
                lines.Add("All the earth rejoice");
                lines.Add(Environment.NewLine);
                lines.Add("He wraps himself in Light");
                lines.Add("And darkness tries to hide");
                lines.Add("And trembles at His voice");
                lines.Add("Trembles at His voice");
                lines.Add(Environment.NewLine);
                lines.Add("Chorus");
                lines.Add("How great is our God, sing with me");
                lines.Add("How great is our God, and all will see");
                lines.Add("How great, how great is our God");
                lines.Add(Environment.NewLine);
                lines.Add("Verse 2");
                lines.Add("Age to age He stands");
                lines.Add("And time is in His hands");
                lines.Add("Beginning and the end");
                lines.Add("Beginning and the end");
                lines.Add(Environment.NewLine);
                lines.Add("The Godhead Three in One");
                lines.Add("Father Spirit Son");
                lines.Add("The Lion and the Lamb");
                lines.Add("The Lion and the Lamb");
                lines.Add(Environment.NewLine);
                lines.Add("Bridge");
                lines.Add("Name above all names");
                lines.Add("Worthy of our praise");
                lines.Add("My heart will sing");
                lines.Add("How great is our God");
                data.Lyrics = new System.Collections.ObjectModel.ObservableCollection<Lyrics>(CreateLyricsCollection(lines));
                return data;
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
