using ProjectSelah.Data_Access;
using ProjectSelah.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace ProjectSelah.API.CustomControls
{
    [DesignerCategory("Code")]
    public class SelahLyricsEditor : RichTextBox
    {
        bool allowRender;
        bool allowLyricsPropertyNotify;

        #region Lyrics Dependency Property
        public static readonly DependencyProperty LyricsProperty =
          DependencyProperty.Register("Lyrics", typeof(ObservableCollection<Lyrics>),
             typeof(SelahLyricsEditor), new FrameworkPropertyMetadata(null,
                 FrameworkPropertyMetadataOptions.AffectsRender,
                   new PropertyChangedCallback(OnLyricsChange)));

        public ObservableCollection<Lyrics> Lyrics
        {
            get { return (ObservableCollection<Lyrics>)GetValue(LyricsProperty); }
            set { SetValue(LyricsProperty, value); }
        }

        private static void OnLyricsChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as SelahLyricsEditor).SetLyrics(e.NewValue as ObservableCollection<Lyrics>);
        }

        private void SetLyrics(ObservableCollection<Lyrics> LyricsList)
        {
            if (!allowLyricsPropertyNotify)
                return;

            Document.Blocks.Clear();

            allowRender = false;

            if (LyricsList != null)
            {
                var Header = new Header();

                foreach (var stanza in LyricsList)
                {
                    if (stanza.Header != null && stanza.Header.Name != null)
                    {
                        if (Header != stanza.Header)
                        {
                            Header = stanza.Header;
                            AddParagraph(stanza.Header.Name);
                        }
                    }

                    AddParagraph(stanza.Stanza);

                    if (stanza != LyricsList.Last())
                        AddParagraph("");
                }
            }
            allowRender = true;
            InvalidateVisual();
        }

        private void AddParagraph(string Value)
        {
            var p = new Paragraph();
            p.Inlines.Add(Value.Trim());
            p.Margin = new Thickness(0);
            Document.Blocks.Add(p);
        }
        #endregion Lyrics Dependency Property

        public SelahLyricsEditor()
        {
            allowLyricsPropertyNotify = true;
        }

        public void ForceRefreshLyrics()
        {
            allowLyricsPropertyNotify = false;
            Lyrics = new ObservableCollection<Lyrics>(ConsolidateLyrics());
            allowLyricsPropertyNotify = true;
        }

        protected override void OnLostFocus(RoutedEventArgs e)
        {
            base.OnLostFocus(e);

            ForceRefreshLyrics();
        }

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);
            if (!IsReadOnly && !FormExtensions.IsInDesignMode)
            {
                allowLyricsPropertyNotify = false;
                Lyrics = new ObservableCollection<Lyrics>(ConsolidateLyrics());
                allowLyricsPropertyNotify = true;
            }
            allowRender = true;
            InvalidateVisual();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            if (!allowRender || FormExtensions.IsInDesignMode) return;

            using (var c = new DatabaseContext())
            {
                foreach (var block in Document.Blocks)
                {
                    var linevalue = GetString(block as Paragraph).Trim().ToLower();

                    if (linevalue != "" && linevalue.Length < 10)
                    {
                        IEnumerable<Header> q = from header in c.Headers
                                                where header.Name.ToLower() == linevalue
                                                select header;

                        var temp = default(Header);

                        if ((temp = q.FirstOrDefault()) != default(Header))
                        {
                            block.Background = new SolidColorBrush(temp.Highlight);
                            block.FontWeight = FontWeights.SemiBold;
                        }
                    }

                    block.Margin = new Thickness(0);
                }
            }
        }

        private string GetString(Paragraph par)
        {
            return new TextRange(par.ContentStart, par.ContentEnd).Text.Trim();
        }

        private IEnumerable<Lyrics> ConsolidateLyrics()
        {
            var lyricsCollection = new List<Lyrics>();
            using (var c = new DatabaseContext())
            {
                var currentHeader = ((IEnumerable<Header>)from header in c.Headers
                                                          where header.IsDefault
                                                          select header).FirstOrDefault();

                var stanza = new StringBuilder();

                var lines = new TextRange(Document.ContentStart, Document.ContentEnd).Text.Trim() + Environment.NewLine + "_$ENDOFTEXT$_";

                if (lines.Replace("_$ENDOFTEXT$_", "") == "") return lyricsCollection;

                foreach (var line in lines.Trim().Replace("\r\n", "%").Split('%'))
                {
                    if (line.Trim() == "" || line.Trim() == "_$ENDOFTEXT$_")
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