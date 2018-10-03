using ProjectSelah.Data_Access;
using ProjectSelah.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Timers;

namespace ProjectSelah.API.CustomControls
{
    [DesignerCategory("Code")]
    public class SelahLyricsEditor : RichTextBox
    {
        public bool IsDirty
        {
            get { return originalText != Text; }
        }

        bool allowRender;
        bool allowContentNotification;
        bool textChangedLock;

        Dictionary<string, Header> headers;

        string originalText;

        Timer keypressTimer;

        public string Text
        {
            get
            {
                return new TextRange(
                    Document.ContentStart,
                    Document.ContentEnd).Text.Trim() +
                    Environment.NewLine +
                    "_$ENDOFTEXT$_";
            }
        }

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
            (d as SelahLyricsEditor).SetInternalLyrics((e.NewValue as ObservableCollection<Lyrics>));
        }

        private void SetInternalLyrics(ObservableCollection<Lyrics> value)
        {
            if (!allowContentNotification)
                return;

            Document.Blocks.Clear();

            allowRender = false;

            textChangedLock = true;

            if (value != null)
            {
                IOrderedEnumerable<Lyrics> LyricsList = value.OrderBy(i => i.Order);

                Header Header = new Header();

                int counter = 0;

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

                    if (counter != LyricsList.Count() - 1)
                        AddParagraph();

                    counter++;
                }
            }

            originalText = Text;

            allowRender = true;

            textChangedLock = false;

            InvalidateVisual();
        }

        private void UpdateLyrics()
        {
            ObservableCollection<Lyrics> lyricsCollection = new ObservableCollection<Lyrics>();

            Header currentHeader = headers.FirstOrDefault(i => i.Value.IsDefault).Value;

            StringBuilder stanza = new StringBuilder();

            int a = 0;

            foreach (string line in Text.Trim().Replace("\r", "%").Replace("\n", "%").Replace("\r\n", "%").Replace("%%", "%").Split('%'))
            {
                if (line.Trim() == "" || line.Trim() == "_$ENDOFTEXT$_")
                {
                    lyricsCollection.Add(new Lyrics()
                    {
                        Name = "",
                        Stanza = stanza.ToString(),
                        Header = currentHeader,
                        HeaderId = currentHeader.Id.ToString(),
                        Order = a++
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

            allowContentNotification = false;

            Lyrics = lyricsCollection;

            allowContentNotification = true;
        }

        private void AddParagraph(string Value = "")
        {
            var p = new Paragraph();
            p.Inlines.Add(Value.Trim());
            p.Margin = new Thickness(0);
            Document.Blocks.Add(p);
        }
        #endregion Lyrics Dependency Property

        public SelahLyricsEditor()
        {
            allowContentNotification = true;

            keypressTimer = new Timer(1500);
            keypressTimer.Elapsed += (o, e) =>
            {
                NotifyLyricsProperty();
                keypressTimer.Stop();
            };

            //TextChanged += SelahLyricsEditor_TextChanged;
        }

        private void SelahLyricsEditor_TextChanged(object sender, TextChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void RefreshHeaders()
        {
            if (headers == null)
                headers = new Dictionary<string, Header>();

            headers.Clear();

            using (var c = new DatabaseContext())
            {
                foreach (var header in c.Headers)
                {
                    headers.Add(header.Name.ToLower(), header);
                }
            }

            allowContentNotification = true;
        }

        public void NotifyLyricsProperty()
        {
            allowContentNotification = false;

            UpdateLyrics();

            allowContentNotification = true;
        }

        public void UpdateOriginalText()
        {
            originalText = Text;
        }

        protected override void OnLostFocus(RoutedEventArgs e)
        {
            base.OnLostFocus(e);

            NotifyLyricsProperty();
        }

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            if (textChangedLock)
                return;

            allowRender = true;

            InvalidateVisual();

            NotifyLyricsProperty();

            base.OnTextChanged(e);
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            if (!allowRender || FormExtensions.IsInDesignMode)
                return;

            textChangedLock = true;

            foreach (var block in Document.Blocks)
            {
                var linevalue = new TextRange(block.ContentStart, block.ContentEnd).Text.Trim().ToLower();

                block.Background = new SolidColorBrush(Colors.White);
                block.FontWeight = FontWeights.Normal;

                if (linevalue != "" && linevalue.Length < 10)
                {
                    if (headers.TryGetValue(linevalue.ToLower(), out Header temp))
                    {
                        block.Background = new SolidColorBrush(temp.Highlight);
                        block.FontWeight = FontWeights.SemiBold;
                    }
                }

                block.Margin = new Thickness(0);
            }

            textChangedLock = false;
        }
    }
};