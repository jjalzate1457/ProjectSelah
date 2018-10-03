using ProjectSelah.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjectSelah.API.CustomControls
{
    /// <summary>
    /// Interaction logic for LiveEditControl.xaml
    /// </summary>
    public partial class LiveEditControl : UserControl
    {
        private Song Data { get; set; }

        public LiveEditControl()
        {
            InitializeComponent();

            KeyBinding keyBinding = new KeyBinding(
                ApplicationCommands.NotACommand, 
                Key.Enter, ModifierKeys.Control);

            editor.InputBindings.Add(keyBinding);

            editor.KeyDown += Editor_KeyDown;
        }

        public void Edit(Song song)
        {
            Data = song.Clone();
        }

        private void Editor_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                if (e.Key == Key.Enter)
                {
                    
                }
            }
        }
    }
}
