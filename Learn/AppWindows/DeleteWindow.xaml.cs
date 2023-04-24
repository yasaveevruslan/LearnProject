using Learn.Model;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Learn.AppWindows
{
    /// <summary>
    /// Логика взаимодействия для DeleteWindow.xaml
    /// </summary>
    public partial class DeleteWindow : Window
    {
        Service contextService;
        public DeleteWindow(Service service)
        {
            InitializeComponent();
            contextService = service;
            DataContext = contextService;
            
        }

        private void BAgree_Click(object sender, RoutedEventArgs e)
        {
            App.DB.Service.Remove(contextService);
            App.DB.SaveChanges();
            this.DialogResult = true;
        }

        private void BCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
