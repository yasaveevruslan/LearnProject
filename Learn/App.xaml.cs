using Learn.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Learn
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static LearnEntities DB = new LearnEntities();
        public static MainWindow MainWindowInstance;
        public App()
        {
            DispatcherUnhandledException += App_DispatcherUnhadledExeption;
        }
        private void App_DispatcherUnhadledExeption(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message);
            e.Handled = true;
        }
    }
}
