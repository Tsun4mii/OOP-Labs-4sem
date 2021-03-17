using Lab_6_7.Settings;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Lab_6_7
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static Theme theme;
        private static Language language;
        public static Theme Theme
        {
            get => theme ?? (theme = new Theme());
        }
        public static Language Language
        {
            get => language ?? (language = new Language());
        }


        public App()
        {
            InitializeComponent();
        }
    }
}
