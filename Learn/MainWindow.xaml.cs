using Learn.Pages;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Learn
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            App.MainWindowInstance = this;
            MainFrame.Navigate(new PListService());
        }

        private void BBack_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.GoBack();
        }

        private void TBCode_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(TBCode.Text == "0000")
            {
                MainFrame.Navigate(new PListService());
               
            }
            else
            {
                MainFrame.Navigate(new PListService());
                
            }
        }
    }
}
