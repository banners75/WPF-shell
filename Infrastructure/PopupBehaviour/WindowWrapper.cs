using System;
using System.Windows;
using MahApps.Metro.Controls;

namespace Infrastructure.PopupBehaviour
{
    public class WindowWrapper : IWindow
    {
        protected readonly Window Window;

        public WindowWrapper(double width, double height, string title)
        {
            //Window = new Window { Width = width, Height = height };

            Window = new MetroWindow { Width = width, Height = height };
            Window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Window.Title = title;
            Window.Resources.MergedDictionaries.Add(new ResourceDictionary { Source = new Uri(@"pack://application:,,,/MahApps.Metro;component/Styles/Colours.xaml") });
            Window.Resources.MergedDictionaries.Add(new ResourceDictionary { Source = new Uri(@"pack://application:,,,/MahApps.Metro;component/Styles/Fonts.xaml") });
            Window.Resources.MergedDictionaries.Add(new ResourceDictionary { Source = new Uri(@"pack://application:,,,/MahApps.Metro;component/Styles/Controls.xaml") });
            Window.Resources.MergedDictionaries.Add(new ResourceDictionary { Source = new Uri(@"pack://application:,,,/MahApps.Metro;component/Styles/Accents/BaseLight.xaml") });
            Window.Resources.MergedDictionaries.Add(new ResourceDictionary { Source = new Uri(@"pack://application:,,,/MahApps.Metro;component/Styles/Accents/Red.xaml") });
            Window.Resources.MergedDictionaries.Add(new ResourceDictionary { Source = new Uri(@"pack://application:,,,/Shell;component/Resources/MetroStyleOverrides.xaml") });
        }

        public event EventHandler Closed
        {
            add { Window.Closed += value; }
            remove { Window.Closed -= value; }
        }

        public object Content
        {
            get { return Window.Content; }
            set { Window.Content = value; }
        }

        public object Owner
        {
            get { return Window.Owner; }
            set { Window.Owner = value as Window; }
        }

        public Style Style
        {
            get { return Window.Style; }
            set { Window.Style = value; }
        }

        public virtual void Show()
        {
            Window.Show();
        }

        public void Close()
        {
            Window.Close();
        }
    }
}