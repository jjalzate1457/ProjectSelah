using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ProjectSelah.API.Attached_Behaviors
{
    public class MouseActions
    {
        #region Double Click
        public static DependencyProperty DoubleClickCommandProperty =
            DependencyProperty.RegisterAttached("DoubleClickCommand",
            typeof(ICommand),
            typeof(MouseActions),
            new UIPropertyMetadata(DoubleClickCommandChanged));

        public static DependencyProperty DoubleClickCommandParameterProperty =
            DependencyProperty.RegisterAttached("DoubleClickCommandParameter",
                                                typeof(object),
                                                typeof(MouseActions),
                                                new UIPropertyMetadata(null));

        public static void SetDoubleClickCommand(DependencyObject target, ICommand value)
        {
            target.SetValue(DoubleClickCommandProperty, value);
        }

        public static void SetDoubleClickCommandParameter(DependencyObject target, object value)
        {
            target.SetValue(DoubleClickCommandParameterProperty, value);
        }
        public static object GetDoubleClickCommandParameter(DependencyObject target)
        {
            return target.GetValue(DoubleClickCommandParameterProperty);
        }

        private static void DoubleClickCommandChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            Control control = target as Control;
            if (control != null)
            {
                if ((e.NewValue != null) && (e.OldValue == null))
                {
                    control.MouseDoubleClick += OnMouseDoubleClick;
                }
                else if ((e.NewValue == null) && (e.OldValue != null))
                {
                    control.MouseDoubleClick -= OnMouseDoubleClick;
                }
            }
        }

        private static void OnMouseDoubleClick(object sender, RoutedEventArgs e)
        {
            Control control = sender as Control;
            ICommand command = (ICommand)control.GetValue(DoubleClickCommandProperty);
            object commandParameter = control.GetValue(DoubleClickCommandParameterProperty);
            command.Execute(commandParameter);
        }
        #endregion

        #region MouseLeave
        public static DependencyProperty MouseLeaveCommandProperty =
            DependencyProperty.RegisterAttached("MouseLeaveCommand",
            typeof(ICommand),
            typeof(MouseActions),
            new UIPropertyMetadata(MouseLeaveCommandChanged));

        public static DependencyProperty MouseLeaveCommandParameterProperty =
            DependencyProperty.RegisterAttached("MouseLeaveCommandParameter",
                                                typeof(object),
                                                typeof(MouseActions),
                                                new UIPropertyMetadata(null));

        public static void SetMouseLeaveCommand(DependencyObject target, ICommand value)
        {
            target.SetValue(MouseLeaveCommandProperty, value);
        }

        public static void SetMouseLeaveCommandParameter(DependencyObject target, object value)
        {
            target.SetValue(MouseLeaveCommandParameterProperty, value);
        }
        public static object GeMouseLeaveCommandParameter(DependencyObject target)
        {
            return target.GetValue(MouseLeaveCommandParameterProperty);
        }

        private static void MouseLeaveCommandChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            Control control = target as Control;
            if (control != null)
            {
                if ((e.NewValue != null) && (e.OldValue == null))
                {
                    control.MouseLeave += OnMouseLeave;
                }
                else if ((e.NewValue == null) && (e.OldValue != null))
                {
                    control.MouseLeave -= OnMouseLeave;
                }
            }
        }

        private static void OnMouseLeave(object sender, RoutedEventArgs e)
        {
            Control control = sender as Control;
            ICommand command = (ICommand)control.GetValue(MouseLeaveCommandProperty);
            object commandParameter = control.GetValue(MouseLeaveCommandParameterProperty);
            command.Execute(commandParameter);
        }
        #endregion

        #region MouseEnter
        public static DependencyProperty MouseEnterCommandProperty =
            DependencyProperty.RegisterAttached("MouseEnterCommand",
            typeof(ICommand),
            typeof(MouseActions),
            new UIPropertyMetadata(MouseEnterCommandChanged));

        public static DependencyProperty MouseEnterCommandParameterProperty =
            DependencyProperty.RegisterAttached("MouseEnterCommandParameter",
                                                typeof(object),
                                                typeof(MouseActions),
                                                new UIPropertyMetadata(null));

        public static void SetMouseEnterCommand(DependencyObject target, ICommand value)
        {
            target.SetValue(MouseEnterCommandProperty, value);
        }

        public static void SetMouseEnterCommandParameter(DependencyObject target, object value)
        {
            target.SetValue(MouseEnterCommandParameterProperty, value);
        }
        public static object GeMouseEnterCommandParameter(DependencyObject target)
        {
            return target.GetValue(MouseEnterCommandParameterProperty);
        }

        private static void MouseEnterCommandChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            Control control = target as Control;
            if (control != null)
            {
                if ((e.NewValue != null) && (e.OldValue == null))
                {
                    control.MouseEnter += OnMouseEnter;
                }
                else if ((e.NewValue == null) && (e.OldValue != null))
                {
                    control.MouseEnter -= OnMouseEnter;
                }
            }
        }

        private static void OnMouseEnter(object sender, RoutedEventArgs e)
        {
            Control control = sender as Control;
            ICommand command = (ICommand)control.GetValue(MouseEnterCommandProperty);
            object commandParameter = control.GetValue(MouseEnterCommandParameterProperty);
            command.Execute(commandParameter);
        }
        #endregion
    }

}
